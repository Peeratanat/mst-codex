using Base.DTOs.PRJ;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using Database.Models.ACC;
using Database.Models;
using System.Linq;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Database.Models.MasterKeys;

namespace Base.DTOs.ACC
{
    public class PostingGLDTO : BaseDTO
    {
        /// <summary>
        /// DocumentID ของรายการทั้งหมด
        /// </summary>
        public List<Guid> DocumentKey { get; set; }

        public List<PostingGLHeader> PostingGLHeader { get; set; }

        public Guid? SessionKey { get; set; }

        public static List<PostingGLHeader> CreateDTOFromPostGLTemps(List<PostGLTemp> model, ref List<Guid> DocumentKey)
        {
            var PostingGLDTO = new PostingGLDTO();

            if (model.Any())
            {
                var PostingGLHeaderList = new List<PostingGLHeader>();

                var CompanyList = model.Select(o => o.Company).Distinct().Select(o => CompanyDTO.CreateFromModel(o)).ToList() ?? new List<CompanyDTO>();

                // ตัดรายการซ้ำออก (เฉพาะเคส PI และ CA ที่มาจาก PI (CB = ระบบเก่า))
                model.Where(o => o.PostGLDocumentType.Key == PostGLDocumentTypeKeys.PI || (o.PostGLDocumentType.Key == PostGLDocumentTypeKeys.CA && (o.Description.Contains("PI") || o.Description.Contains("CB")))).ToList().ForEach(o => { o.Project = null; o.ProjectID = null; o.ProjectNo = null; o.Unit = null; o.UnitNo = null; });

                model.Where(o => o.PostGLDocumentType.Key == PostGLDocumentTypeKeys.JV && o.Description.Contains("ย้ายแปลง")).ToList().ForEach(o => { o.Unit = null; o.UnitNo = o.Description.Replace("ย้ายแปลง ", ""); });

                var headerList = model
                    .GroupBy(o => new
                    {
                        o.Company,
                        o.DocumentKey,
                        o.DocumentText,
                        o.PostGLDocumentType,
                        o.Description, 
                        o.PostingDate,
                        o.Project,
                        o.UnitNo,
                        o.TotalAmount,
                        o.TotalFee
                    }).Select(o => new PostingGLHeader
                    {
                        Company = CompanyDropdownDTO.CreateFromModel(o.Key.Company),
                        DocumentKey = o.Key.DocumentKey,
                        DocumentText = o.Key.DocumentText,
                        PostGLDocumentType = MasterCenterDropdownDTO.CreateFromModel(o.Key.PostGLDocumentType),
                        Description = o.Key.Description,
                        Project = ProjectDropdownDTO.CreateFromModel(o.Key.Project),
                        Unit = new UnitDropdownDTO{ UnitNo = o.Key.UnitNo },
                        PostingDate = o.Key.PostingDate,
                        TotalAmount = o.Key.TotalAmount,
                        TotalFee = o.Key.TotalFee,
                    }).ToList() ?? new List<PostingGLHeader>();

                foreach (var header in headerList)
                {
                    var PostingGLDetaliList = model.Where(o => o.DocumentKey == header.DocumentKey)
                        .Select(o => new PostingGLDetail
                        {
                            PostGLType = o.PostGLType,
                            GLAccount = o.GLAccountCode,
                            GLAccountName = o.GLAccountName,
                            Amount = o.Amount
                        }).ToList() ?? new List<PostingGLDetail>();

                    header.PostingGLDetails = PostingGLDetaliList;

                    header.Remark = ValidatePostingGLData(PostingGLDetaliList);

                    if (header.Remark == "")
                        DocumentKey.Add(header.DocumentKey);
                    else
                        header.hasError = true;

                    PostingGLHeaderList.Add(header);
                }

                return PostingGLHeaderList;
            }
            else
            {
                return new List<PostingGLHeader>();
            }
        }

        public static string ValidatePostingGLData(List<PostingGLDetail> model)
        {
            string remark = "";

            var SumDRAmount = model.Where(o => o.PostGLType == "DR").Sum(o => o.Amount);
            var SumCRAmount = model.Where(o => o.PostGLType == "CR").Sum(o => o.Amount);

            if (SumDRAmount != SumCRAmount)
                remark = "DR/CR ไม่เท่ากัน ";

            var NotHasAccount = model.Where(o => (o.GLAccount ?? "") == "").FirstOrDefault();

            if (NotHasAccount != null)
                remark = (string.IsNullOrEmpty(remark) ? "" : ", ") + (NotHasAccount?.GLAccountName ?? "") + "ยังไม่ได้ผูกคู่บัญชี ";

            return remark;
        }

        public static async Task ValidateAsync(Guid CompanyID, DateTime PostingDateFrom, DateTime PostingDateTo, DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            var chkCalendar = await db.CalendarLocks.Include(o => o.Company)
                .Where(o => o.Company.ID == CompanyID
                    && (o.LockDate.Date >= PostingDateFrom.Date && o.LockDate.Date <= PostingDateTo.Date)).FirstOrDefaultAsync() ?? new CalendarLock();
            
            bool IsLockCalendar = chkCalendar.IsLocked;

            /* ** วันที่ปิดบัญชี ** */
            if (IsLockCalendar)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0091").FirstAsync();
                string CompanyName = string.Format("{0} - {1}", chkCalendar.Company.Code, chkCalendar.Company.NameTH);
                string strReceiveDate = chkCalendar.LockDate.ToString("dd/MM/yyyy");
                var msg = errMsg.Message.Replace("[company]", CompanyName);
                msg = msg.Replace("[date]", strReceiveDate);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static PostToSAP CreatePostGLFromPostGLTemps(List<PostGLTemp> model, DatabaseContext db)
        {
            var PostToSAP = new PostToSAP { PostGLHeader = new List<PostGLHeader>(), PostGLDetail = new List<PostGLDetail>() };

            var headerList = model
              .GroupBy(o => new
              {
                  o.DocumentKey,
                  o.DocumentText,
                  o.PostGLDocumentType,
                  o.PostGLDocumentTypeMasterCenterID,
                  o.Company,
                  o.DocumentDate,
                  o.PostingDate,
                  o.ReferentID,
                  o.ReferentType,
                  o.TotalAmount,
                  o.TotalFee,
                  o.DocType,
                  o.Description
              }).Select(async o => new PostGLHeader
              {
                  IsDeleted = false,
                  PostGLDocumentTypeMasterCenterID = o.Key.PostGLDocumentTypeMasterCenterID,
                  DocumentNo = (o.Key.PostGLDocumentType.Name == "PI" || o.Key.PostGLDocumentType.Name == "UN") ? o.Key.DocumentText : await db.GetNewPostGLDocumentAsync(o.Key.PostGLDocumentType.Name, o.Key.Company.SAPCompanyID, o.Key.PostingDate.Year, o.Key.PostingDate.Month),
                  CompanyID = o.Key.Company?.ID,
                  DocumentDate = o.Key.DocumentDate,
                  PostingDate = o.Key.PostingDate,
                  ReferentID = o.Key.DocumentKey,
                  ReferentType = o.Key.ReferentType,
                  TotalAmount = o.Key.TotalAmount,
                  Fee = o.Key.TotalFee,
                  IsCancel = false,
                  DocType = o.Key.DocType,
                  Description = o.Key.Description
              }).Select(o => o.Result).ToList() ?? new List<PostGLHeader>();

            // var results = data.Select(async o => await BudgetPromotionDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            PostToSAP.PostGLHeader = headerList;

            foreach (var header in headerList)
            {
                if (header.ReferentType.Equals("RV")) // check cancel ใบเสร็จ
                { 
                    var payment = db.Payments.Where(o => o.ID == header.ReferentID).FirstOrDefault();
                    if (payment != null && payment.IsCancel == true)
                    {
                        ValidateException ex = new ValidateException();
                        ex.AddError("ERR9999", $"ใบเสร็จ {payment.ReceiptTempNo} ถูกยกเลิกโดยการเงินแล้ว\nโปรดตรวจสอบรายการใหม่อีกครั้ง", 1);
                        throw ex;
                    }
                }
                var DetailList = model.Where(o => o.DocumentKey == header.ReferentID)
                      .Select(o => new PostGLDetail
                      {
                          IsDeleted = false,
                          PostGLHeaderID = header.ID,
                          PostingKey = o.PostingKey,
                          GLAccountID = o.GLAccountID,
                          AccountCode = o.GLAccountCode,
                          FormatTextFileID = o.FormatTextFileID,
                          Amount = o.Amount,
                          BookingID = o.BookingID,
                          ValueDate = o.ValueDate,
                          WBSNumber = o.WBSNumber,
                          ProfitCenter = o.ProfitCenter,
                          CostCenter = o.CostCenter,
                          Quantity = o.Quantity,
                          Unit = o.UnitNo,
                          Assignment = o.Assignment,
                          ProjectNo = o.ProjectNo,
                          TaxCode = o.TaxCode,
                          UnitNo = o.UnitNo,
                          ObjectNumber = o.ObjectNumber,
                          CustomerName = o.CustomerName,
                          Street = o.Street,
                          City = o.City,
                          PostCode = o.PostCode,
                          Country = o.Country,
                          PostGLType = o.PostGLType,
                          TaxID = o.TaxID,
                          AccountName = o.GLAccountName,
                          QuotationID = o.QuotationID
                      }
                    ).ToList() ?? new List<PostGLDetail>();

                PostToSAP.PostGLDetail.AddRange(DetailList);
            }

            return PostToSAP;
        }
    }

    public class PostingGLHeader
    {
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// เลขรายการที่โพส
        /// </summary>
        public Guid DocumentKey { get; set; }

        /// <summary>
        /// เลขรายการอ้างอิง (ReceiveNo, DepositNo, UnknownPaymentNo, CA ???)
        /// </summary>
        public string DocumentText { get; set; }

        /// <summary>
        /// ประเภท Doc RV,JV,PI,CA
        /// </summary>
        public MasterCenterDropdownDTO PostGLDocumentType { get; set; }

        /// <summary>
        /// รายละเอียด
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// วันที่ Posting date
        /// </summary>
        public DateTime PostingDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่ Post
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal TotalFee { get; set; }

        /// <summary>
        /// รายละเอียด Credit,Debit
        /// </summary>
        public List<PostingGLDetail> PostingGLDetails { get; set; }

        public bool? hasError { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

    }

    public class PostingGLDetail
    {      
        /// <summary>
        /// ประเภทรายการ Credit/Debit
        /// </summary>
        public string PostGLType { get; set; }

        /// <summary>
        /// เลขที่ GL
        /// </summary>
        public string GLAccount { get; set; }

        /// <summary>
        /// ชื่อ GL
        /// </summary>
        public string GLAccountName { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }
    }

    public class PostToSAP
    {
        public List<PostGLHeader> PostGLHeader { get; set; }

        public List<PostGLDetail> PostGLDetail { get; set; }
    }
}