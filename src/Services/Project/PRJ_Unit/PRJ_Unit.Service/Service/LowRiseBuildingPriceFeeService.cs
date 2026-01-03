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
    public class LowRiseBuildingPriceFeeService : ILowRiseBuildingPriceFeeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public LowRiseBuildingPriceFeeService(DatabaseContext db)
        {
            logModel = new LogModel("LowRiseBuildingPriceFeeService", null);
            this.DB = db;
        }
        public async Task<LowRiseBuildingPriceFeePaging> GetLowRiseBuildingPriceFeeListAsync(Guid projectID, LowRiseBuildingPriceFeeFilter filter, PageParam pageParam, LowRiseBuildingPriceFeeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<LowRiseBuildingPriceFeeQueryResult> query = DB.LowRiseBuildingPriceFees.AsNoTracking().Where(o => o.ProjectID == projectID).Include(o => o.Model).ThenInclude(o => o.TypeOfRealEstate).Include(o => o.Unit)
                                                                                              .Select(o => new LowRiseBuildingPriceFeeQueryResult
                                                                                              {
                                                                                                  LowRiseBuildingPriceFee = o,
                                                                                                  Model = o.Model,
                                                                                                  Unit = o.Unit,
                                                                                                  UpdatedBy = o.UpdatedBy,
                                                                                                  TypeOfRealEstate = o.Unit.Model.TypeOfRealEstate
                                                                                              });

            #region Filter
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Model.ID == filter.ModelID);
            }
            if (filter.UnitID != null && filter.UnitID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ID == filter.UnitID);
            }
            if (filter.PriceFrom != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Price >= filter.PriceFrom);
            }
            if (filter.PriceTo != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Price <= filter.PriceTo);
            }
            if (filter.PriceFrom != null && filter.PriceTo != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Price >= filter.PriceFrom &&
                                         x.LowRiseBuildingPriceFee.Price <= filter.PriceTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.Updated >= filter.UpdatedFrom && x.LowRiseBuildingPriceFee.Updated <= filter.UpdatedTo);
            }
            if (filter.ModelID == Guid.Empty)
            {
                query = query.Where(o => o.LowRiseBuildingPriceFee.ModelID == null);
            }
            if (filter.UnitID == Guid.Empty)
            {
                query = query.Where(o => o.LowRiseBuildingPriceFee.UnitID == null);
            }
            if (filter.TypeOfRealEstateID != null)
            {
                query = query.Where(o => o.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }

            if (filter.BuildingPermitPricePerAreaFrom != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.BuildingPermitPricePerArea >= filter.BuildingPermitPricePerAreaFrom);
            }
            if (filter.BuildingPermitPricePerAreaTo != null)
            {
                query = query.Where(x => x.LowRiseBuildingPriceFee.BuildingPermitPricePerArea <= filter.BuildingPermitPricePerAreaTo);
            }
            #endregion

            LowRiseBuildingPriceFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<LowRiseBuildingPriceFeeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);

            var results = queryResults.Select(o => LowRiseBuildingPriceFeeDTO.CreateFromQueryResult(o)).ToList();

            return new LowRiseBuildingPriceFeePaging()
            {
                PageOutput = pageOutput,
                LowRiseBuildingPriceFees = results
            };
        }

        public async Task<LowRiseBuildingPriceFeeDTO> GetLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                                         .Include(o => o.Model)
                                                         .Include(o => o.Unit)
                                                         .Include(o => o.UpdatedBy)
                                                         .FirstAsync(cancellationToken);
            var result = LowRiseBuildingPriceFeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<LowRiseBuildingPriceFeeDTO> CreateLowRiseBuildingPriceFeeAsync(Guid projectID, LowRiseBuildingPriceFeeDTO input)
        {
            await this.ValidateLowRiseBuildingPriceFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            LowRiseBuildingPriceFee model = new LowRiseBuildingPriceFee();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.BuildingPermitPricePerArea = input.BuildingPermitPricePerArea;
            await DB.LowRiseBuildingPriceFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseBuildingPriceFeeAsync(projectID, model.ID);
            return result;
        }

        public async Task<LowRiseBuildingPriceFeeDTO> UpdateLowRiseBuildingPriceFeesync(Guid projectID, Guid id, LowRiseBuildingPriceFeeDTO input)
        {
            await this.ValidateLowRiseBuildingPriceFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.BuildingPermitPricePerArea = input.BuildingPermitPricePerArea == 0 ? null : input.BuildingPermitPricePerArea;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseBuildingPriceFeeAsync(projectID, id);
            return result;
        }

        public async Task<LowRiseBuildingPriceFee> DeleteLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

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

        private async Task ValidateLowRiseBuildingPriceFee(Guid projectID, LowRiseBuildingPriceFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.Model != null)
            {
                var checkUniqueModel = input.Id != null && input.Id != Guid.Empty
               ? await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.ModelID == input.Model.Id).CountAsync() > 0
               : await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ModelID == input.Model.Id).CountAsync() > 0;
                if (checkUniqueModel)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(LowRiseBuildingPriceFeeDTO.Model)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.Model.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (input.Unit != null)
            {
                var checkUniqueUnit = input.Id != (Guid?)null
               ? await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.UnitID == input.Unit.Id).CountAsync() > 0
               : await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID && o.UnitID == input.Unit.Id).CountAsync() > 0;
                if (checkUniqueUnit)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(LowRiseBuildingPriceFeeDTO.Unit)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.Unit.UnitNo);
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
