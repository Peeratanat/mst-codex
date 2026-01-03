using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class UnitFenceAreaExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _wbsNoIndex = 1;
        public const int _unitNoIndex = 2;
        public const int _fenceAreaIndex = 3;
        public const int _fenceIronAreaIndex = 4;

        public string ProjectNo { get; set; }
        public string WBSNo { get; set; }
        public string UnitNo { get; set; }
        public double FenceArea { get; set; }
        public double FenceIronArea { get; set; }

        public static UnitFenceAreaExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new UnitFenceAreaExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
           
            double fenceArea;
            if (double.TryParse(dr[_fenceAreaIndex]?.ToString(), out fenceArea))
            {
                result.FenceArea = fenceArea;
            }
            double fenceIronArea;
            if (double.TryParse(dr[_fenceAreaIndex]?.ToString(), out fenceIronArea))
            {
                result.FenceIronArea = fenceIronArea;
            }
            return result;
        }
        public void ToModel(ref Unit model)
        {
            model.FenceArea = this.FenceArea;
            model.FenceIronArea = this.FenceIronArea;
        }
    }
}
