using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PagingExtensions;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public class AddressService : IAddressService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        public AddressService(DatabaseContext db)
        {
            logModel = new LogModel("AddressService", null);
            this.DB = db;
        }
        public async Task<List<ProjectAddressListDTO>> GetProjectAddressDropdownListAsync(Guid projectID, string name, string projectAddressTypeKey, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Address> query = DB.Addresses.AsNoTracking().Where(o => o.ProjectID == projectID).Include(o => o.UpdatedBy);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.AddressNameTH.Contains(name));
            }
            if (!string.IsNullOrEmpty(projectAddressTypeKey))
            {
                var projectAddressTypeID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType && o.Key == projectAddressTypeKey).Select(o => o.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.ProjectAddressTypeMasterCenterID == projectAddressTypeID);
            }

            var queryResults = await query.OrderBy(o => o.AddressNameTH).ToListAsync(cancellationToken);
            var results = queryResults.Select(o => ProjectAddressListDTO.CreateFromModel(o)).ToList();

            return results;
        }




        public async Task<AddressPaging> GetProjectAddressListAsync(Guid projectID, PageParam pageParam, SortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<ProjectAddressQueryResult> query = DB.Addresses.AsNoTracking().Where(o => o.ProjectID == projectID)
                                           .Select(x => new ProjectAddressQueryResult
                                           {
                                               Address = x,
                                               District = x.District,
                                               ProjectAddressType = x.ProjectAddressType,
                                               Province = x.Province,
                                               SubDistrict = x.SubDistrict,
                                               TitledeedSubDistrict = x.TitledeedSubDistrict,
                                               UpdatedBy = x.UpdatedBy
                                           });

            ProjectAddressListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ProjectAddressQueryResult>(pageParam, ref query);

            var results = await query.Select(o => ProjectAddressListDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new AddressPaging()
            {
                PageOutput = pageOutput,
                ProjectAddresses = results
            };
        }

        public async Task<ProjectAddressDTO> GetProjectAddressAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Addresses.AsNoTracking()
                                        .Include(o => o.ProjectAddressType)
                                        .Include(o => o.Province)
                                        .Include(o => o.District)
                                        .Include(o => o.SubDistrict)
                                        .Include(o => o.HouseSubDistrict)
                                        .Include(o => o.TitledeedSubDistrict)
                                        .Include(o => o.LandOffice)
                                        .Include(o => o.UpdatedBy)
                                        .FirstAsync(o => o.ID == id, cancellationToken);

            //var titleDees = new List<TitledeedDetail>();
            var units = new List<Unit>();
            if (model != null)
            {
                //titleDees = await DB.TitledeedDetails.Where(o => o.AddressID == model.ID)
                //                                    .OrderBy(o => o.Unit.UnitNo)
                //                                    .Include(o => o.Unit).ToListAsync();
                units = await DB.Units.AsNoTracking().Where(o => o.AddressID == model.ID).ToListAsync(cancellationToken);
            }

            var result = ProjectAddressDTO.CreateAddressWithUnitTitledeedFromModel(model, units);
            return result;
        }

        public async Task<ProjectAddressDTO> GetProjectAddressOnlyAsync(Guid id)
        {
            var model = await DB.Addresses.AsNoTracking()
                                        .Include(o => o.ProjectAddressType)
                                        .Include(o => o.Province)
                                        .Include(o => o.District)
                                        .Include(o => o.SubDistrict)
                                        .Include(o => o.HouseSubDistrict)
                                        .Include(o => o.TitledeedSubDistrict)
                                        .Include(o => o.LandOffice)
                                        .Include(o => o.UpdatedBy)
                                        .FirstAsync(o => o.ID == id);

            var result = ProjectAddressDTO.CreateFromModel(model);
            return result;
        }


        public async Task<ProjectAddressDTO> CreateProjectAddressAsync(Guid projectID, ProjectAddressDTO input)
        {
            var model = new Database.Models.PRJ.Address();
            input.ToModel(ref model);
            model.ProjectAddressTypeMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType && o.Key == input.ProjectAddressType.Key)).ID;
            model.ProjectID = projectID;

            await DB.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetProjectAddressOnlyAsync(model.ID);

            //ที่ตั้งโอน ระบุแปลง
            if (input.UnitList != null)
            {
                //var titleDeeds = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();
                //var tds = new List<TitledeedDetail>();
                //foreach (TitleDeedDTO t in input.TitleDeedList)
                //{
                //    if (t.Unit == null && t.UnitTD == null)
                //    {
                //        continue;
                //    }
                //    Guid? unitID = t.Unit != null ? t.Unit.Id : t.UnitTD.Id;
                //    var td = titleDeeds.Where(x => x.UnitID == unitID).OrderByDescending(o => o.Created).FirstOrDefault();
                //    td.AddressID = result.Id;
                //    tds.Add(td);
                //}
                //var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
                //var uns = new List<Unit>();
                //foreach (TitleDeedDTO t in input.TitleDeedList)
                //{
                //    if (t.Unit == null && t.UnitTD == null)
                //    {
                //        continue;
                //    }
                //    Guid? unitID = t.Unit != null ? t.Unit.Id : t.UnitTD.Id;
                //    var u = units.Where(x => x.ID == unitID).OrderByDescending(o => o.Created).FirstOrDefault();
                //    u.AddressID = result.Id;
                //    uns.Add(u);
                //}

                var unitList = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
                var units = new List<Unit>();

                foreach (UnitDTO t in input.UnitList)
                {
                    if (t == null && t.UnitTD == null)
                    {
                        continue;
                    }
                    Guid? unitID = t != null ? t.Id : t.UnitTD.Id;
                    var un = unitList.Where(x => x.ID == unitID).OrderByDescending(o => o.Created).FirstOrDefault();
                    un.AddressID = result.Id;
                    units.Add(un);
                }
                DB.UpdateRange(units);
                await DB.SaveChangesAsync();

            }

            return result;
        }

        public async Task<ProjectAddressDTO> UpdateProjectAddressAsync(Guid projectID, Guid addressID, ProjectAddressDTO input)
        {
            var model = await DB.Addresses.FirstOrDefaultAsync(o => o.ID == addressID);
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            //ที่ตั้งโอน ระบุแปลง
            if (input.UnitList != null)
            {
                var unitList = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
                var units = new List<Unit>();
                foreach (Unit u in unitList)
                {
                    var un = unitList.Where(x => x.ID == u.ID && x.AddressID == addressID).OrderByDescending(o => o.Created).FirstOrDefault();
                    if (un == null)
                    {
                        continue;
                    }
                    un.AddressID = null;
                    units.Add(un);
                }
                DB.UpdateRange(units);
                await DB.SaveChangesAsync();


                units = new List<Unit>();
                foreach (UnitDTO t in input.UnitList)
                {
                    if (t == null && t.UnitTD == null)
                    {
                        continue;
                    }
                    Guid? unitID = t != null ? t.Id : t.UnitTD.Id;
                    var un = unitList.Where(x => x.ID == unitID).OrderByDescending(o => o.Created).FirstOrDefault();
                    un.AddressID = addressID;
                    units.Add(un);
                }
                DB.UpdateRange(units);
                await DB.SaveChangesAsync();
            }

            var result = await this.GetProjectAddressOnlyAsync(addressID);
            return result;
        }


        public async Task DeleteProjectAddressAsync(Guid id)
        {
            var model = await DB.Addresses.FindAsync(id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;

            // await DB.Units.Where(o => o.AddressID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.AddressID, (Guid?)null)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            //   );


            var modelUN = await DB.Units.Where(o => o.AddressID == id).ToListAsync();
            var uns = new List<Unit>();
            foreach (Unit u in modelUN)
            {
                u.AddressID = null;
                uns.Add(u);
            }
            DB.UpdateRange(uns);

            await DB.SaveChangesAsync();
        }

    }
}