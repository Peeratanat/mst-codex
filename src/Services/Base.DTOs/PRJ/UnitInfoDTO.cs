using System;
using System.Linq;
using Database.Models.PRJ;

namespace Base.DTOs.PRJ
{
    public class UnitInfoDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string HouseNo { get; set; }
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
        /// พื้นที่ใช้สอย
        /// </summary>
        public double? UsedArea { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        ///  Project/api/Projects/{projectID}/TitleDeeds
        /// </summary>
        public TitleDeedDropdownDTO TitleDeed { get; set; }
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
        /// ให้ขายต่างชาติได้
        /// </summary>
        public bool IsForeignUnit { get; set; }

        public static UnitInfoDTO CreateFromModel(Unit model)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO();

                result.Id = model.ID;
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.UnitNo = model.UnitNo;
                result.HouseNo = model.HouseNo;
                result.Model = ModelDropdownDTO.CreateFromModel(model.Model);
                result.UnitDirection = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitDirection);
                result.UnitType = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitType);
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);
                result.SaleArea = model.SaleArea;
                result.UsedArea = model.UsedArea;
                result.TitleDeed = TitleDeedDropdownDTO.CreateFromModel(model.TitledeedDetails.FirstOrDefault());

                result.Tower = TowerDropdownDTO.CreateFromModel(model.Tower);
                result.Floor = FloorDropdownDTO.CreateFromModel(model.Floor);
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.Updated = model.Updated;
                result.Position = model.Position;

                result.IsForeignUnit = model.IsForeignUnit;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
