using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;
using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using System.Linq;
using Base.DTOs.PRJ;

namespace Base.DTOs.SAL
{
    public class TransferStatusDTO : BaseDTO
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
        /// สถานะแปลง
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        /// วันที่นัดโอนกรรมสิทธิ์
        /// </summary>
        public DateTime? ScheduleTransferDate { get; set; }
        /// <summary>
        /// สถานะเตรียมโอนกรรมสิทธิ์
        /// </summary>
        public bool? IsPrepareTransfer { get; set; }
        /// <summary>
        /// วันที่เตรียมโอนกรรมสิทธิ์
        /// </summary>
        public DateTime? PrepareTransferDate { get; set; }

        /// <summary>
        /// สถานะโฉนด
        /// </summary>
        public MST.MasterCenterDropdownDTO LandStatus { get; set; }

        /// <summary>
        /// สถานะขอปลอด
        /// Master/api/MasterCenters?masterCenterGroupKey=PreferStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PreferStatus { get; set; }

        /// <summary>
        /// ข้อมูลโฉนด
        /// </summary>
        public PRJ.TitleDeedDTO TitleDeed { get; set; }

        /// <summary>
        /// รายการขอเบิกโฉนด
        /// </summary>
        public TitleDeedRequestFlowDTO TitledeedRequestFlow { get; set; }

        /// <summary>
        /// สำนักงานที่ดิน
        /// </summary>
        public LandOfficeDTO LandOffice { get; set; }



        public static TransferStatusDTO CreateFromModel(Unit model, DatabaseContext DB)
        {
            try
            {
                if (model != null)
                {
                    var titledeed = model.TitledeedDetails.FirstOrDefault();

                    var result = new TransferStatusDTO()
                    {
                        Id = model.ID,
                        Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                        Unit = PRJ.UnitDropdownDTO.CreateFromModel(model),
                        UnitStatus = MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus),
                        ScheduleTransferDate = model.ScheduleTransferDate,
                        IsPrepareTransfer = model.IsPrepareTransfer,
                        PrepareTransferDate = model.PrepareTransferDate,
                        LandStatus = MasterCenterDropdownDTO.CreateFromModel(titledeed?.LandStatus),
                        PreferStatus = MasterCenterDropdownDTO.CreateFromModel(titledeed?.PreferStatus),
                        TitleDeed = TitleDeedDTO.CreateFromModel(titledeed),
                        //TitledeedRequestFlow = TitleDeedRequestFlowDTO.CreateFromModel(titledeed.TitledeedRequestFlow),
                        
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

        public static TransferStatusDTO CreateFromQueryResult(TransferStatusQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                //var booking = DB.TitledeedRequestFlows
                //    .Include(o => o.Booking)            
                //    .Where(o => o.Booking.ID == model.TitledeedRequestFlow.BookingID && o.Booking.IsCancelled == true).FirstOrDefault();
                var result = new TransferStatusDTO()
                {
                    
                    Id = model.Unit.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    UnitStatus = MasterCenterDropdownDTO.CreateFromModel(model.Unit.UnitStatus),
                    //ScheduleTransferDate = model.Unit.ScheduleTransferDate ?? model.Transfer?.ScheduleTransferDate,
                    //ScheduleTransferDate = model.Transfer?.ScheduleTransferDate ?? model.TitledeedRequestFlow?.ScheduleTransferDate,
                    ScheduleTransferDate = model.Transfer?.ScheduleTransferDate ?? model.TitledeedRequestFlow?.ScheduleTransferDate,

                    IsPrepareTransfer = model.Unit.IsPrepareTransfer,
                    PrepareTransferDate = model.Unit.PrepareTransferDate,
                    LandStatus = MasterCenterDropdownDTO.CreateFromModel(model.LandStatus),
                    PreferStatus = MasterCenterDropdownDTO.CreateFromModel(model.PreferStatus),
                    TitleDeed = TitleDeedDTO.CreateFromModel(model.TitleDeed),
                    TitledeedRequestFlow = TitleDeedRequestFlowDTO.CreateFromModel(model.TitledeedRequestFlow),
                    LandOffice = LandOfficeDTO.CreateFromModels(model.LandOffice, DB),

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(TransferStatusListSortByParam sortByParam, ref IQueryable<TransferStatusQueryResult> query)
        {
            IOrderedQueryable<TransferStatusQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TransferStatusListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => o.Unit.UnitNo);
                        break;
                    //case TransferStatusListSortBy.ScheduleTransferDate:
                    //    if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => (o.Unit.ScheduleTransferDate ?? o.Transfer.ScheduleTransferDate));
                    //    else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => (o.Unit.ScheduleTransferDate ?? o.Transfer.ScheduleTransferDate));
                    //    break;
                    case TransferStatusListSortBy.PrepareTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.PrepareTransferDate);
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => o.Unit.PrepareTransferDate);
                        break;
                    case TransferStatusListSortBy.UnitStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        break;
                    case TransferStatusListSortBy.IsPrepareTransfer:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.IsPrepareTransfer);
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => o.Unit.IsPrepareTransfer);
                        break;
                    case TransferStatusListSortBy.LandStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LandStatus.Name).ThenBy(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        else orderQuery = query.OrderBy(o => o.LandStatus.Name).ThenByDescending(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        break;
                    case TransferStatusListSortBy.PreferStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreferStatus.Name).ThenBy(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        else orderQuery = query.OrderBy(o => o.PreferStatus.Name).ThenByDescending(o => o.Unit.UnitStatus != null ? o.Unit.UnitStatus.Name : "");
                        break;
                    case TransferStatusListSortBy.LandStatusDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => (o.TitleDeed.LandStatusDate ?? o.TitleDeed.LandStatusDate));
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => (o.TitleDeed.LandStatusDate ?? o.TitleDeed.LandStatusDate));
                        break;
                    case TransferStatusListSortBy.ScheduleTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => (o.Transfer.ScheduleTransferDate ?? o.TitledeedRequestFlow.ScheduleTransferDate));
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => (o.Transfer.ScheduleTransferDate ?? o.TitledeedRequestFlow.ScheduleTransferDate));
                        break;
                    case TransferStatusListSortBy.HouseNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => (o.Unit.HouseNo));
                        else orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenByDescending(o => (o.Unit.HouseNo));
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
            }

            orderQuery.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
            query = orderQuery;
        }
    }

    public class TransferStatusQueryResult
    {
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Transfer Transfer { get; set; }
        public MasterCenter LandStatus { get; set; }
        public MasterCenter PreferStatus { get; set; }
        public TitledeedDetail TitleDeed { get; set; }
        public TitledeedRequestFlow TitledeedRequestFlow { get; set; }
        public LandOffice LandOffice { get; set; }
        public Address Addresses { get; set; }
    }
}
