using Database.Models.PRJ;
using System;
namespace Base.DTOs.PRJ
{
    public class ProjectDataStatusDTO
    {
        /// <summary>
        /// Project ID
        /// </summary>
        public Guid ProjectID { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProjectStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลทั่วไปของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO GeneralDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลเอกสารสัญญาของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO AgreementDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลแบบบ้านของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลตึกของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO TowerDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลแปลงของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลโฉนดของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO TitleDeedDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลจัดการรูปของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO PictureDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลMinPriceของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลPriceListของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceListDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลค่าธรรมเนียมโอนของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO TransferFeeDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลBudgetPromotionของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO BudgetProDataStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectDataStatus
        /// สถานะข้อมูลWaiveของโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO WaiveDataStatus { get; set; }

        public static ProjectDataStatusDTO CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectDataStatusDTO()
                {
                    ProjectID = model.ID,
                    GeneralDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.GeneralDataStatus),
                    AgreementDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.AgreementDataStatus),
                    ModelDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelDataStatus),
                    TowerDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TowerDataStatus),
                    UnitDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitDataStatus),
                    TitleDeedDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TitleDeedDataStatus),
                    PictureDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PictureDataStatus),
                    MinPriceDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceDataStatus),
                    PriceListDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PriceListDataStatus),
                    TransferFeeDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferFeeDataStatus),
                    BudgetProDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BudgetProDataStatus),
                    WaiveDataStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.WaiveDataStatus),
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
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
