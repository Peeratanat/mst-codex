using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class RoundFeeDTO : BaseDTO
    {
        /// <summary>
        ///  สำนักงานที่ดิน
        /// </summary>
        [Description("สำนักงานที่ดิน")]
        public MST.LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        ///  ค่าทำเนียมจิปาภะ
        /// </summary>
        public decimal? OtherFee { get; set; }
        /// <summary>
        ///  สูตรปัดเศาค่าธรรมเนียมการโอน
        ///  Master/api/MasterCenters?masterCenterGroupKey=RoundFormulaType
        /// </summary>
        public MST.MasterCenterDropdownDTO TransferFeeRoundFormula { get; set; }
        /// <summary>
        ///  สูตรปัดเศษภาษีธุรกิจเฉพาะ
        ///  Master/api/MasterCenters?masterCenterGroupKey=RoundFormulaType
        /// </summary>
        public MST.MasterCenterDropdownDTO BusinessTaxRoundFormula { get; set; }
        /// <summary>
        ///  สูตรปัดเศษภาษีท้องถื่น
        ///  Master/api/MasterCenters?masterCenterGroupKey=RoundFormulaType
        /// </summary>
        public MST.MasterCenterDropdownDTO LocalTaxRoundFormula { get; set; }
        /// <summary>
        ///  สูตรปัดเศษเงินได้นิติบุคคล
        ///  Master/api/MasterCenters?masterCenterGroupKey=RoundFormulaType
        /// </summary>
        public MST.MasterCenterDropdownDTO IncomeTaxRoundFormula { get; set; }

        public static RoundFeeDTO CreateFromModel(RoundFee model)
        {
            if (model != null)
            {
                var result = new RoundFeeDTO()
                {
                    Id = model.ID,
                    LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    OtherFee= model.OtherFee,
                    TransferFeeRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferFeeRoundFormula),
                    BusinessTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.BusinessTaxRoundFormula),
                    LocalTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.LocalTaxRoundFormula),
                    IncomeTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.IncomeTaxRoundFormula),
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

        public static RoundFeeDTO CreateFromQueryResult(RoundFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new RoundFeeDTO()
                {
                    Id = model.RoundFee.ID,
                    LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    OtherFee = model.RoundFee.OtherFee,
                    TransferFeeRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferFeeRoundFormula),
                    BusinessTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.BusinessTaxRoundFormula),
                    LocalTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.LocalTaxRoundFormula),
                    IncomeTaxRoundFormula = MST.MasterCenterDropdownDTO.CreateFromModel(model.IncomeTaxRoundFormula),
                    Updated = model.RoundFee.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RoundFeeSortByParam sortByParam, ref IQueryable<RoundFeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RoundFeeSortBy.LandOffice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LandOffice.NameTH);
                        else query = query.OrderByDescending(o => o.LandOffice.NameTH);
                        break;
                    case RoundFeeSortBy.OtherFee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.RoundFee.OtherFee);
                        else query = query.OrderByDescending(o => o.RoundFee.OtherFee);
                        break;
                    case RoundFeeSortBy.TransferFeeRoundFormula:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TransferFeeRoundFormula.Name);
                        else query = query.OrderByDescending(o => o.TransferFeeRoundFormula.Name);
                        break;
                    case RoundFeeSortBy.BusinessTaxRoundFormula:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BusinessTaxRoundFormula.Name);
                        else query = query.OrderByDescending(o => o.BusinessTaxRoundFormula.Name);
                        break;
                    case RoundFeeSortBy.LocalTaxRoundFormula:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LocalTaxRoundFormula.Name);
                        else query = query.OrderByDescending(o => o.LocalTaxRoundFormula.Name);
                        break;
                    case RoundFeeSortBy.IncomeTaxRoundFormula:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.IncomeTaxRoundFormula.Name);
                        else query = query.OrderByDescending(o => o.IncomeTaxRoundFormula.Name);
                        break;
                    case RoundFeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case RoundFeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.RoundFee.Updated);
                        else query = query.OrderByDescending(o => o.RoundFee.Updated);
                        break;
                    default:
                        query = query.OrderBy(o => o.LandOffice.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.LandOffice.NameTH);
            }
        }

        public void ToModel(ref RoundFee model)
        {
            model.LandOfficeID = this.LandOffice?.Id;
            model.OtherFee = this.OtherFee;
            model.TransferFeeRoundFormulaMasterCenterID = this.TransferFeeRoundFormula?.Id;
            model.BusinessTaxRoundFormulaMasterCenterID = this.BusinessTaxRoundFormula?.Id;
            model.LocalTaxRoundFormulaMasterCenterID = this.LocalTaxRoundFormula?.Id;
            model.IncomeTaxRoundFormulaMasterCenterID = this.IncomeTaxRoundFormula?.Id;
        }
    }
    public class RoundFeeQueryResult
    {
        public RoundFee RoundFee { get; set; }
        public MasterCenter TransferFeeRoundFormula { get; set; }
        public MasterCenter BusinessTaxRoundFormula { get; set; }
        public MasterCenter LocalTaxRoundFormula { get; set; }
        public MasterCenter IncomeTaxRoundFormula { get; set; }
        public LandOffice LandOffice { get; set; }
        public User UpdatedBy { get; set; }
    }
}
