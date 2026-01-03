using Base.DTOs.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.USR;
using System;
using System.Linq;

namespace Base.DTOs.MST
{
    public class GPProjectDTO : BaseDTO
    {
        public Guid? GPVersionID { get; set; }
        public decimal? BudgetLI { get; set; }
        public decimal? BudgetCO01 { get; set; }
        public decimal? BudgetCOC1 { get; set; }
        public decimal? BudgetCOA1 { get; set; }
        public decimal? BudgetUT { get; set; }
        public decimal? BudgetAC { get; set; }
        public decimal? PercentGPCommit { get; set; }
        public decimal? PriceCommit { get; set; }
        public decimal? OriCOGSLD { get; set; }
        public decimal? OriCOGSLI { get; set; }
        public decimal? OriCOGSCO { get; set; }
        public decimal? OriWIPCOC1 { get; set; }
        public decimal? OriBudgetCO01 { get; set; }
        public decimal? OriBudgetCOP1 { get; set; }
        public decimal? OriCOGSUT { get; set; }
        public decimal? OriCOGSAC { get; set; }
        public decimal? OriNetPrice { get; set; }
        public decimal? NetPrice { get; set; }

        public static GPProjectDTO CreateFromResult(GPProject model , GPOriginalProject modelOri )
        {
            var result = new GPProjectDTO();
            if( model != null)
            { 
                    result = new GPProjectDTO()
                    {
                        Id = model.ID,
                        BudgetLI = model.BudgetLI,
                        BudgetCO01 = model.BudgetCO01,
                        BudgetCOC1 = model.BudgetCOC1,
                        BudgetCOA1 = model.BudgetCOA1,
                        BudgetUT = model.BudgetUT,
                        BudgetAC = model.BudgetAC,
                        PercentGPCommit = model.PercentGPCommit,
                        PriceCommit = model.PriceCommit,
                        OriCOGSLD = model.OriCOGSLD,
                        OriCOGSLI = model.OriCOGSLI,
                        OriCOGSCO = model.OriCOGSCO,
                        OriWIPCOC1 = model.OriWIPCOC1,
                        OriBudgetCO01 = model.OriBudgetCO01,
                        OriBudgetCOP1 = model.OriBudgetCOP1,
                        OriCOGSUT = model.OriCOGSUT,
                        OriCOGSAC = model.OriCOGSAC,
                        OriNetPrice = model.OriNetPrice,
                        NetPrice = model.NetPrice,
                    }; 
            }
            
            if (modelOri != null  )
                {
                    result.OriCOGSLD = modelOri.Ori_COGS_LD;
                    result.OriCOGSLI = modelOri.Ori_COGS_LI;
                    result.OriCOGSCO = modelOri.Ori_COGS_CO;
                    result.OriWIPCOC1 = modelOri.Ori_WIP_COC1;
                    result.OriBudgetCO01 = modelOri.Ori_Budget_CO01;
                    result.OriBudgetCOP1 = modelOri.Ori_Budget_COP1;
                    result.OriCOGSUT = modelOri.Ori_COGS_UT;
                    result.OriCOGSAC = modelOri.Ori_COGS_AC;
                    result.OriNetPrice = modelOri.Ori_NetPrice;
                }
                return result;
            
        }
         

    }
     
}
