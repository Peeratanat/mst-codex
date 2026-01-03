using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.PRJ
{
    public class RoomPlanDetailDTO : BaseDTO
    {
        //public UnitDetailDTO unit { get; set; }
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
        public FileDTO FloorPlanFile { get; set; }
        /// <summary>
        /// Room Plan
        /// </summary>
        public FileDTO RoomPlanFile { get; set; }
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

        public static async Task<RoomPlanDetailDTO> CreateFromModelAsync(Unit model, FileHelper fileHelper, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new RoomPlanDetailDTO();

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

                var floorplan = (await db.FloorPlanImages.FirstOrDefaultAsync(o => o.Name == model.FloorPlanFileName))?.FileName;
                var roomplan = (await db.RoomPlanImages.FirstOrDefaultAsync(o => o.Name == model.RoomPlanFileName))?.FileName;

                if(!string.IsNullOrEmpty(floorplan))
                    result.FloorPlanFile = await FileDTO.CreateFromBucketandFileNameAsync("projects", floorplan, fileHelper);
                if (!string.IsNullOrEmpty(roomplan))
                    result.RoomPlanFile = await FileDTO.CreateFromBucketandFileNameAsync("projects", roomplan, fileHelper);

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


    }
}
