using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class BillPaymentHeaderDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ BatchID
        /// </summary>
        [Description("เลขที่ BatchID")]
        public string BatchID { get; set; }

        /// <summary>
        /// วันที่เงินเข้า/วันที่ชำระเงิน
        /// </summary>
        [Description("วันที่เงินเข้า/วันที่ชำระเงิน")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// วันที่โหลด
        /// </summary>
        [Description("วันที่โหลด")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// ชื่อผู้โหลด
        /// </summary>
        [Description("ชื่อผู้โหลด")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร 
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// ช่วงเลขที่ BatchID ของ Detail
        /// </summary>
        [Description("เลขที่ เริ่มต้น-สิ้นสุด")]
        public string BatchRange { get; set; }

        /// <summary>
        /// จำนวนเงินรวม
        /// </summary>
        [Description("จำนวนเงินรวม")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// จำนวนรายการ
        /// </summary>
        [Description("จำนวนรายการ")]
        public int TotalRecord { get; set; }

        /// <summary>
        /// จำนวนรายการที่สำเร็จ
        /// </summary>
        [Description("จำนวนรายการที่สำเร็จ")]
        public int TotalRecordDone { get; set; }

        /// <summary>
        /// จำนวนรายการที่ไม่พบ
        /// </summary>
        [Description("จำนวนรายการที่ไม่พบ")]
        public int TotalRecordWiat { get; set; }

        /// <summary>
        /// ข้อมูลรายละเอียด
        /// </summary>
        [Description("จำนวนรายการที่ไม่พบ")]
        public BillPaymentDetailDTO BillPaymentDetails { get; set; }

        [Description("ชื่อ Text file ที่ Import")]

        public string ImportFileName { get; set; }
        [Description("file ที่ Import")]
        public FileDTO fileImport { get; set; }

        public List<PaymentMethodDTO> PaymentMethod { get; set; }

        public class BillPaymentQueryResult
        {
            public BillPayment BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            //public Company Company { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public BillPaymentDetail BillPaymentDetail { get; set; }
            public Agreement Agreement { get; set; }
            public PaymentMethod PaymentMethods { get; set; }
        }

        public class BillPaymentQueryListResult
        {
            public BillPayment BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public List<BillPaymentDetail> BillPaymentDetail { get; set; }
            public Agreement Agreement { get; set; }
            public List<PaymentMethod> PaymentMethods { get; set; }
            public string strAgreementNo { get; set; }
            public string strUnitNo { get; set; }
            public string strProject { get; set; }
            public string strProjectID { get; set; }
        }

        public class BillPaymentDetailQueryResult
        {
            public BillPayment BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            public Company Company { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public BillPaymentDetail BillPaymentDetail { get; set; }
            public Agreement Agreement { get; set; }
            public PaymentMethod PaymentMethods { get; set; }
            public string strAgreementNo { get; set; }
            public string strUnitNo { get; set; }
            public string strProject { get; set; }
            public string strUnit { get; set; }

            public string strProjectID { get; set; }
        }

        public class BillPaymentTampQueryResult
        {
            public BillPaymentTemp BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public BillPaymentDetailTemp BillPaymentDetail { get; set; }

            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
        }

        public class importBillPaymentQueryResult
        {
            public Booking Booking { get; set; }
            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public Transfer Transfer { get; set; }
        }

        public static void SortBy(BillPaymentHeaderSortByParam sortByParam, ref IQueryable<BillPayment> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case BillPaymentSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BatchID);
                            else query = query.OrderByDescending(o => o.BatchID);
                            break;
                        case BillPaymentSortBy.Bank:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Bank.Alias);
                            else query = query.OrderByDescending(o => o.BankAccount.Bank.Alias);
                            break;

                        case BillPaymentSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiveDate);
                            else query = query.OrderByDescending(o => o.ReceiveDate);
                            break;
                        case BillPaymentSortBy.CreateDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Created);
                            else query = query.OrderByDescending(o => o.Created);
                            break;
                        case BillPaymentSortBy.TotalRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.TotalRecord);
                            else query = query.OrderByDescending(o => o.TotalRecord);
                            break;
                        case BillPaymentSortBy.TotalSuccessRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.TotalSuccessRecord);
                            else query = query.OrderByDescending(o => o.TotalSuccessRecord);
                            break;
                        case BillPaymentSortBy.TotalWatingRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.TotalInprogressRecord);
                            else query = query.OrderByDescending(o => o.TotalInprogressRecord);
                            break;
                        case BillPaymentSortBy.TotalAmount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.TotalAmount);
                            else query = query.OrderByDescending(o => o.TotalAmount);
                            break;
                        case BillPaymentSortBy.ImportBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.CreatedBy.DisplayName);
                            else query = query.OrderByDescending(o => o.CreatedBy.DisplayName);
                            break;
                        case BillPaymentSortBy.Company:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Company.SAPCompanyID);
                            else query = query.OrderByDescending(o => o.BankAccount.Company.SAPCompanyID);
                            break;
                        case BillPaymentSortBy.BankAccount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.DisplayName);
                            else query = query.OrderByDescending(o => o.BankAccount.DisplayName);
                            break;
                        default:
                            query = query.OrderByDescending(o => o.Created);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(o => o.Created);
                }
            }
        }

        public static void SortByDetail(BillPaymentDetailSortByParam sortByParam, ref List<BillPaymentHeaderDTO> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case BillPaymentWaitingSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.ReceiveDate).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.ReceiveDate).ToList();
                            break;
                        case BillPaymentWaitingSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.CustomerName).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.CustomerName).ToList();
                            break;
                        case BillPaymentWaitingSortBy.BankRef1:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.BankRef1).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.BankRef1).ToList();
                            break;
                        case BillPaymentWaitingSortBy.BankRef2:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.BankRef2).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.BankRef2).ToList();
                            break;
                        case BillPaymentWaitingSortBy.BankRef3:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.BankRef3).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.BankRef3).ToList();
                            break;
                        case BillPaymentWaitingSortBy.AgreementNO:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.strAgreementNo).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.strAgreementNo).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Project:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.strProject).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.strProject).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Unit:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.strUnit).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.strUnit).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Amount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.PayAmount).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.PayAmount).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Status:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.BillPaymentStatus.Name).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.BillPaymentStatus.Name).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Period:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.Period).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.Period).ToList();
                            break;
                        case BillPaymentWaitingSortBy.ReceiptTempNo:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.ReceiptTempNo).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.ReceiptTempNo).ToList();
                            break;
                        case BillPaymentWaitingSortBy.IsWrongAccount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.IsWrongAccount).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.IsWrongAccount).ToList();
                            break;
                        case BillPaymentWaitingSortBy.PaymentItemName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.PaymentItemName).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.PaymentItemName).ToList();
                            break;
                        case BillPaymentWaitingSortBy.Company:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.Company.SAPCompanyID).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.Company.SAPCompanyID).ToList();
                            break;
                        case BillPaymentWaitingSortBy.PayAmountDetail:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetails.PayAmountDetail).ToList();
                            else query = query.OrderByDescending(o => o.BillPaymentDetails.PayAmountDetail).ToList();
                            break;
                        default:
                            query.OrderBy(o => o.BillPaymentDetails.strProject).ThenBy(x => x.BillPaymentDetails.strUnit).ToList();
                            break;
                    }
                }
                else
                {
                    query = query.OrderBy(o => o.BillPaymentDetails.strProject).ThenBy(x=>x.BillPaymentDetails.strUnit).ToList();
                }
            }
        }

        public static BillPaymentHeaderDTO CreateFromModel(BillPaymentQueryResult model, List<PaymentMethod> PaymentMethod, DatabaseContext DB, bool chkPaymentBillPayments = false)
        {
            if (model != null)
            {
                BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
                {
                    Id = model.BillPaymentHeader.ID,
                    BatchID = model.BillPaymentHeader.BatchID,
                    ReceiveDate = model.BillPaymentHeader.ReceiveDate,
                    CreateDate = model.BillPaymentHeader.Created,
                    CreatedBy = model.BillPaymentHeader.CreatedBy != null ? model.BillPaymentHeader?.CreatedBy?.DisplayName : null,
                    Updated = model.BillPaymentHeader.Updated,
                    Company = CompanyDropdownDTO.CreateFromModel(model.BillPaymentHeader.BankAccount?.Company),
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    TotalAmount = model.BillPaymentHeader.TotalAmount,
                    TotalRecord = model.BillPaymentHeader.TotalRecord,
                    TotalRecordDone = model.BillPaymentHeader.TotalSuccessRecord,
                    TotalRecordWiat = model.BillPaymentHeader.TotalInprogressRecord,
                    BillPaymentDetails = new BillPaymentDetailDTO(),
                    PaymentMethod = new List<PaymentMethodDTO>(),
                    ImportFileName = model.BillPaymentHeader.ImportFileName
                };

                if (model.BillPaymentDetail != null)
                {
                    result.BillPaymentDetails = BillPaymentDetailDTO.CreateFromModel(model.BillPaymentDetail, DB);

                    if (chkPaymentBillPayments)
                    {
                        var PaymentBillPaymentModel = PaymentMethod.Where(x => x.BillPaymentDetailID == model.BillPaymentDetail.ID).ToList() ?? null;
                        if (PaymentBillPaymentModel.Select(x => x.Payment.Booking) != null)
                        {
                            result.PaymentMethod = PaymentBillPaymentModel.Select(x => PaymentMethodDTO.CreateFromModel(model.PaymentMethods)).ToList();
                        }
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BillPaymentHeaderDTO CreateFromModelList(BillPayment model)
        {
            if (model != null)
            {
                BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
                {
                    Id = model.ID,
                    BatchID = model.BatchID,
                    ReceiveDate = model.ReceiveDate,
                    CreateDate = model.Created,
                    CreatedBy = model.CreatedBy != null ? model.CreatedBy?.DisplayName : null,
                    Updated = model.Updated,
                    Company = CompanyDropdownDTO.CreateFromModel(model.BankAccount?.Company),
                    Bank = BankDropdownDTO.CreateFromModel(model.BankAccount?.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    TotalAmount = model.TotalAmount,
                    TotalRecord = model.TotalRecord,
                    TotalRecordDone = model.TotalSuccessRecord,
                    TotalRecordWiat = model.TotalInprogressRecord,
                    BillPaymentDetails = new BillPaymentDetailDTO(),
                    PaymentMethod = new List<PaymentMethodDTO>(),
                    ImportFileName = model.ImportFileName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static BillPaymentHeaderDTO CreateFromModelTamp(BillPaymentTampQueryResult model, DatabaseContext DB, DbQueryContext dbQuery)
        {
            if (model != null)
            {
                BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
                {
                    Id = model.BillPaymentHeader.ID,
                    BatchID = model.BillPaymentHeader.BatchID,
                    CreateDate = model.BillPaymentHeader.Created,
                    CreatedBy = model.BillPaymentHeader.CreatedBy.DisplayName,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Company = CompanyDropdownDTO.CreateFromModel(model.BankAccount?.Company),
                    TotalAmount = model.BillPaymentHeader.TotalAmount,
                    TotalRecord = model.BillPaymentHeader.TotalRecord,
                    TotalRecordDone = model.countDone,
                    TotalRecordWiat = model.countWiat,
                    BillPaymentDetails = new BillPaymentDetailDTO(),
                    ReceiveDate = model.BillPaymentHeader.ReceiveDate
                };

                if (model.BillPaymentDetail != null)
                    result.BillPaymentDetails = BillPaymentDetailDTO.CreateFromModelTamp(model.BillPaymentDetail, model.Agreement, DB, dbQuery);
            
                return result;
            }
            else
            {
                return null;
            }
        }

        public static BillPayment ToModel(BillPaymentTemp model)
        {
            if (model != null)
            {
                BillPayment result = new BillPayment()
                {
                    ID = model.ID,
                    BatchID = model.BatchID,
                    BankAccountID = model.BankAccountID,
                    ImportFileName = model.ID.ToString() ,
                    BillPaymentImportTypeMasterCenterID = model.BillPaymentImportTypeMasterCenterID,
                    TotalAmount = model.TotalAmount,
                    TotalRecord = model.TotalRecord,
                    ReceiveDate = model.ReceiveDate,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public class listRef
        {
            public string Ref1 { get; set; }
            public string Ref2 { get; set; }
            public string Ref3 { get; set; }
            public DateTime Date { get; set; }
            public string CustomerName { get; set; }
            public decimal Amount { get; set; }
            public string PayType { get; set; }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            var BillPaymentDetail = new BillPaymentDetailDTO();
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();

            var masterCenterModel = db.MasterCenters.Where(o => o.ID == this.BillPaymentDetails.BillPaymentStatus.Id).ToList() ?? new List<MasterCenter>();
            if (!masterCenterModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.BillPaymentStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var Bank = await db.BankAccounts.Where(o => o.ID == this.BankAccount.Id).ToListAsync() ?? new List<BankAccount>();
            if (!Bank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //var ChkBooking = await db.Bookings.Where(o => this.BillPaymentDetails.Booking.Id == o.ID).FirstOrDefaultAsync() ?? null;
            //if (ChkBooking == null)

            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.Booking)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            if (this.BillPaymentDetails.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.BillPaymentDetails.DetailBatchID == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.PayAmount <= 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.PayAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.CustomerName == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.CustomerName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateTempAsync(DatabaseContext db)
        {
            var BillPaymentDetail = new BillPaymentDetailDTO();
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();

            var masterCenterModel = await db.MasterCenters.Where(o => o.ID == this.BillPaymentDetails.BillPaymentStatus.Id).ToListAsync() ?? new List<MasterCenter>();
            if (!masterCenterModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.BillPaymentStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var Bank = await db.BankAccounts.Where(o => o.ID == this.BankAccount.Id).ToListAsync() ?? new List<BankAccount>();
            if (!Bank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.BillPaymentDetails.DetailBatchID == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.PayAmount <= 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.PayAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.CustomerName == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.CustomerName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}