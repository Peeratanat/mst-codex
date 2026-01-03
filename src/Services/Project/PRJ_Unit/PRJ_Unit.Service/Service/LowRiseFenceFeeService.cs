using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
    public class LowRiseFenceFeeService : ILowRiseFenceFeeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public LowRiseFenceFeeService(DatabaseContext db)
        {
            logModel = new LogModel("LowRiseFenceFeeService", null);
            this.DB = db;
        }
        public async Task<LowRiseFenceFeePaging> GetLowRiseFenceFeeListAsync(Guid projectID, LowRiseFenceFeeFilter filter, PageParam pageParam, LowRiseFenceFeeSortByParam sortByParam,CancellationToken cancellationToken = default)
        {
            IQueryable<LowRiseFenceFeeQueryResult> query = DB.LowRiseFenceFees.AsNoTracking().Where(o => o.ProjectID == projectID)
                                                                              .Select(o => new LowRiseFenceFeeQueryResult
                                                                              {
                                                                                  LowRiseFenceFee = o,
                                                                                  LandOffice = o.LandOffice,
                                                                                  TypeOfRealEstate = o.TypeOfRealEstate,
                                                                                  UpdatedBy = o.UpdatedBy
                                                                              });
            #region Filter
            if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
            {
                query = query.Where(x => x.LowRiseFenceFee.LandOfficeID == filter.LandOfficeID);
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(x => x.LowRiseFenceFee.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }

            #region ConcreateRate
            if (filter.ConcreteRateFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcreteRate >= filter.ConcreteRateFrom);
            }
            if (filter.ConcreteRateTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcreteRate <= filter.ConcreteRateTo);
            }
            if (filter.ConcreteRateFrom != null && filter.ConcreteRateTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcreteRate >= filter.ConcreteRateFrom &&
                                         x.LowRiseFenceFee.ConcreteRate <= filter.ConcreteRateTo);
            }
            #endregion

            #region IronPrice
            if (filter.IronPriceFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IronPrice >= filter.IronPriceFrom);
            }
            if (filter.IronPriceTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IronPrice <= filter.IronPriceTo);
            }
            if (filter.IronPriceFrom != null && filter.IronPriceTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IronPrice >= filter.IronPriceFrom &&
                                         x.LowRiseFenceFee.IronPrice <= filter.IronPriceTo);
            }
            #endregion

            #region IronRate
            if (filter.IronRateFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IronRate >= filter.IronRateFrom);
            }
            if (filter.IronRateTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IronRate <= filter.IronRateTo);
            }
            if (filter.IronRateFrom != null && filter.IronRateTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcreteRate >= filter.IronRateFrom &&
                                         x.LowRiseFenceFee.ConcreteRate <= filter.IronRateTo);
            }
            #endregion

            #region ConcreatePrice
            if (filter.ConcretePriceFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcretePrice >= filter.ConcretePriceFrom);
            }
            if (filter.ConcretePriceTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcretePrice <= filter.ConcretePriceTo);
            }
            if (filter.ConcretePriceFrom != null && filter.ConcretePriceTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.ConcretePrice >= filter.ConcretePriceFrom &&
                                         x.LowRiseFenceFee.ConcretePrice <= filter.ConcretePriceTo);
            }
            #endregion

            #region DepreciationPerYear
            if (filter.DepreciationPerYearFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.DepreciationPerYear >= filter.DepreciationPerYearFrom);
            }
            if (filter.DepreciationPerYearTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.DepreciationPerYear <= filter.DepreciationPerYearTo);
            }
            if (filter.DepreciationPerYearFrom != null && filter.DepreciationPerYearTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.DepreciationPerYear >= filter.DepreciationPerYearFrom &&
                                         x.LowRiseFenceFee.DepreciationPerYear <= filter.DepreciationPerYearTo);
            }
            #endregion

            if (filter.IsCalculateDepreciation != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.IsCalculateDepreciation == filter.IsCalculateDepreciation);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseFenceFee.Updated >= filter.UpdatedFrom && x.LowRiseFenceFee.Updated <= filter.UpdatedTo);
            }
            #endregion

            LowRiseFenceFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<LowRiseFenceFeeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => LowRiseFenceFeeDTO.CreateFromQueryResult(o)).ToList();

            return new LowRiseFenceFeePaging()
            {
                PageOutput = pageOutput,
                LowRiseFenceFees = results
            };
        }

        public async Task<LowRiseFenceFeeDTO> GetLowRiseFenceFeeAsync(Guid projectID, Guid id,CancellationToken cancellationToken = default)
        {
            var model = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                                 .Include(o => o.LandOffice)
                                                 .Include(o => o.TypeOfRealEstate)
                                                 .Include(o => o.UpdatedBy)
                                                 .FirstAsync(cancellationToken);
            var result = LowRiseFenceFeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<LowRiseFenceFeeDTO> CreateLowRiseFenceFeeAsync(Guid projectID, LowRiseFenceFeeDTO input)
        {
            await this.ValidateLowRiseFenceFeeService(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();

            LowRiseFenceFee model = new LowRiseFenceFee();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.LowRiseFenceFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseFenceFeeAsync(projectID, model.ID);
            return result;
        }

        public async Task<LowRiseFenceFeeDTO> UpdateLowRiseFenceFeeAsync(Guid projectID, Guid id, LowRiseFenceFeeDTO input)
        {
            await this.ValidateLowRiseFenceFeeService(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseFenceFeeAsync(projectID, id);
            return result;
        }

        public async Task<LowRiseFenceFee> DeleteLowRiseFenceFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> TransferFeeDataStatus(Guid projectID)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
            var allHiseRiseFee = await DB.HighRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFees = await DB.LowRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFenceFees = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseBuildingPriceFees = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allRoundFees = await DB.RoundFees.Where(o => o.ProjectID == projectID).ToListAsync();


            var transferFeeDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusMasterCenterID = transferFeeDataStatusPrepareMasterCenterID;

            if (project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                if (allLowRiseFees.Count() == 0 || allRoundFees.Count() == 0 || allLowRiseBuildingPriceFees.Count() == 0)
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
                if (allRoundFees.Count() == 0)
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

        private async Task ValidateLowRiseFenceFeeService(Guid projectID, LowRiseFenceFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.LandOffice != null && input.TypeOfRealEstate!=null)
            {
                var checkUniqueLandOfficeAndType = input.Id != (Guid?)null
               ? await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.LandOfficeID == input.LandOffice.Id && o.TypeOfRealEstateID==input.TypeOfRealEstate.Id).CountAsync() > 0
               : await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID && o.LandOfficeID == input.LandOffice.Id && o.TypeOfRealEstateID == input.TypeOfRealEstate.Id).CountAsync() > 0;
                if (checkUniqueLandOfficeAndType)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(LowRiseFenceFeeDTO.LandOffice)).GetCustomAttribute<DescriptionAttribute>().Description +" และ "+ input.GetType().GetProperty(nameof(LowRiseFenceFeeDTO.TypeOfRealEstate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.LandOffice.NameTH +" และ "+input.TypeOfRealEstate.Name);
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
