using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitMeterListDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลแปลง
        /// </summary>
        public UnitDTO Unit { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnerShipDate { get; set; }
        /// <summary>
        /// เลขที่มิเตอร์ไฟฟ้า
        /// </summary>
        public string ElectricMeter { get; set; }
        /// <summary>
        /// เลขที่มิเตอร์น้ำ
        /// </summary>
        public string WaterMeter { get; set; }
        /// <summary>
        /// วันที่เอกสารครบ
        /// </summary>
        public DateTime? CompletedDocumentDate { get; set; }
        /// <summary>
        /// สถานะมิเตอร์ไฟฟ้า
        /// Master/api/MasterCenters?masterCenterGroupKey=MeterStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO ElectricMeterStatus { get; set; }
        /// <summary>
        /// สถานะมิเตอร์น้ำ
        /// Master/api/MasterCenters?masterCenterGroupKey=MeterStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO WaterMeterStatus { get; set; }

        public static UnitMeterListDTO CreateFromModel(Unit model)
        {
            if (model != null)
            {
                var result = new UnitMeterListDTO()
                {
                    Unit = UnitDTO.CreateFromModel(model),
                    TransferOwnerShipDate = model.TransferOwnerShipDate,
                    ElectricMeter = model.ElectricMeter,
                    WaterMeter = model.WaterMeter,
                    CompletedDocumentDate = model.CompletedDocumentDate,
                    ElectricMeterStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ElectrictMeterStatus),
                    WaterMeterStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.WaterMeterStatus),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static UnitMeterListDTO CreateFromQueryResult(UnitMeterListQueryResult model)
        {
            if (model != null)
            {
                var result = new UnitMeterListDTO()
                {
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    TransferOwnerShipDate=model.Unit.TransferOwnerShipDate,
                    ElectricMeter=model.Unit.ElectricMeter,
                    WaterMeter=model.Unit.WaterMeter,
                    CompletedDocumentDate=model.Unit.CompletedDocumentDate,
                    ElectricMeterStatus=MST.MasterCenterDropdownDTO.CreateFromModel(model.Unit.ElectrictMeterStatus),
                    WaterMeterStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.Unit.WaterMeterStatus),
                    Updated = model.Unit.Updated,
                    UpdatedBy = model.Unit.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class ProjectUnitMeterListDTO
    {
        public ProjectDTO Project { get; set; }
        public UnitMeterListDTO UnitMeter { get; set; }

        public static void SortBy(UnitMeterListSortByParam sortByParam, ref IQueryable<UnitMeterListQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitMeterListSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case UnitMeterListSortBy.Unit_UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitMeterListSortBy.Unit_HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Unit.HouseNo);
                        break;
                    case UnitMeterListSortBy.Unit_Model_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.Model.NameTH);
                        else query = query.OrderByDescending(o => o.Unit.Model.NameTH);
                        break;
                    case UnitMeterListSortBy.Unit_UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    case UnitMeterListSortBy.TransferOwnerShipDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.TransferOwnerShipDate);
                        else query = query.OrderByDescending(o => o.Unit.TransferOwnerShipDate);
                        break;
                    case UnitMeterListSortBy.ElectricMeter:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.ElectricMeter);
                        else query = query.OrderByDescending(o => o.Unit.ElectricMeter);
                        break;
                    case UnitMeterListSortBy.WaterMeter:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.WaterMeter);
                        else query = query.OrderByDescending(o => o.Unit.WaterMeter);
                        break;
                    case UnitMeterListSortBy.CompletedDocumentDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.CompletedDocumentDate);
                        else query = query.OrderByDescending(o => o.Unit.CompletedDocumentDate);
                        break;
                    case UnitMeterListSortBy.ElectricMeterStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.ElectrictMeterStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.ElectrictMeterStatus.Name);
                        break;
                    case UnitMeterListSortBy.WaterMeterStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.WaterMeterStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.WaterMeterStatus.Name);
                        break;
                    case UnitMeterListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.Unit.UpdatedBy.DisplayName);
                        break;
                    case UnitMeterListSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.Updated);
                        else query = query.OrderByDescending(o => o.Unit.Updated);
                        break;
                    default:
                        query = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo);
            }
        }

        public static void SortBy(UnitMeterListSortByParam sortByParam, ref List<UnitMeterListQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitMeterListSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo).ToList();
                        else query = query.OrderByDescending(o => o.Project.ProjectNo).ToList();
                        break;
                    case UnitMeterListSortBy.Unit_UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo).ToList();
                        else query = query.OrderByDescending(o => o.Unit.UnitNo).ToList();
                        break;
                    case UnitMeterListSortBy.Unit_HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo).ToList();
                        else query = query.OrderByDescending(o => o.Unit.HouseNo).ToList();
                        break;
                    case UnitMeterListSortBy.Unit_Model_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.Model.NameTH).ToList();
                        else query = query.OrderByDescending(o => o.Unit.Model.NameTH).ToList();
                        break;
                    case UnitMeterListSortBy.Unit_UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name).ToList();
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name).ToList();
                        break;
                    case UnitMeterListSortBy.TransferOwnerShipDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.TransferOwnerShipDate).ToList();
                        else query = query.OrderByDescending(o => o.Unit.TransferOwnerShipDate).ToList();
                        break;
                    case UnitMeterListSortBy.ElectricMeter:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.ElectricMeter).ToList();
                        else query = query.OrderByDescending(o => o.Unit.ElectricMeter).ToList();
                        break;
                    case UnitMeterListSortBy.WaterMeter:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.WaterMeter).ToList();
                        else query = query.OrderByDescending(o => o.Unit.WaterMeter).ToList();
                        break;
                    case UnitMeterListSortBy.CompletedDocumentDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.CompletedDocumentDate).ToList();
                        else query = query.OrderByDescending(o => o.Unit.CompletedDocumentDate).ToList();
                        break;
                    case UnitMeterListSortBy.ElectricMeterStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.ElectrictMeterStatus.Name).ToList();
                        else query = query.OrderByDescending(o => o.Unit.ElectrictMeterStatus.Name).ToList();
                        break;
                    case UnitMeterListSortBy.WaterMeterStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.WaterMeterStatus.Name).ToList();
                        else query = query.OrderByDescending(o => o.Unit.WaterMeterStatus.Name).ToList();
                        break;
                    case UnitMeterListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UpdatedBy.DisplayName).ToList();
                        else query = query.OrderByDescending(o => o.Unit.UpdatedBy.DisplayName).ToList();
                        break;
                    case UnitMeterListSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.Updated).ToList();
                        else query = query.OrderByDescending(o => o.Unit.Updated).ToList();
                        break;
                    default:
                        query = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit?.UnitNo).ToList();
            }
        }
    }

    public class UnitMeterListQueryResult
    {
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public User UpdatedBy { get; set; }
    }
}