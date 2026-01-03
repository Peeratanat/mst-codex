using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// ผู้ทำสัญญา
        ///  Sale/api/Bookings/{bookingID}/AgreementOwners/DropdownList
        /// </summary>
        public AgreementOwnerDropdownDTO AgreementOwner { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// สถานะสัญญา
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=AgreementStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO AgreementStatus { get; set; }

        /// <summary>
        /// Key สถานะสัญญา
        /// </summary>
        public string AgreementStatusKey { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่อง
        /// </summary>
        public ChangeAgreementOwnerWorkflowDTO ChangeAgreementOwnerWorkflow { get; set; }
        /// <summary>
        /// สถานะอนุมัติพิมพ์สัญญา
        /// </summary>
        public bool? IsPrintApproved { get; set; }
        /// <summary>
        /// วันที่อนุมัติพิมพ์สัญญา
        /// </summary>
        public DateTime? PrintApprovedDate { get; set; }

        public USR.UserDTO SignContractRequestUser { get; set; }

        /// <summary>
        /// สถานะอนุมัติลงนามสัญญา
        /// </summary>
        public bool? IsApproveSignContract { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// วันที่อนุมัติ Sign Contact
        /// </summary>
        public DateTime? SignContractApproveDate { get; set; }

        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? SignAgreementDate { get; set; }

        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? SignContractApprovedDate { get; set; }

        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? SignContractRequestDate { get; set; }

        /// <summary>
        /// วันที่ชำระเงิน
        /// </summary>
        public DateTime? BookingApprovedDate { get; set; }

        /// <summary>
        /// ผู้ทำสัญญา Fullname
        /// </summary>
        public string FullName { get; set; }
        public bool? IsCombine { get; set; }

        public static AgreementListDTO CreateFromQueryResult(AgreementListQueryResult model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                { 
                    var result = new AgreementListDTO()
                    {
                        Id = model.Agreement.ID,
                        Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                        AgreementNo = model.Agreement.AgreementNo,
                        Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                        AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(model.AgreementOwner),
                        Booking = BookingDropdownDTO.CreateFromModel(model.Agreement.Booking),
                        ContractDate = model.Agreement.SignContractRequestDate,
                        TransferOwnershipDate = model.Agreement.TransferOwnershipDate,
                        AgreementStatus = MasterCenterDropdownDTO.CreateFromModel(model.Agreement.AgreementStatus),
                        ChangeAgreementOwnerWorkflow = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(model.ChangeAgreementOwnerWorkflow, DB),
                        IsPrintApproved = model.Agreement.IsPrintApproved,
                        PrintApprovedDate = model.Agreement.PrintApprovedDate,
                        SignContractRequestUser = model.Agreement.SignContractRequestUser == null ? new USR.UserDTO() : USR.UserDTO.CreateFromModel(model.Agreement.SignContractRequestUser),
                        IsApproveSignContract = model.Agreement.IsSignContractApproved,
                        Remark = model.Agreement.Remark,
                        SignContractApproveDate = model.Agreement.SignContractApprovedDate,
                        SignAgreementDate = model.Agreement.SignAgreementDate,
                        SignContractApprovedDate = model.Agreement.SignContractApprovedDate,
                        SignContractRequestDate = model.Agreement.SignContractRequestDate,
                        BookingApprovedDate = model.Agreement.Booking.ApproveDate,
                        FullName = model.Agreement.MainOwnerName,
                        IsCombine = model.CombineUnit != null ? true : false,
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

        public static AgreementListDTO CreateFromModel(Agreement model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    var owner = DB.AgreementOwners.Where(o => o.AgreementID == model.ID && o.IsMainOwner == true).FirstOrDefault();
                    var changeAgreementOwnerWorkflow = DB.ChangeAgreementOwnerWorkflows.Where(o => o.AgreementID == model.ID).FirstOrDefault();

                    var result = new AgreementListDTO()
                    {
                        Id = model.ID,
                        Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                        AgreementNo = model.AgreementNo,
                        Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                        AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(owner),
                        Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                        ContractDate = model.ContractDate,
                        TransferOwnershipDate = model.TransferOwnershipDate,
                        AgreementStatus = MasterCenterDropdownDTO.CreateFromModel(model.AgreementStatus),
                        ChangeAgreementOwnerWorkflow = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(changeAgreementOwnerWorkflow, DB),
                        IsPrintApproved = model.IsPrintApproved,
                        PrintApprovedDate = model.PrintApprovedDate,
                        SignContractRequestUser = model.SignContractRequestUser == null ? new USR.UserDTO() : USR.UserDTO.CreateFromModel(model.SignContractRequestUser),
                        IsApproveSignContract = model.IsSignContractApproved,
                        Remark = model.Remark,
                        SignAgreementDate = model.SignAgreementDate,
                        SignContractApprovedDate = model.SignContractApprovedDate,
                        SignContractRequestDate = model.SignContractRequestDate,
                        FullName = owner?.FullnameTH
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

        public static void SortBy(AgreementListSortByListSortByParam sortByParam, ref IQueryable<AgreementListQueryResult> query)
        {
            IOrderedQueryable<AgreementListQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgreementListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.Unit.UnitNo);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.Unit.UnitNo);
                        break;
                    case AgreementListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.AgreementOwner.FirstNameTH);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.AgreementOwner.FirstNameTH);
                        break;
                    case AgreementListSortBy.BookingNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.Booking.BookingNo);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.Booking.BookingNo);
                        break;
                    case AgreementListSortBy.AgreementNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.AgreementNo);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.AgreementNo);
                        break;
                    case AgreementListSortBy.AgreementStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.AgreementStatus.Key);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.AgreementStatus.Key);
                        break;
                    case AgreementListSortBy.IsPrintApproved:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.IsPrintApproved);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.IsPrintApproved);
                        break;
                    case AgreementListSortBy.PrintApprovedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.PrintApprovedDate);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.Agreement.PrintApprovedDate);
                        break;
                    case AgreementListSortBy.ChangeAgreementOwnerType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key);
                        break;
                    case AgreementListSortBy.Combine:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.CombineUnit.Created);
                        else orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenByDescending(o => o.CombineUnit.Created);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.Unit.UnitNo).ThenBy(o => o.Agreement.IsPrintApproved);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Agreement.Project.ProjectNo).ThenBy(o => o.Agreement.Unit.UnitNo);
            }

            orderQuery.ThenBy(o => o.Agreement.AgreementNo);
            query = orderQuery;

        }

        public class AgreementListQueryResult
        {
            public Agreement Agreement { get; set; }
            public Unit Unit { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }
            public CombineUnit CombineUnit { get; set; }
            //public MinPriceBudgetWorkflow MinPriceBudgetWorkflow { get; set; }
            //public List<MinPriceBudgetApproval> MinPriceBudgetApprovals { get; set; }

        }
    }
}
