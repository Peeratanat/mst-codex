using Base.DTOs.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.MST
{
    public class GPBlockDTO : BaseDTO
    {
        public Guid? GPVersionID { get; set; }
        public string WBSBlock { get; set; }
        public string BlockNumber { get; set; }
        public decimal? BudgetCO01 { get; set; }
        public decimal? Budget_CO01_Block { get; set; } 
        public decimal? Budget_COC1_Block { get; set; } 
        public decimal? Budget_CO01_Sum { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }

        public static GPBlockDTO CreateFromResult(List< GPUnitDTO> ListUnit )
        {
            var result = new GPBlockDTO();
            result.Budget_CO01_Block = ListUnit.FirstOrDefault().Budget_CO01_Block;
            result.WBSBlock = ListUnit.FirstOrDefault().WBSBlock;
            result.BlockNumber = ListUnit.FirstOrDefault().BlockNumber;
            result.Budget_CO01_Sum = ListUnit.Sum(o => o.Ori_Budget_CO01);
            var ListUnitStatus = ListUnit.GroupBy(x => x.UnitStatus)
                .Select(x=>(x.Key??"").Replace("TF", "โอน").Replace("AG", "สัญญา").Replace("BK", "จอง").Replace("AV", "ว่าง")).OrderBy(x=>x).ToList(); 
            result.Status =string.Join(",",ListUnitStatus);
            return result;

        }


    }

}
