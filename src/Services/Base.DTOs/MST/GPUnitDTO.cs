using Base.DTOs.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.USR;
using System;
using System.Linq;

namespace Base.DTOs.MST
{
    public class GPUnitDTO : BaseDTO
    {
        public Guid? GPVersionID { get; set; }
        public Guid? UnitID { get; set; }
        public string WBSBlock { get; set; }
        public string BlockNumber { get; set; }
        public decimal? BudgetLI { get; set; }
        public decimal? BudgetCO01 { get; set; }
        public decimal? Budget_CO01_Block { get; set; }
        public decimal? BudgetCOC1 { get; set; }
        public decimal? Budget_CO_A1 { get; set; }
        public decimal? Budget_UT { get; set; }
        public decimal? Budget_AC { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? PercentGPNew { get; set; }
        public decimal? PriceCommit { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? Ori_COGS_LD { get; set; }
        public decimal? Ori_COGS_LI { get; set; }
        public decimal? Ori_COGS_CO { get; set; }
        public decimal? Ori_Budget_CO01 { get; set; }
        public decimal? Ori_WIP_COC1 { get; set; }
        public decimal? Ori_COGS_UT { get; set; }
        public decimal? Ori_COGS_AC { get; set; }
        public decimal? Ori_NetPrice { get; set; }
        public decimal? Ori_PercentGPCommit { get; set; }
        public decimal? Ori_MinPrice { get; set; }
        public UnitDTO Unit { get; set; }
        public string UnitStatus { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; } 

        public static GPUnitDTO CreateFromResult(GPUnit model, GPOriginalUnit modelOri, Unit unit , VWUnitStatus unitStatus)
        {
            var result = new GPUnitDTO();
            if (model != null)
            {
                result = new GPUnitDTO()
                {
                    Id = model.ID,
                    GPVersionID = model.GPVersionID,
                    UnitID = model.UnitID,
                    WBSBlock = model.WBSBlock,
                    BlockNumber = model.BlockNumber,
                    BudgetLI = model.Budget_LI,
                    BudgetCO01 = model.Budget_CO01,
                    Budget_CO01_Block = model.Budget_CO01_Block,
                    BudgetCOC1 = model.Budget_COC1,
                    Budget_CO_A1 = model.Budget_CO_A1,
                    Budget_UT = model.Budget_UT,
                    Budget_AC = model.Budget_AC,
                    NetPrice = model.NetPrice,
                    PercentGPNew = model.PercentGPNew,
                    PriceCommit = model.PriceCommit,
                    MinPrice = model.MinPrice,
                    Ori_COGS_LD = model.Ori_COGS_LD,
                    Ori_COGS_LI = model.Ori_COGS_LI,
                    Ori_COGS_CO = model.Ori_COGS_CO,
                    Ori_Budget_CO01 = model.Ori_Budget_CO01,
                    Ori_WIP_COC1 = model.Ori_WIP_COC1,
                    Ori_COGS_UT = model.Ori_COGS_UT,
                    Ori_COGS_AC = model.Ori_COGS_AC,
                    Ori_NetPrice = model.Ori_NetPrice,
                    Ori_PercentGPCommit = model.Ori_PercentGPCommit,
                    Ori_MinPrice = model.Ori_MinPrice,
                };
            }
            if (modelOri != null)
            {
                result.UnitID = modelOri.UnitID;
                result.WBSBlock = modelOri.WBSBlock;
                result.BlockNumber = modelOri.BlockNumber;
                result.Ori_COGS_LD = modelOri.COGS_LD;
                result.Ori_COGS_LI = modelOri.COGS_LI;
                result.Ori_COGS_CO = modelOri.COGS_CO;
                result.Ori_Budget_CO01 = modelOri.Budget_CO01;
                result.Ori_WIP_COC1 = modelOri.WIP_COC1;
                result.Ori_COGS_UT = modelOri.COGS_UT;
                result.Ori_COGS_AC = modelOri.COGS_AC;
                result.Ori_NetPrice = modelOri.Revenue;
                result.Ori_PercentGPCommit = modelOri.PercentGP;
                result.Ori_MinPrice = modelOri.MinPrice;
            }
            result.Unit = UnitDTO.CreateFromModel(unit);
            if(unitStatus != null)
            {
                result.UnitStatus = unitStatus.UnitStatus;
            }
            return result;

        }


    }

}
