using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Project.Services
{
    public class RoundFeeService : IRoundFeeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public RoundFeeService(DatabaseContext db)
        {
            logModel = new LogModel("RoundFeeService", null);
            this.DB = db;
        }

        public async Task<RoundFeePaging> GetRoundFeeListAsync(Guid projectID, RoundFeeFilter filter, PageParam pageParam, RoundFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<RoundFeeQueryResult> query = DB.RoundFees.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                                .Select(o => new RoundFeeQueryResult
                                                                {
                                                                    LandOffice = o.LandOffice,
                                                                    BusinessTaxRoundFormula = o.BusinessTaxRoundFormula,
                                                                    IncomeTaxRoundFormula = o.IncomeTaxRoundFormula,
                                                                    LocalTaxRoundFormula = o.LocalTaxRoundFormula,
                                                                    RoundFee = o,
                                                                    TransferFeeRoundFormula = o.TransferFeeRoundFormula,
                                                                    UpdatedBy = o.UpdatedBy
                                                                });
            #region Filter
            if (!string.IsNullOrEmpty(filter.IncomeTaxRoundFormulaKey))
            {
                var incomeTaxRoundFormulaID = await DB.MasterCenters.FirstAsync(x => x.Key == filter.IncomeTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType");
                query = query.Where(x => x.RoundFee.IncomeTaxRoundFormulaMasterCenterID == incomeTaxRoundFormulaID.ID);
            }
            if (!string.IsNullOrEmpty(filter.BusinessTaxRoundFormulaKey))
            {
                var businessTaxRoundFormulaID = await DB.MasterCenters.FirstAsync(x => x.Key == filter.BusinessTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType");
                query = query.Where(x => x.RoundFee.BusinessTaxRoundFormulaMasterCenterID == businessTaxRoundFormulaID.ID);
            }
            if (!string.IsNullOrEmpty(filter.LocalTaxRoundFormulaKey))
            {
                var localTaxRoundFormulaID = await DB.MasterCenters.FirstAsync(x => x.Key == filter.LocalTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType");
                query = query.Where(x => x.RoundFee.LocalTaxRoundFormulaMasterCenterID == localTaxRoundFormulaID.ID);
            }
            if (!string.IsNullOrEmpty(filter.TransferFeeRoundFormulaKey))
            {
                var transferFeeRoundFormulaID = await DB.MasterCenters.FirstAsync(x => x.Key == filter.TransferFeeRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType");
                query = query.Where(x => x.RoundFee.IncomeTaxRoundFormulaMasterCenterID == transferFeeRoundFormulaID.ID);
            }
            if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
            {
                query = query.Where(x => x.RoundFee.LandOfficeID == filter.LandOfficeID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.RoundFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.RoundFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.RoundFee.Updated >= filter.UpdatedFrom && x.RoundFee.Updated <= filter.UpdatedTo);
            }
            #endregion

            RoundFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RoundFeeQueryResult>(pageParam, ref query);

            var results = await query.Select(o => RoundFeeDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new RoundFeePaging()
            {
                PageOutput = pageOutput,
                RoundFees = results
            };
        }

        public async Task<RoundFeeDTO> GetRoundFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.RoundFees.AsNoTracking()
                                          .Include(o => o.LandOffice)
                                          .Include(o => o.TransferFeeRoundFormula)
                                          .Include(o => o.BusinessTaxRoundFormula)
                                          .Include(o => o.LocalTaxRoundFormula)
                                          .Include(o => o.IncomeTaxRoundFormula)
                                          .Include(o => o.UpdatedBy)
                                          .FirstAsync(o => o.ProjectID == projectID && o.ID == id, cancellationToken);
            var result = RoundFeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RoundFeeDTO> CreateRoundFeeAsync(Guid projectID, RoundFeeDTO input)
        {
            await this.ValidateRoundFee(projectID, input);
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            RoundFee model = new RoundFee();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.RoundFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetRoundFeeAsync(projectID, model.ID);

            return result;
        }

        public async Task<RoundFeeDTO> UpdateRoundFeeAsync(Guid projectID, Guid id, RoundFeeDTO input)
        {
            await this.ValidateRoundFee(projectID, input);
            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.RoundFees.FirstAsync(o => o.ProjectID == projectID && o.ID == id);

            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetRoundFeeAsync(projectID, model.ID);
            return result;
        }

        public async Task<RoundFee> DeleteRoundFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.RoundFees.FirstAsync(o => o.ProjectID == projectID && o.ID == id);

            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> TransferFeeDataStatus(Guid projectID)
        {
            var project = await DB.Projects.Include(o => o.ProductType).FirstAsync(o => o.ID == projectID);
            var allHiseRiseFee = await DB.HighRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFees = await DB.LowRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFenceFees = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseBuildingPriceFees = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allRoundFees = await DB.RoundFees.Where(o => o.ProjectID == projectID).ToListAsync();


            var transferFeeDataStatusPrepareMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft)).ID;
            var transferFeeDataStatusTransferMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer)).ID;
            var transferFeeDataStatusMasterCenterID = transferFeeDataStatusPrepareMasterCenterID;

            if (project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                if (allLowRiseFees.Count == 0 || allRoundFees.Count == 0 || allLowRiseBuildingPriceFees.Count == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allLowRiseFees.TrueForAll(o => o.EstimatePriceArea != null)
                    && allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null)
                    && allLowRiseBuildingPriceFees.TrueForAll(o => o.ModelID != null && o.UnitID != null && o.Price != null)
                    )
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            else
            {
                if (allRoundFees.Count == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null))
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            return transferFeeDataStatusMasterCenterID;
        }

        private async Task ValidateRoundFee(Guid projectID, RoundFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.LandOffice != null)
            {
                var checkUniqueLandOffice = input.Id != (Guid?)null
               ? DB.RoundFees.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.LandOfficeID == input.LandOffice.Id)
               : DB.RoundFees.Any(o => o.ProjectID == projectID && o.LandOfficeID == input.LandOffice.Id);
                if (checkUniqueLandOffice)
                {
                    var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(RoundFeeDTO.LandOffice)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.LandOffice.NameTH);
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
