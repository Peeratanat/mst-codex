using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.SAL;
using Database.Models.PRJ;
using System;
using System.ComponentModel;
using System.Linq;
using Base.DTOs.USR;
using Base.DTOs.SAL;

using static Base.DTOs.FIN.BillPaymentHeaderDTO;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Database.Models.MST;
using Database.Models.MasterKeys;
using static Database.Models.DbQueries.DBQueryParam;
using Microsoft.Data.SqlClient;
using Database.Models.DbQueries;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.FIN
{
    public class BillPaymentDetailDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ BatchID ของ Detail
        /// </summary>
        [Description("เลขที่ BatchID ของ Detail")]
        public string DetailBatchID { get; set; }

        /// <summary>
        /// วันที่ลูกค้าชำระเงิน 
        /// </summary>
        [Description("วันที่ลูกค้าชำระเงิน")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 1
        /// </summary>
        [Description("Referenct Code จากธนาคาร 1")]
        public string BankRef1 { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 2
        /// </summary>
        [Description("Referenct Code จากธนาคาร 2")]
        public string BankRef2 { get; set; }

        /// <summary>
        /// Referenct Code จากธนาคาร 3
        /// </summary>
        [Description("Referenct Code จากธนาคาร 3")]
        public string BankRef3 { get; set; }

        /// <summary>
        /// การชำระ
        /// </summary>
        [Description("การชำระ")]
        public string PayType { get; set; }

        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        [Description("จำนวนเงินที่จ่าย")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// Booking ที่ลูกค้าชำระ Bill Payment
        /// </summary>
        [Description("Booking ที่ลูกค้าชำระ Bill Payment")]
        public SAL.BookingDTO Booking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Description("สถานะ Bill Payment")]
        public MST.MasterCenterDropdownDTO BillPaymentStatus { get; set; }

        /// <summary>
        /// วันที่-เวลา Reconcile หรือ confirm รายการ
        /// </summary>
        [Description("วันที่-เวลา Reconcile หรือ confirm รายการ")]
        public DateTime? ReconcileDate { get; set; }

        /// <summary>
        /// กลุ่มเหตุผลการยกเลิก
        /// </summary>
        [Description("กลุ่มเหตุผลการยกเลิก")]
        public MST.MasterCenterDropdownDTO DeleteReason { get; set; }

        /// <summary>
        /// หมายเหตุการยกเลิก
        /// </summary>
        [Description("หมายเหตุการยกเลิก")]
        public string Remark { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public List<ProjectDropdownDTO> Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public List<UnitDropdownDTO> Unit { get; set; }

        /// <summary>
        /// ผู้บันทึก
        /// </summary>
        [Description("ผู้บันทึก")]
        public UserDTO CreatedBy { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        [Description("ผู้กลับรายการ")]
        public UserDTO ReversedBy { get; set; }

        /// <summary>
        /// ชำระเงินค่าอะไร 
        /// </summary>
        [Description("ชำระเงินค่าอะไร")]
        public string PayAmountDetail { get; set; }

        /// <summary>
        /// CancelRemark
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        /// <summary>
        /// หมายเหตุรายการด้าน SAP
        /// </summary>
        [Description("หมายเหตุรายการด้าน SAP")]
        public string SAPRemark { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        [Description("สัญญา")]
        public List<AgreementDropdownDTO> Agreement { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

        /// <summary>
        /// รวมเลข AgreementNo
        /// </summary>
        [Description("รวมเลข AgreementNo")]
        public string strAgreementNo { get; set; }
        /// <summary>
        /// รวมชื่อโครงการ
        /// </summary>
        [Description("รวมชื่อโครงการ")]
        public string strProject { get; set; }

        /// <summary>
        /// รวมชื่อโครงการ
        /// </summary>
        [Description("รวมชื่อโครงการ")]
        public string strProjectID { get; set; }
        /// <summary>
        /// รวมชื่อแปลง
        /// </summary>
        [Description("รวมชื่อแปลง")]
        public string strUnit { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// ชำระค่า
        /// </summary>
        [Description("ชำระค่า")]
        public string PaymentItemName { get; set; }

        //ลำดับงวดที่ชำระเงิน
        [Description("ลำดับงวดที่ชำระเงิน")]
        public string Period { get; set; }

        public static BillPaymentDetailDTO CreateFromModel(BillPaymentDetail model, DatabaseContext DB)
        {
            if (model != null)
            {
                BillPaymentDetailDTO result = new BillPaymentDetailDTO()
                {
                    Id = model.ID,
                    ReceiveDate = model.ReceiveDate,
                    BankRef1 = model.BankRef1,
                    BankRef2 = model.BankRef2,
                    BankRef3 = model.BankRef3,
                    PayType = model.PayType,
                    PayAmount = model.PayAmount,
                    ReconcileDate = model.ReconcileDate,
                    Remark = model.Remark,
                    BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                    DetailBatchID = model.DetailBatchID,
                    DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                    CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    CustomerName = model.CustomerName.Replace("  "," "),
                    CancelRemark = model.Remark,
                    IsWrongAccount = model.IsWrongAccount,

                    Agreement = new List<AgreementDropdownDTO>(),
                    Project = new List<ProjectDropdownDTO>(),
                    Unit = new List<UnitDropdownDTO>(),
                    BankAccount = new BankAccountDropdownDTO(),
                    Bank = new BankDropdownDTO(),
                    Booking = new BookingDTO {Id = model.BookingID }
                };
                if (model.BillPaymentStatus.Key.Equals(BillPaymentStatusKey.Split) || model.BillPaymentStatus.Key.Equals(BillPaymentStatusKey.Complete))
                {
                    IQueryable<QueryResult> Query = from o in DB.PaymentMethods.Where(x => x.BillPaymentDetailID == result.Id && x.Payment.IsCancel == false)
                            .Include(x => x.Payment)
                                .ThenInclude(x => x.Booking)
                                    .ThenInclude(x => x.Unit)
                                        .ThenInclude(x => x.Project)
                                            .ThenInclude(x => x.Company)

                                                    join AgreementData in DB.Agreements on o.Payment.BookingID equals AgreementData.BookingID into AgreementGroup
                                                    from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                        //join BankAccData in DB.BankAccounts on o.Payment.Booking.Unit.Project.CompanyID equals BankAccData.CompanyID into BankAccGroup
                                                        //from BankAccModel in BankAccGroup.DefaultIfEmpty()

                                                        //join BankData in DB.Banks on BankAccModel.BankID equals BankData.ID into BankGroup
                                                        //from BankModel in BankGroup.DefaultIfEmpty()

                                                    join PaymentItemData in DB.PaymentItems.Include(x => x.MasterPriceItem) on o.PaymentID equals PaymentItemData.PaymentID into PaymentItemGroup
                                                    from PaymentItemModel in PaymentItemGroup.DefaultIfEmpty()

                                                    select new QueryResult
                                                    {
                                                        PaymentBillPayment = o,
                                                        Agreement = AgreementModel ?? new Agreement(),
                                                        //BankAccount = BankAccModel,
                                                        //Bank = BankModel,
                                                        PaymentItem = PaymentItemModel ?? new PaymentItem(),
                                                        Unit = o.Payment.Booking.Unit,
                                                        Project = o.Payment.Booking.Unit.Project
                                                    };

                    var QueryResult = Query.ToList();

                    if (QueryResult.Count > 0)
                    {
                        result.strAgreementNo = string.Join(",", QueryResult.GroupBy(x => new { x.Agreement.ID, x.PaymentBillPayment.Payment.BookingID }).Select(o => o.FirstOrDefault().Agreement.AgreementNo ?? o.FirstOrDefault().PaymentBillPayment.Payment.Booking.BookingNo).ToList());

                        result.strProject = string.Join(",", QueryResult.GroupBy(x => x.Project.ID).Select(o => o.FirstOrDefault().Project.ProjectNo + " - " + o.FirstOrDefault().Project.ProjectNameTH).ToList());
                        result.strUnit = string.Join(",", QueryResult.GroupBy(x => x.Unit.ID).Select(o => o.FirstOrDefault().Unit.UnitNo).ToList());
                        result.strProjectID = string.Join(",", QueryResult.Select(o => o.Project.ID).ToList());

                        result.ReceiptTempNo = string.Join(",", QueryResult.GroupBy(o => o.PaymentBillPayment.Payment.ReceiptTempNo).Select(x => x.Key).ToList());
                         
                        var PayAmountDetail = QueryResult.Where(x => x.PaymentItem.MasterPriceItem?.Key != MasterPriceItemKeys.InstallmentAmount).Select(o => o.PaymentItem.MasterPriceItem?.Detail).ToList();
                        result.PaymentItemName = string.Join(",", QueryResult.GroupBy(o => o.PaymentBillPayment.Payment.PaymentItemName).Select(x => x.Key).ToList());
                        result.Period = string.Join(",", QueryResult.Select(o => o.PaymentItem.Period).ToList());

                        result.Project = QueryResult.Select(o => ProjectDropdownDTO.CreateFromModel(o.PaymentBillPayment.Payment.Booking.Unit.Project)).ToList();
                        result.Unit = QueryResult.Select(o => UnitDropdownDTO.CreateFromModel(o.PaymentBillPayment.Payment.Booking.Unit)).ToList();
                        result.Agreement = QueryResult.Select(o => AgreementDropdownDTO.CreateFromModel(o.Agreement)).ToList();
                        //result.BankAccount = BankAccountDropdownDTO.CreateFromModel(QueryResult.Select(x => x.BankAccount).FirstOrDefault());
                        //result.Bank = BankDropdownDTO.CreateFromModel(QueryResult.Select(x => x.Bank).FirstOrDefault());
                        //result.PayAmountDetail = result.PayAmountDetail;
                    }
                }
                else if (model.BookingID != null && model.BookingID != Guid.Empty)
                {
                    IQueryable<QueryTampResult> QueryBooking = from o in DB.Bookings.Where(x => x.ID == model.BookingID)
                               .Include(x => x.Unit)
                                   .ThenInclude(x => x.Project)
                                       .ThenInclude(x => x.Company)

                                                               join AgreementData in DB.Agreements on o.ID equals AgreementData.BookingID into AgreementGroup
                                                               from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                               select new QueryTampResult
                                                               {
                                                                   Booking = o,
                                                                   Agreement = AgreementModel ?? new Agreement(),
                                                                   Unit = o.Unit,
                                                                   Project = o.Project
                                                               };
                    var QueryTolist = QueryBooking.ToList();
                    if (QueryTolist.Count > 0)
                    {
                        result.strAgreementNo = string.Join(",", QueryTolist.Select(o => o.Agreement.AgreementNo ?? o.Booking.BookingNo).ToList());
                        result.strProject = string.Join(",", QueryTolist.Select(o => o.Project.ProjectNo + " - " + o.Project.ProjectNameTH).ToList());
                        result.strUnit = string.Join(",", QueryTolist.Select(o => o.Unit.UnitNo).ToList());
                        result.strProjectID = string.Join(",", QueryTolist.Select(o => o.Project.ID).ToList());

                        result.Project = QueryTolist.Select(o => ProjectDropdownDTO.CreateFromModel(o.Project)).ToList();
                        result.Unit = QueryTolist.Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToList();
                        result.Agreement = QueryTolist.Select(o => AgreementDropdownDTO.CreateFromModel(o.Agreement)).ToList();
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }
       
        public class QueryResult
        {
            public PaymentMethod PaymentBillPayment { get; set; }
            public Agreement Agreement { get; set; }
            public BankAccount BankAccount { get; set; }
            public Bank Bank { get; set; }
            public PaymentItem PaymentItem { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }
        }

        public class QueryTampResult
        {
            public Booking Booking { get; set; }
            public Agreement Agreement { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }
        }

        public static BillPaymentDetailDTO CreateFromModelTamp(BillPaymentDetailTemp model, Agreement Agreement, DatabaseContext DB , DbQueryContext dbQuery)
        {
            if (model != null)
            {
                BillPaymentDetailDTO result = new BillPaymentDetailDTO();
                if (model.Booking == null)
                {
                    BillPaymentDetailDTO genDTO = new BillPaymentDetailDTO()
                    {
                        Id = model.ID,
                        ReceiveDate = model.ReceiveDate,
                        BankRef1 = model.BankRef1,
                        BankRef2 = model.BankRef2,
                        BankRef3 = model.BankRef3,
                        PayType = model.PayType,
                        PayAmount = model.PayAmount,

                        ReconcileDate = model.ReconcileDate,
                        Remark = model.Remark,
                        BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        DetailBatchID = model.DetailBatchID,
                        DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                        CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                        UpdatedBy = model.UpdatedBy.DisplayName,
                        CustomerName = model.CustomerName.Replace("  ", " "),

                        Agreement = new List<AgreementDropdownDTO>(),
                        Booking = new BookingDTO(),
                        Project = new List<ProjectDropdownDTO>(),
                        Unit = new List<UnitDropdownDTO>(),
                        Company = new CompanyDropdownDTO(),

                        CancelRemark = model.Remark,
                        IsWrongAccount = model.IsWrongAccount
                    };

                    result = genDTO;
                }
                else
                {
                    BillPaymentDetailDTO genDTO = new BillPaymentDetailDTO()
                    {
                        Id = model.ID,
                        ReceiveDate = model.ReceiveDate,
                        BankRef1 = model.BankRef1,
                        BankRef2 = model.BankRef2,
                        BankRef3 = model.BankRef3,
                        PayType = model.PayType,
                        PayAmount = model.PayAmount,
                        ReconcileDate = model.ReconcileDate,
                        Remark = model.Remark,
                        BillPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentStatus),
                        DetailBatchID = model.DetailBatchID,
                        DeleteReason = MasterCenterDropdownDTO.CreateFromModel(model.BillPaymentDeleteReason),
                        CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                        UpdatedBy = model.UpdatedBy.DisplayName,
                        CustomerName = model.CustomerName.Replace("  ", " "),
                        CancelRemark = model.Remark,
                        IsWrongAccount = model.IsWrongAccount,
                        Agreement = new List<AgreementDropdownDTO>(),
                        Booking = new BookingDTO { Id = model.BookingID },
                        Project = new List<ProjectDropdownDTO>(),
                        Unit = new List<UnitDropdownDTO>(),
                        Company = new CompanyDropdownDTO(),
                    }; 
                    //result.PaymentItemName = string.Join(",", QueryTolist.Select(o => o.).ToList());
                    IQueryable<QueryTampResult> Query = from o in DB.Bookings.Where(x => x.ID == model.BookingID)
                               .Include(x => x.Unit)
                                   .ThenInclude(x => x.Project)
                                       .ThenInclude(x => x.Company)

                                                    join AgreementData in DB.Agreements on o.ID equals AgreementData.BookingID into AgreementGroup
                                                    from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                    select new QueryTampResult
                                                    {
                                                        Booking = o ,
                                                        Agreement = AgreementModel ?? new Agreement(),
                                                        Unit = o.Unit,
                                                        Project = o.Project
                                                    };
                    var QueryTolist = Query.ToList();
                    if (QueryTolist.Count > 0)
                    {
                        genDTO.strAgreementNo = string.Join(",", QueryTolist.Select(o => o.Agreement.AgreementNo ?? o.Booking.BookingNo).ToList());
                        genDTO.strProject = string.Join(",", QueryTolist.Select(o => o.Project.ProjectNo + " - " + o.Project.ProjectNameTH).ToList());
                        genDTO.strUnit = string.Join(",", QueryTolist.Select(o => o.Unit.UnitNo).ToList());
                        genDTO.strProjectID = string.Join(",", QueryTolist.Select(o => o.Project.ID).ToList());
                          
                        genDTO.Project = QueryTolist.Select(o => ProjectDropdownDTO.CreateFromModel(o.Project)).ToList();
                        genDTO.Unit = QueryTolist.Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToList();
                        genDTO.Agreement = QueryTolist.Select(o => AgreementDropdownDTO.CreateFromModel(o.Agreement)).ToList();

                    }
                    var ParamList = new List<dbqParam>() ?? new List<dbqParam>();
                    ParamList.Add(new dbqParam { Key = "@BookingID", sqlparam = new SqlParameter("@prmBookingID", model.Booking.ID) });

                    string strQuery = string.Format("EXEC {0} {1}", DBStoredNames.spPaymentUnitPriceList, string.Join(", ", ParamList.Select(o => string.Format("{0} = {1}", o.Key, o.sqlparam.ParameterName))));

                    var queryPaymentUnit = dbQuery.dbqSPPaymentUnitPriceLists.FromSqlRaw(strQuery, ParamList.Select(o => o.sqlparam).ToArray());
                    var queryResult = queryPaymentUnit.ToList() ?? new List<dbqSPPaymentUnitPriceList>();
                    var RemainUnitPrice = queryResult.Where(x=>x.RemainAmount >0).FirstOrDefault();
                    genDTO.PayAmountDetail = RemainUnitPrice?.Name;
                    result = genDTO;
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BillPaymentDetailSortByParam sortByParam, ref IQueryable<BillPaymentQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case BillPaymentWaitingSortBy.ReceiveDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.ReceiveDate);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.ReceiveDate);
                            break;
                        case BillPaymentWaitingSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.CustomerName);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.CustomerName);
                            break;
                        case BillPaymentWaitingSortBy.BankRef1:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.BankRef1);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.BankRef1);
                            break;
                        case BillPaymentWaitingSortBy.BankRef2:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.BankRef2);
                            else query = query.OrderByDescending(o => o.BillPaymentDetail.BankRef2);
                            break;
                        case BillPaymentWaitingSortBy.AgreementNO:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.AgreementNo);
                            else query = query.OrderByDescending(o => o.Agreement.AgreementNo);
                            break;
                    }
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentDetail.Created);
                    else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                }
                else
                {
                    query = query.OrderBy(o => o.BillPaymentDetail.Created);
                }
            }
        }

        public static BillPaymentDetail ToModel(BillPaymentDetailTemp model, Guid BillPaymentHeaderID)
        {
            if (model != null)
            {
                BillPaymentDetail result = new BillPaymentDetail()
                {
                    ID = model.ID,
                    ReceiveDate = model.ReceiveDate,
                    BankRef1 = model.BankRef1,
                    BankRef2 = model.BankRef2,
                    BankRef3 = model.BankRef3,
                    PayType = model.PayType,
                    PayAmount = model.PayAmount,
                    ReconcileDate = model.ReconcileDate,
                    Remark = model.Remark,
                    BillPaymentStatusMasterCenterID = model.BillPaymentStatusMasterCenterID,
                    DetailBatchID = model.DetailBatchID,
                    BillPaymentHeaderID = BillPaymentHeaderID,
                    IsWrongAccount = model.IsWrongAccount,
                    BillPaymentDeleteReasonMasterCenterID = model.BillPaymentDeleteReasonMasterCenterID,
                    BookingID = model.BookingID,
                    CustomerName = model.CustomerName.Replace("  ", " ")
                };
                return result;
            }
            else
            {
                return null;
            } 
        }
         
    }
}
