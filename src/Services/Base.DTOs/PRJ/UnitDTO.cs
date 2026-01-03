using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// เลขที่แปลง ที่ทำการ Map เข้ากับทีตั้งโฉนด
        /// </summary>
        public UnitDTO UnitTD { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        [Description("บ้านเลขที่")]
        public string HouseNo { get; set; }
        /// <summary>
        /// ปีที่ได้บ้านเลขที่
        /// </summary>
        public int? HouseNoReceivedYear { get; set; }
        /// <summary>
        /// SAP WBS Object
        /// </summary>
        [Description("SAP WBS Number")]
        public string SapwbsObject { get; set; }
        /// <summary>
        /// SAP WBS Object สำหรับ Budget Promotion
        /// </summary>
        [Description("SAP WBS Object สำหรับ Budget Promotion")]
        public string SapwbsNo { get; set; }
        /// <summary>
        /// แบบบ้าน
        ///  Project/api/Projects/{projectID}/Models/DropdownList
        /// </summary>
        public ModelDropdownDTO Model { get; set; }
        /// <summary>
        /// ทิศ
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitDirection
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitDirection { get; set; }
        /// <summary>
        /// ประเภทแปลง
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitType
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitType { get; set; }
        /// <summary>
        /// สถานะแปลง
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        /// พื้นที่ขาย (พื้นที่ผังขาย)
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        ///  Project/api/Projects/{projectID}/TitleDeeds
        /// </summary>
        public TitleDeedDTO TitleDeed { get; set; }
        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        public double? UsedArea { get; set; }
        /// <summary>
        /// ตึก
        /// Project/api/Projects/{projectID}/Towers/DropdownList
        /// </summary>
        public TowerDropdownDTO Tower { get; set; }
        /// <summary>
        /// ชั้น
        /// Project/api/Projects/{projectID}/Towers/{towerID}/Floors/DropdownList
        /// </summary>
        public FloorDropdownDTO Floor { get; set; }
        /// <summary>
        /// ตำแหน่งห้อง (เฉพาะแนวสูง)
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// จำนวนบุริมสิทธ์
        /// </summary>
        public double? NumberOfPrivilege { get; set; }

        /// <summary>
        /// ราคาบุริมสิทธ์
        /// </summary>
        public decimal? UnitLoanAmount { get; set; }
        /// <summary>
        /// จำนวนที่จอดรถ FIX
        /// </summary>
        public double? NumberOfParkingFix { get; set; }
        /// <summary>
        /// จำนวนที่จอดรถไม่ FIX
        /// </summary>
        public double? NumberOfParkingUnFix { get; set; }
        /// <summary>
        /// Floor Plan
        /// </summary>
        public FloorPlanImageDTO FloorPlan { get; set; }
        /// <summary>
        /// Room Plan
        /// </summary>
        public RoomPlanImageDTO RoomPlan { get; set; }
        /// <summary>
        /// ให้ขายต่างชาติได้
        /// </summary>
        public bool IsForeignUnit { get; set; }
        /// <summary>
        /// SAP WBS Number_P
        /// </summary>
        public string SapwbsNo_P { get; set; }
        /// <summary>
        /// SAP WBS Object_P
        /// </summary>
        public string SapwbsObject_P { get; set; }
        /// <summary>
        /// พื้นที่เพิ่ม-ลด
        /// </summary>
        public double? AddOnArea { get; set; }

        /// <summary>
        /// AssetType
        /// </summary>
        public MST.MasterCenterDropdownDTO AssetType { get; set; }
        /// <summary>
        ///  เปิด-ปิดตามแปลงว่าง
        /// </summary>
        public bool IsAction { get; set; }


        public int? PositionX { get; set; }
        public int? PositionY { get; set; }

        public bool? IsNotSale { get; set; }
        public string RemarkNotSale { get; set; }

        public static UnitDTO CreateFromModel(Unit model, FloorPlanImageDTO floorPlan = null, RoomPlanImageDTO roomPlan = null)
        {
            if (model != null)
            {
                var result = new UnitDTO();

                result.Id = model.ID;
                result.UnitNo = model.UnitNo;
                result.HouseNo = model.HouseNo;
                result.HouseNoReceivedYear = model.HouseNoReceivedYear;
                result.SapwbsObject = model.SAPWBSObject;
                result.SapwbsNo = model.SAPWBSNo;
                result.Model = ModelDropdownDTO.CreateFromModel(model.Model);
                result.UnitDirection = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitDirection);
                result.UnitType = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitType);
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);
                result.SaleArea = model.SaleArea;
                result.TitleDeed = TitleDeedDTO.CreateFromModel(model.TitledeedDetails?.FirstOrDefault());

                //result.UsedArea = (result.TitleDeed?.TitledeedArea != null) ? result.TitleDeed?.UsedArea : model.UsedArea;
                result.UsedArea = model.UsedArea;

                result.Tower = TowerDropdownDTO.CreateFromModel(model.Tower);
                result.Floor = FloorDropdownDTO.CreateFromModel(model.Floor);
                result.NumberOfPrivilege = model.NumberOfPrivilege;
                result.UnitLoanAmount = model.UnitLoanAmount;
                result.NumberOfParkingFix = model.NumberOfParkingFix;
                result.NumberOfParkingUnFix = model.NumberOfParkingUnFix;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.Updated = model.Updated;
                result.Position = model.Position;

                result.FloorPlan = floorPlan;
                result.RoomPlan = roomPlan;

                result.IsForeignUnit = model.IsForeignUnit;
                result.SapwbsNo_P = model.SAPWBSNo_P;
                result.SapwbsObject_P = model.SAPWBSObject_P;
                result.UnitLoanAmount = model.UnitLoanAmount;
                result.AddOnArea = Convert.ToDouble(
                            Convert.ToDecimal((result.TitleDeed?.TitledeedArea ?? 0).ToString())
                            - Convert.ToDecimal((result.SaleArea ?? 0).ToString())
                        );
                if (model.AssetType != null)
                {
                    result.AssetType = MST.MasterCenterDropdownDTO.CreateFromModel(model.AssetType);
                }

                result.IsNotSale = model.IsNotSale;
                result.RemarkNotSale = model.RemarkNotSale;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static List<UnitDTO> CreateListFromModel(List<Unit> model)
        {
            var unitList = new List<UnitDTO>();
            if (model.Count > 0)
            {
                foreach (Unit td in model)
                {
                    var result = new UnitDTO();
                    result.Id = td.ID;
                    result.UnitTD = UnitDTO.CreateFromModel(td);
                    unitList.Add(result);
                }
                return unitList;
            }
            else
            {
                return null;
            }
        }

        public static UnitDTO CreateFromQueryResult(UnitQueryResult model)
        {
            if (model != null)
            {
                var result = new UnitDTO();

                result.Id = model.Unit.ID;
                result.UnitNo = model.Unit.UnitNo;
                result.HouseNo = model.Unit.HouseNo;
                result.HouseNoReceivedYear = model.Unit.HouseNoReceivedYear;
                result.SapwbsObject = model.Unit.SAPWBSObject;
                result.SapwbsNo = model.Unit.SAPWBSNo;
                result.Model = ModelDropdownDTO.CreateFromModel(model.Model);
                result.UnitDirection = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitDirection);
                result.UnitType = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitType);
                result.AssetType = MST.MasterCenterDropdownDTO.CreateFromModel(model.AssetType);
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);
                result.SaleArea = model.Unit.SaleArea;
                result.TitleDeed = TitleDeedDTO.CreateFromModel(model.Unit.TitledeedDetails?.FirstOrDefault());
                result.Tower = TowerDropdownDTO.CreateFromModel(model.Tower);
                result.Floor = FloorDropdownDTO.CreateFromModel(model.Floor);
                result.NumberOfPrivilege = model.Unit.NumberOfPrivilege;
                result.UnitLoanAmount = model.Unit.UnitLoanAmount;
                result.NumberOfParkingFix = model.Unit.NumberOfParkingFix;
                result.NumberOfParkingUnFix = model.Unit.NumberOfParkingUnFix;
                result.Updated = model.Unit.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.IsForeignUnit = model.Unit.IsForeignUnit;
                result.Position = model.Unit.Position;
                result.SapwbsNo_P = model.Unit.SAPWBSNo_P;
                result.SapwbsObject_P = model.Unit.SAPWBSObject_P;
                result.IsAction = UnitDTO.CheckActions(model.UnitStatus);

                return result;
            }
            else
            {
                return null;
            }
        }
        public static bool CheckActions(MasterCenter model) //UnitStatus
        {
            if (model != null)
            {
                if (model.Order > 3)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public static void SortBy(UnitListSortByParam sortByParam, ref IQueryable<UnitQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitListSortBy.HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Unit.HouseNo);
                        break;
                    case UnitListSortBy.Model_Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.Code);
                        else query = query.OrderByDescending(o => o.Model.Code);
                        break;
                    case UnitListSortBy.Model_TypeOfRealEstate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.TypeOfRealEstate.Name);
                        else query = query.OrderByDescending(o => o.Model.TypeOfRealEstate.Name);
                        break;
                    case UnitListSortBy.Model_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.NameTH);
                        else query = query.OrderByDescending(o => o.Model.NameTH);
                        break;
                    case UnitListSortBy.Tower:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerNoTH);
                        else query = query.OrderByDescending(o => o.Tower.TowerNoTH);
                        break;
                    case UnitListSortBy.Floor:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameTH);
                        else query = query.OrderByDescending(o => o.Floor.NameTH);
                        break;
                    case UnitListSortBy.UnitDirection:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitDirection.Name);
                        else query = query.OrderByDescending(o => o.UnitDirection.Name);
                        break;
                    case UnitListSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.UnitStatus.Name);
                        break;
                    case UnitListSortBy.UnitType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitType.Name);
                        else query = query.OrderByDescending(o => o.UnitType.Name);
                        break;
                    case UnitListSortBy.SaleArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.SaleArea);
                        else query = query.OrderByDescending(o => o.Unit.SaleArea);
                        break;
                    case UnitListSortBy.TitleDeed_TitleDeedNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TitledeedDetail.TitledeedNo);
                        else query = query.OrderByDescending(o => o.TitledeedDetail.TitledeedNo);
                        break;
                    case UnitListSortBy.TitleDeed_TitleDeedArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TitledeedDetail.TitledeedArea);
                        else query = query.OrderByDescending(o => o.TitledeedDetail.TitledeedArea);
                        break;
                    case UnitListSortBy.NumberOfPrivilege:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.NumberOfPrivilege);
                        else query = query.OrderByDescending(o => o.Unit.NumberOfPrivilege);
                        break;
                    case UnitListSortBy.NumberOfParkingFix:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.NumberOfParkingFix);
                        else query = query.OrderByDescending(o => o.Unit.NumberOfParkingFix);
                        break;
                    case UnitListSortBy.NumberOfParkingUnFix:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.NumberOfParkingUnFix);
                        else query = query.OrderByDescending(o => o.Unit.NumberOfParkingUnFix);
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(this.HouseNo))
            {
                if (!this.HouseNo.IsOnlyNumberWithSpecialCharacter(true))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(UnitDTO.HouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Unit model)
        {
            model.UnitNo = this.UnitNo;
            model.HouseNo = this.HouseNo;
            model.SAPWBSNo = this.SapwbsNo;
            model.SAPWBSObject = this.SapwbsObject;
            model.HouseNoReceivedYear = this.HouseNoReceivedYear;
            model.ModelID = this.Model?.Id;
            model.FloorPlanFileName = this.FloorPlan?.Name;
            model.RoomPlanFileName = this.RoomPlan?.Name;
            model.UnitDirectionMasterCenterID = this.UnitDirection?.Id;
            model.UnitTypeMasterCenterID = this.UnitType?.Id;
            model.AssetTypeMasterCenterID = this.AssetType?.Id;
            model.UnitStatusMasterCenterID = this.UnitStatus?.Id;
            model.SaleArea = this.SaleArea;
            model.TowerID = this.Tower?.Id;
            model.FloorID = this.Floor?.Id;
            model.NumberOfPrivilege = this.NumberOfPrivilege;
            model.NumberOfParkingFix = this.NumberOfParkingFix;
            model.NumberOfParkingUnFix = this.NumberOfParkingUnFix;
            model.IsForeignUnit = this.IsForeignUnit;
            model.Position = this.Position;
            model.UnitLoanAmount = this.UnitLoanAmount;

            model.IsNotSale = this.IsNotSale;
            model.RemarkNotSale = this.RemarkNotSale;
        }
    }
    public class UnitQueryResult
    {
        public Unit Unit { get; set; }
        public Floor Floor { get; set; }
        public Tower Tower { get; set; }
        public MasterCenter UnitType { get; set; }
        public MasterCenter UnitStatus { get; set; }
        public MasterCenter UnitDirection { get; set; }
        public TitledeedDetail TitledeedDetail { get; set; }
        public Model Model { get; set; }

        public MasterCenter AssetType { get; set; }
        public User UpdatedBy { get; set; }

    }
}
