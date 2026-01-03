using System;
using System.Data;
using Database.Models.PRJ;

namespace PRJ_Unit.Services.Excels
{
    public class UnitInitialExcelModel
    {
        public const int _projectSapCodeIndex = 0;
        public const int _wbsNoIndex = 1;
        public const int _wbsObjectCodeIndex = 2;
        public const int _companyIndex = 3;
        public const int _boqStyleIndex = 4;
        public const int _typeOfRealEstateIndex = 5;
        public const int _wbsStatusIndex = 6;
        public const int _areaAmountIndex = 7;
        public const int _saleAreaIndex = 8;
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectSapCode { get; set; }
        /// <summary>
        /// Sap WBS Number
        /// </summary>
        public string SAPWBSNo { get; set; }
        /// <summary>
        /// Sap WBS Object Code
        /// </summary>
        public string SAPWBSObject { get; set; }
        /// <summary>
        /// บริษัท
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// BoqStyle
        /// </summary>
        public string BoqStyle { get; set; }
        /// <summary>
        /// แบบบ้าน
        /// </summary>
        public string TypeOfRealEstate { get; set; }
        /// <summary>
        /// สถานะ Sap WBS
        /// </summary>
        public string SAPWBSStatus { get; set; }
        /// <summary>
        /// AreaAmount
        /// </summary>
        public double AreaAmount { get; set; }
        /// <summary>
        /// พื้นที่ขาย (Unit)
        /// </summary>
        public double SaleArea { get; set; }

        public static UnitInitialExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new UnitInitialExcelModel();
            result.ProjectSapCode = dr[_projectSapCodeIndex]?.ToString();
            result.SAPWBSNo = dr[_wbsNoIndex]?.ToString();
            result.SAPWBSObject = dr[_wbsObjectCodeIndex]?.ToString();
            result.Company = dr[_companyIndex]?.ToString();
            result.BoqStyle = dr[_boqStyleIndex]?.ToString();
            result.TypeOfRealEstate = dr[_typeOfRealEstateIndex]?.ToString();
            result.SAPWBSStatus = dr[_wbsStatusIndex]?.ToString();

            double SaleArea;
            if (double.TryParse(dr[_saleAreaIndex]?.ToString(), out SaleArea))
            {
                result.SaleArea = SaleArea;
            }
            double AreaAmount;
            if (double.TryParse(dr[_areaAmountIndex]?.ToString(), out AreaAmount))
            {
                result.AreaAmount = AreaAmount;
            }
            return result;

        }

        public void ToModel(ref Unit model)
        {
            model.SAPWBSNo = this.SAPWBSNo;
            model.SAPWBSObject = this.SAPWBSObject;
            model.SaleArea = this.SaleArea;
        }

    }
}
