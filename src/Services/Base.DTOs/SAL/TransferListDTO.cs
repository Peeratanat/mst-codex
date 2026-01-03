using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.PRJ;
using Database.Models.MST;
using Database.Models.USR;
using Database.Models.DbQueries.SAL;

namespace Base.DTOs.SAL
{
    public class TransferListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// เลขที่โอน
        /// </summary>
        public string TransferNo { get; set; }
        /// <summary>
        /// ผู้โอน
        /// </summary>
        public TransferOwnerDropdownDTO TransferOwner { get; set; }
        /// <summary>
        /// วิธีรับเงิน
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditBankingType
        /// </summary>
        public MasterCenterDropdownDTO CreditBankingType { get; set; }
        ///<summary>
        ///วันที่นัดโอนกรรมสิทธื์
        ///</summary>
        public DateTime? ScheduleTransferDate { get; set; }
        ///<summary>
        ///วันที่โอนจริง
        ///</summary>
        public DateTime? ActualTransferDate { get; set; }
        /// <summary>
        /// มอบอำนาจหรือไม่?
        /// </summary>
        public bool IsAssignAuthority { get; set; }
        /// <summary>
        /// สถานะสมรส
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=TransferMarriageStatus
        /// </summary>
        public MasterCenterDropdownDTO TransferMarriageStatus { get; set; }
        /// <summary>
        /// สถานะโอนกรรมสิทธิ์
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=TransferStatus
        /// </summary>
        public MasterCenterDropdownDTO TransferStatus { get; set; }

        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ชื่อผู้โอน
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// สถานะขอสินเชื่อ
        /// </summary>
        public string CreditBankingStatusName { get; set; }

        /// <summary>
        /// ยกยอดไปนิติบุคคล
        /// </summary>
        public bool? IsAPBalanceTransfer { get; set; }

        /// <summary>
        /// รวมเงินทอน AP
        /// </summary>
        public decimal? APChangeAmount { get; set; }

        /// <summary>
        /// รวมเงินทอน  (นิติบุคคล)
        /// </summary>
        public decimal? LegalEntityChangeAmount { get; set; }



        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public int? ConfigNumberOfDays { get; set; }
        public int? ConfigIsFullDay { get; set; }
        public int? IsPassHalfDay { get; set; }
        public DateTime? ScheduleDate { get; set; }

        public DateTime? RequestDate { get; set; }
        public DateTime? AddNumbFullDayDate { get; set; }
        public DateTime? MinusNumbDayDate { get; set; }
        public TimeSpan RequestTime { get; set; }
        public string RequestAllDayFlag { get; set; }



        public static TransferListDTO CreateFromModel(Transfer model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    var owner = DB.TransferOwners.Include(o => o.TransferMarriageStatus).Where(o => o.TransferID == model.ID && o.Order == 1).FirstOrDefault();

                    var result = new TransferListDTO()
                    {
                        Id = model.ID,
                        Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                        Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                        TransferNo = model.TransferNo,
                        TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(owner),
                        CreditBankingType = MasterCenterDropdownDTO.CreateFromModel(model.Agreement?.Booking?.CreditBankingType),
                        ScheduleTransferDate = model.ScheduleTransferDate,
                        ActualTransferDate = model.ActualTransferDate,
                        IsAssignAuthority = owner.IsAssignAuthority,
                        TransferMarriageStatus = MasterCenterDropdownDTO.CreateFromModel(owner.TransferMarriageStatus),
                        TransferStatus = MasterCenterDropdownDTO.CreateFromModel(model.TransferStatus),
                        IsAPBalanceTransfer = model.IsAPBalanceTransfer,
                        APChangeAmount = (model.APChangeAmount ?? 0),
                        LegalEntityChangeAmount = (model.LegalEntityChangeAmount ?? 0)

                    };

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return null;
            }
        }

        public static TransferListDTO CreateFromQueryResult(TransferQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                model.AgreementOwner = model.AgreementOwner ?? new AgreementOwner();

                var transferOwner = new TransferOwner();

                if (model.Owner != null)
                {
                    transferOwner = model.Owner;
                }
                else
                {
                    transferOwner.ID = Guid.NewGuid();
                    //transferOwner.Updated = model.AgreementOwner.Updated;
                    //transferOwner.UpdatedBy = model.AgreementOwner.UpdatedBy;
                    transferOwner.FirstNameTH = model.AgreementOwner.FirstNameTH;
                    transferOwner.LastNameTH = model.AgreementOwner.LastNameTH;
                    transferOwner.MiddleNameTH = model.AgreementOwner.MiddleNameTH;
                    //transferOwner.FromContact = model.AgreementOwner.FromContact;
                }

                model.Transfer = model.Transfer ?? new Transfer();
                model.Owner = model.Owner ?? new TransferOwner();
                model.TransferStatus = model.TransferStatus ?? new MasterCenter();
                model.TransferMarriageStatus = model.TransferMarriageStatus ?? new MasterCenter();

                var result = new TransferListDTO()
                {
                    Id = model.Transfer.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    TransferNo = model.Transfer?.TransferNo,
                    TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(transferOwner),
                    CreditBankingType = MasterCenterDropdownDTO.CreateFromModel(model.Agreement?.Booking?.CreditBankingType),
                    ScheduleTransferDate = model.Transfer?.ScheduleTransferDate,
                    ActualTransferDate = model.Transfer?.ActualTransferDate,
                    IsAssignAuthority = transferOwner.IsAssignAuthority,
                    TransferMarriageStatus = MasterCenterDropdownDTO.CreateFromModel(model.TransferMarriageStatus),
                    TransferStatus = MasterCenterDropdownDTO.CreateFromModel(model.TransferStatus),
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    OwnerName = model.OwnerName,
                    CreditBankingStatusName = model.CreditBankingStatusName,
                    IsAPBalanceTransfer = model.Transfer.IsAPBalanceTransfer,
                    APChangeAmount = (model.Transfer.APChangeAmount ?? 0),
                    LegalEntityChangeAmount = (model.Transfer.LegalEntityChangeAmount ?? 0)
                };

                return result;
            }
            else
            {
                return null;
            }
        }



        public static TransferListDTO CreateFromQueryCheckResult(dbqCHECKTitledeedRequestFlowChangeDue model, DatabaseContext DB)
        {
            if (model != null)
            {

                var result = new TransferListDTO()
                {
                    StatusCode = model.StatusCode,
                    StatusMsg = model.StatusMsg,
                    ConfigNumberOfDays = model.ConfigNumberOfDays,
                    ConfigIsFullDay = model.ConfigIsFullDay,
                    IsPassHalfDay = model.IsPassHalfDay,
                    ScheduleDate = model.ScheduleDate,

                    RequestDate = model.RequestDate,
                    AddNumbFullDayDate = model.MinusNumbDayDate,
                    MinusNumbDayDate = model.MinusNumbDayDate,
                    RequestTime = model.RequestTime,
                    RequestAllDayFlag = model.RequestAllDayFlag
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(TransferListSortByParam sortByParam, ref IQueryable<TransferQueryResult> query)
        {
            IOrderedQueryable<TransferQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TransferListSortBy.TransferNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Transfer.TransferNo);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Transfer.TransferNo);
                        break;
                    case TransferListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Unit.UnitNo);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Unit.UnitNo);
                        break;
                    case TransferListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        break;
                    case TransferListSortBy.ScheduleTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Transfer.ScheduleTransferDate);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Transfer.ScheduleTransferDate);
                        break;
                    case TransferListSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Transfer.ActualTransferDate);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Transfer.ActualTransferDate);
                        break;
                    case TransferListSortBy.CreditBankingType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.CreditBankingType != null ? o.CreditBankingType.Name : "");
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.CreditBankingType != null ? o.CreditBankingType.Name : "");
                        break;
                    case TransferListSortBy.TransferStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.TransferStatus != null ? o.TransferStatus.Name : "");
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.TransferStatus != null ? o.TransferStatus.Name : "");
                        break;
                    case TransferListSortBy.MarriageStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.TransferMarriageStatus != null ? o.TransferMarriageStatus.Name : "");
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.TransferMarriageStatus != null ? o.TransferMarriageStatus.Name : "");
                        break;
                    case TransferListSortBy.IsAssignAuthority:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Owner.IsAssignAuthority);
                        else orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Owner.IsAssignAuthority);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenByDescending(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Unit.UnitNo);
            }

            orderQuery.ThenBy(o => o.Transfer.ID);
            query = orderQuery;
        }

        public static TransferListDTO CreateFromQuery(dbqTransferList model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelProject = db.Projects.FirstOrDefault(o => o.ID == model.ProjectID);
                var modelUnit = db.Units.FirstOrDefault(o => o.ID == model.UnitID);
                var modelTransferOwner = db.TransferOwners.FirstOrDefault(o => o.ID == model.TransferOwnerID);
                var modelCreditBankingType = db.MasterCenters.FirstOrDefault(o => o.ID == model.CreditBankingTypeID);
                var modelTransferMarriageStatus = db.MasterCenters.FirstOrDefault(o => o.ID == model.MarriageStatusID);
                var modelTransferStatus = db.MasterCenters.FirstOrDefault(o => o.ID == model.TransferStatusID);
                var modelAgreement = db.Agreements.FirstOrDefault(o => o.ID == model.AgreementID);
                var modelTransfer = db.Transfers.FirstOrDefault(o => o.ID == model.TransferID) ?? new Transfer();

                var result = new TransferListDTO()
                {
                    Id = model.TransferID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(modelProject),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(modelUnit),
                    TransferNo = model.TransferNo,
                    TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(modelTransferOwner),
                    CreditBankingType = MasterCenterDropdownDTO.CreateFromModel(modelCreditBankingType),
                    ScheduleTransferDate = model.ScheduleTransferDate,
                    ActualTransferDate = model.ActualTransferDate,
                    IsAssignAuthority = model.IsAssignAuthority.HasValue ? model.IsAssignAuthority.Value : false,
                    TransferMarriageStatus = MasterCenterDropdownDTO.CreateFromModel(modelTransferMarriageStatus),
                    TransferStatus = MasterCenterDropdownDTO.CreateFromModel(modelTransferStatus),
                    Agreement = AgreementDropdownDTO.CreateFromModel(modelAgreement),
                    OwnerName = model.OwnerName,
                    CreditBankingStatusName = (model.CreditBankingTypeKey == "2" || model.CreditBankingTypeKey == "3") ? model.CreditBankingStatusName + Environment.NewLine + model.BankName : model.CreditBankingStatusName,
                    IsAPBalanceTransfer = modelTransfer.IsAPBalanceTransfer,
                    APChangeAmount = (modelTransfer.APChangeAmount ?? 0),
                    LegalEntityChangeAmount = (modelTransfer.LegalEntityChangeAmount ?? 0)
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class TransferQueryResult
    {
        public Transfer Transfer { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public MasterCenter CreditBankingType { get; set; }
        public MasterCenter TransferStatus { get; set; }
        public MasterCenter TransferMarriageStatus { get; set; }
        public TransferOwner Owner { get; set; }
        public Agreement Agreement { get; set; }
        public string OwnerName { get; set; }
        public AgreementOwner AgreementOwner { get; set; }
        public string CreditBankingStatusName { get; set; }
    }

}
