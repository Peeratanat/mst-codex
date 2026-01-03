using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
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
using Database.Models.USR;
using Database.Models.DbQueries.FIN;
using Database.Models.MasterKeys;

namespace Base.DTOs.FIN
{
    public class FETDTO : BaseDTO
    {
        /// <summary>
        /// PaymentMethod
        /// </summary>
        public PaymentMethodDTO PaymentMethod { get; set; }

        /// <summary>
        /// ID ข้อมูลการรับชำระเงินโอนต่างประเทศ
        /// </summary>
        //public PaymentForeignBankTransferDTO PaymentForeignBankTransfer { get; set; }

        /// <summary>
        /// ID ข้อมูลการรับชำระเงินบัตรเครดิต
        /// </summary>
        //public PaymentCreditCardDTO PaymentCreditCard { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public Guid? BookingID { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// จำนวนเงินที่ขอ FET
        /// </summary>
        [Description("จำนวนเงินที่ขอ FET")]
        public decimal FETAmount { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        public string Remark { get; set; }

        /// <summary>
        /// สถานะการขอ FET
        /// </summary>
        [Description("สถานะการขอ FET")]
        public MasterCenterDTO FETStatus { get; set; }

        /// <summary>
        /// File แนบ Credit Advice
        /// </summary>
        public FileDTO AttachFile { get; set; }

        // <summary>
        // หมายเหตุ
        // </summary>
        public string CancelRemark { get; set; }

        /// <summary>
        /// User ตีเอกสารกลับไปให้ LC
        /// </summary>
        public USR.UserDTO RejectByUserID { get; set; }

        /// <summary>
        /// วันที่ การตีเอกสารกลับไปให้ LC
        /// </summary>
        public DateTime? RejectDate { get; set; }

        // <summary>
        // เพื่อส่งกลับหาโครงการ
        // </summary>
        [Description("เพื่อส่งกลับหาโครงการ")]
        public string RejectRemark { get; set; }

        // <summary>
        // ส่งกลับหาโครงการ
        // </summary>
        [Description("ส่งกลับหาโครงการ")]
        public bool IsReject { get; set; }

        // <summary>
        // โอนสิทธิ์ใช้ไม่ได้
        // </summary>
        [Description("โอนสิทธิ์ใช้ไม่ได้")]
        public bool IsCancel { get; set; }

        public UnitDropdownDTO Unit { get; set; }
        public ProjectDropdownDTO Project { get; set; }

        public int? CountFET { get; set; }
        public int? countUnit { get; set; }
        public decimal? SumAmountFET { get; set; }

        public User PaymentUserOwner { get; set; }

        //public DepositHeader DepositHeader { get; set; }

        // <DepositNo>
        // เลขที่นำฝาก
        // </DepositNo>
        public string DepositNo { get; set; }

        // <ReceiptTempNo>
        // วันที่ใบเสร็จ
        // </ReceiptTempNo>
        public string ReceiptTempNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public MasterCenterDropdownDTO FETRequesterMasterCenter { get; set; }

        public MasterCenterDropdownDTO FETStatusMasterCenter { get; set; }

        public MasterCenterDropdownDTO PaymentMethodTypeMasterCenter { get; set; }

        public BankDropdownDTO Bank { get; set; }

        //public CancelRemark CancelRemark { get; set; }

        //-----------------------------------------------FETQueryResult----------------------------------------------------------------------------------------------------

        public class FETQueryResult
        {
            public FET FET { get; set; }
            public Booking Booking { get; set; }
            public Unit Unit { get; set; }
            public Project Project { get; set; }

            public PaymentMethod PaymentMethod { get; set; }

            public Payment Payment { get; set; }
            public Company Company { get; set; }

            public DepositHeader DepositHeader { get; set; }

            public MasterCenter FETRequesterMasterCenter { get; set; }
            public MasterCenter FETStatusMasterCenter { get; set; }
            public MasterCenter PaymentMethodTypeMasterCenter { get; set; }

            public Bank Bank { get; set; }
            public User UpdatedBy { get; set; }
        }

        public static FETDTO CreateFromQuery(dbqFETDetailListSP model, DatabaseContext db)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO();
                result.Id = model.ID;
                result.PaymentMethod = new PaymentMethodDTO();

                result.PaymentMethod.Id = model.PaymentMethodID;
                result.PaymentMethod.IR = model.IR;
                result.PaymentMethod.Bank = new BankDropdownDTO {Id = model.BankConID??Guid.Empty,NameEN=model.BankConName };
                result.PaymentMethod.ForeignTransferType = new MasterCenterDropdownDTO {Id =model.ForeignTransferTypeID??Guid.Empty, Name = model.ForeignTransferType };
                result.BookingID = model.BookingID;
                result.FETAmount = model.Amount;
                result.Remark = model.Remark;
                result.FETStatus = new MasterCenterDTO();
                result.FETStatus.Id = model.FETStatusID;
                result.FETStatus.Name = model.FETStatusName;
                result.FETStatus.Key = model.FETStatusKey;
                result.CancelRemark = model.CancelRemark; 
                result.RejectRemark = model.RejectRemark;
                result.IsReject = model.IsReject;
                result.IsCancel = model.IsCancel; 
                result.Unit = new UnitDropdownDTO();
                result.Unit.Id = model.UnitID ?? new Guid();
                result.Unit.UnitNo = model.UnitNo;
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID;
                result.Project.ProjectNameTH = model.ProjectName;

                result.Bank = new BankDropdownDTO();
                result.Bank.Id = model.BankID ?? new Guid();
                result.Bank.NameEN = model.BankName;
                result.Bank.NameTH = model.BankName;
                result.DepositNo = model.DepositNo;

                result.CustomerName = model.CustomerName;

                // result.PaymentUserOwner = model.Payment?.CreatedBy ?? model.FET.CreatedBy,

                result.ReceiptTempNo = model.ReceiptTempNo;
                result.ReceiveDate = model.ReceiveDate;

                result.FETRequesterMasterCenter = new MasterCenterDropdownDTO();
                result.FETRequesterMasterCenter.Id = model.FETRequesterID ?? new Guid();
                result.FETRequesterMasterCenter.Name = model.FETRequesterName;
                result.FETRequesterMasterCenter.Key = model.FETRequesterKey;
                result.FETStatusMasterCenter = new MasterCenterDropdownDTO();
                result.FETStatusMasterCenter.Id = model.FETStatusID?? new Guid();
                result.FETStatusMasterCenter.Name = model.FETStatusName;
                result.FETStatusMasterCenter.Key = model.FETStatusKey;
                result.PaymentMethodTypeMasterCenter = new MasterCenterDropdownDTO();
                result.PaymentMethodTypeMasterCenter.Id = model.PaymentMethodTypeID?? new Guid();
                result.PaymentMethodTypeMasterCenter.Name = model.PaymentMethodTypeName;
                result.PaymentMethodTypeMasterCenter.Key = model.PaymentMethodTypeKey;
                result.Updated = model.UpdatedDate;
                result.UpdatedBy = model.UpdatedBy;
 
                result.AttachFile = new FileDTO();
                result.AttachFile.Name = model.AttachFileName;
                result.AttachFile.Url = model.AttachFile;
                 
                return result;
            }
            else
            {
                return null;
            }
        }

        //public class FETQueryResultViewProject
        //{
        //    public FET FET { get; set; }
        //    public Project Project { get; set; }
        //    public int? countFET { get; set; }
        //    public int? countUnit { get; set; }
        //    public int? countAmountFET { get; set; }
        //}

        public static FETDTO CreateProjectListModel(dbqFETProjectListSP model )
        {
            if (model != null)
            {
                FETDTO result = new FETDTO();
                result.Project = new ProjectDropdownDTO();
                result.Project.Id = model.ProjectID;
                result.Project.ProjectNameTH = model.ProjectNameTH;
                result.Project.ProjectNo = model.ProjectNo;
                result.countUnit = model.countUnit;
                result.CountFET = model.countFET;
                result.SumAmountFET = model.SumAmountFET; 
                return result;
            }
            else
            {
                return null;
            }
        }
        public static FETDTO CreateUnitListModel(dbqFETUnitListSP model)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO();  
                result.Unit = new UnitDropdownDTO();
                result.Unit.UnitNo = model.UnitNo;
                result.Unit.Id = model.UnitID??new Guid();
                result.CustomerName = model.CustomerName;
                result.BookingID = model.BookingID; 
                result.SumAmountFET = model.SumAmountFET;
                result.CountFET = model.CountFET;
                return result;
            }
            else
            {
                return null;
            }
        }


        public class FETQueryResultViewUnit
        {
            public Unit Unit { get; set; }

            public Booking Booking { get; set; }

            public BookingOwner BKOwner { get; set; }

            public Agreement Agreement { get; set; }

            public AgreementOwner AGOwner { get; set; }

        }

        public static async Task<FETDTO> CreateUnitListModel(FETQueryResultViewUnit model, DatabaseContext db)
        {
            if (model != null)
            {
                FETDTO result = new FETDTO();

                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);

                result.CustomerName = model.AGOwner?.Agreement == null ? string.Format("{0} {1}", model.BKOwner?.FirstNameEN, model.BKOwner?.LastNameEN) : string.Format("{0} {1}", model.AGOwner?.FirstNameEN, model.AGOwner?.LastNameEN);

                var paymentMothodModel = await (from o in db.FETs
                                                    .Include(o => o.Booking)
                                                    .ThenInclude(o => o.Unit)
                                                    .Where(o => o.Booking.Unit.ID == model.Unit.ID && o.Booking.IsCancelled == false)

                                                //join pcc in db.PaymentCreditCards on o.ReferentGUID equals pcc.ID into pccData
                                                //from pccModel in pccData.DefaultIfEmpty()

                                                //join pfbt in db.PaymentForeignBankTransfers on o.ReferentGUID equals pfbt.ID into pfbtData
                                                //from pfbtModel in pfbtData.DefaultIfEmpty()

                                                //from p in db.PaymentMethods
                                                //.Where(p => p.ID == ((pccModel != null) ? pccModel.PaymentMethodID : (pfbtModel ?? new PaymentForeignBankTransfer()).PaymentMethodID))
                                                //     .Include(p => p.Payment)
                                                //     .Include(p => p.PaymentMethodType)

                                                join pm in db.PaymentMethods on o.PaymentMethodID equals pm.ID into pmData
                                                from pmModel in pmData.DefaultIfEmpty()

                                                select new { 
                                                    FETUnit = o.Booking.Unit, 
                                                    PaymentMethod = pmModel 
                                                }).ToListAsync();

                result.CountFET = paymentMothodModel.Select(o => o.FETUnit).Distinct().Count();

                result.SumAmountFET = paymentMothodModel.Select(o => o.PaymentMethod.PayAmount).Sum();

                return result;
            }
            else
            {
                return null;
            }
        }

        //----------------------------------------------FETQueryResultViewUnit-----------------------------------------------------------------------------------------------------
        public static void SortBy(FETSortByParam sortByParam, ref IQueryable<FETQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FETSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case FETSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Payment.ReceiveDate);
                        else query = query.OrderByDescending(o => o.Payment.ReceiveDate);
                        break;
                    case FETSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case FETSortBy.FETNumber:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.FETStatusMasterCenterID);
                        else query = query.OrderByDescending(o => o.FET.FETStatusMasterCenterID);
                        break;
                    case FETSortBy.FETAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
                        else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
                        break;
                    case FETSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.Company);
                        else query = query.OrderByDescending(o => o.Project.Company);
                        break;
                    case FETSortBy.DepositCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositHeader.DepositNo);
                        else query = query.OrderByDescending(o => o.DepositHeader.DepositNo);
                        break;
                    case FETSortBy.ReceiptAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
                        else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
                        break;
                    case FETSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.Updated);
                        else query = query.OrderByDescending(o => o.FET.Updated);
                        break;
                    case FETSortBy.UpdateByuser:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FET.UpdatedByUserID);
                        else query = query.OrderByDescending(o => o.FET.UpdatedByUserID);
                        break;

                    default:
                        query = query.OrderByDescending(o => o.FET.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.FET.Created);
            }
        }

        public static void SortProjectListBy(FETSortProjectListByParam sortByParam, ref List<FETDTO> model)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FETSortProjectListBy.Project:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Project.Id).ToList();
                        else model = model.OrderByDescending(o => o.Project.Id).ToList();
                        break;

                    case FETSortProjectListBy.countFET:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.CountFET).ToList();
                        else model = model.OrderByDescending(o => o.CountFET).ToList();
                        break;

                    case FETSortProjectListBy.countUnit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.countUnit).ToList();
                        else model = model.OrderByDescending(o => o.countUnit).ToList();
                        break;

                    case FETSortProjectListBy.countAmountFET:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.SumAmountFET).ToList();
                        else model = model.OrderByDescending(o => o.SumAmountFET).ToList();
                        break;
                    default:
                        model = model.OrderBy(o => o.Project.ProjectNo).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Project.ProjectNo).ToList();
            }
        }

        public static void SortUnitListBy(FETSortUnitListByParam sortByParam, ref List<FETDTO> model)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    //TODO : Kim ฝากตรวจ
                    case FETSortUnitListBy.UnitNo: // case FETSortUnitListBy.countUnit:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.countUnit).ToList();
                        else model = model.OrderByDescending(o => o.countUnit).ToList();
                        break;

                    //case FETSortUnitListBy.countAmountFET: //case FETSortUnitListBy.countAmountFET:
                    //    if (sortByParam.Ascending) model = model.OrderBy(o => o.SumAmountFET).ToList();
                    //    else model = model.OrderByDescending(o => o.SumAmountFET).ToList();
                    //    break;
                    default:
                        model = model.OrderBy(o => o.Unit).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.Unit.UnitNo).ToList();
            }
        }

        public void ToModel(ref FET model)
        {
            model.FETRequesterMasterCenterID = FETRequesterMasterCenter.Id;
            //model.ProjectID = Project.Id;
            model.PaymentMethodID = PaymentMethod.Id;
            model.BookingID = BookingID;
            model.CustomerName = CustomerName;

            model.Amount = FETAmount;

            model.Remark = Remark;
           // model.IsCancel = IsCancel;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (FETAmount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = GetType().GetProperty(nameof(FETAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateStatusToCompleteAsync(DatabaseContext db,FET model)
        {
            ValidateException ex = new ValidateException();
            if (!model.FETStatus.Key.Equals(FETStatusKeys.Print) && model.FETRequester.Key.Equals(FETRequesterKeys.AP))
            { 
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstAsync();
                string desc = GetType().GetProperty(nameof(FETStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateCancelPrintAsync(DatabaseContext db, FET model)
        {
            ValidateException ex = new ValidateException();
            if (!model.FETStatus.Key.Equals(FETStatusKeys.Print))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstAsync();
                string desc = GetType().GetProperty(nameof(FETStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
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
