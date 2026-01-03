using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PagingExtensions;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public class DropdownService : IDropdownService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;
        public DropdownService(DatabaseContext db)
        {
            logModel = new LogModel("DropdownService", null);
            this.DB = db;
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, bool ignoreRepurchase, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = null;


            if (UserID.HasValue)
            { 
                query = from userProj in DB.UserAuthorizeProjects.AsNoTracking()
                        join proj in DB.Projects on userProj.ProjectID equals proj.ID
                        where proj.IsActive == isActive && userProj.UserID == UserID
                        select proj;
            }
            else
            {
                query = DB.Projects.AsNoTracking();
            }

            query = query.Include(o => o.ProjectStatus)
                            .Include(o => o.ProductType)

                            .Include(o => o.ProjectType)
                            .Include(o => o.BG)
                            .Include(o => o.SubBG)
                            .Include(o => o.Brand)
                            .Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0");

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }

            if (ignoreRepurchase)
            {
                query = query.Where(o => !o.ProjectNo.StartsWith("999"));
            }

            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo)
                    .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);


            return results;
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectWalkReferDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects.AsNoTracking();

            query = query.Include(o => o.ProjectStatus)
                            .Include(o => o.ProductType)
                            .Include(o => o.BG)
                            .Include(o => o.SubBG)
                            .Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0");

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.FirstAsync(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus);
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID.ID);
            }

            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo)
                    .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }




        public async Task<List<ProjectDropdownDTO>> GetProjectTitledeedRequestDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? productType, Guid? landOffice, CancellationToken cancellationToken = default)
        {

            var query = DB.Addresses.AsNoTracking()
                        .Include(o => o.LandOffice)
                        .Include(o => o.Project)
                        .Include(o => o.Project.ProjectStatus)
                        .Include(o => o.Project.ProductType)
                        .Include(o => o.Project.BG)
                        .Include(o => o.Project.SubBG)
                        .Where(o => o.Project.IsActive == isActive && o.Project.SubBG.SubBGNo.Substring(2, 1) != "0");

            var ProjectAddressType = await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType && o.Key == "3");
            query = query.Where(o => o.ProjectAddressTypeMasterCenterID == ProjectAddressType.ID);

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.Project.ProjectNo ?? "") + (o.Project.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.Project.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.Project.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }
            if (productType != null)
            {
                query = query.Where(o => o.Project.ProductTypeMasterCenterID == productType);
            }

            if (landOffice != null)
            {
                query = query.Where(o => o.LandOfficeID == landOffice);
            }

            var results = await query.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Project.ProjectNameTH)
                .GroupBy(o => o.ProjectID).Select(o => ProjectDropdownDTO.CreateFromModel(o.FirstOrDefault().Project)).ToListAsync(cancellationToken);

            return results;
        }



        public async Task<List<ProjectDropdownDTO>> GetProjectByProductTypeDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? productType, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects.AsNoTracking();

            query = query.Include(o => o.ProjectStatus)
                            .Include(o => o.ProductType)
                            .Include(o => o.BG)
                            .Include(o => o.SubBG)
                            .Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0");

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }
            if (productType != null)
            {
                query = query.Where(o => o.ProductTypeMasterCenterID == productType);
            }

            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH)
                                .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);


            return results;
        }


        public async Task<List<ProjectDropdownDTO>> GetProjectAllStatusDropdownListAsync(string name, Guid? companyID, Guid? UserID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = null;

            if (UserID.HasValue)
            {
                query = from userProj in DB.UserAuthorizeProjects.AsNoTracking()
                        join proj in DB.Projects on userProj.ProjectID equals proj.ID
                        where userProj.UserID == UserID
                        select proj;
            }
            else
            {
                query = DB.Projects.AsNoTracking();
            }

            query = query.Include(o => o.ProjectStatus)
                         .Include(o => o.ProductType)
                         .Include(o => o.BG)
                         .Include(o => o.SubBG);


            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);


            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo)
            .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }


        public async Task<List<ProjectDropdownDTO>> GetProjectAllIsActiveDropdownListAsync(string name, Guid? companyID, Guid? UserID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = null;
            if (UserID.HasValue)
            {
                query = from userProj in DB.UserAuthorizeProjects.AsNoTracking()
                        join proj in DB.Projects on userProj.ProjectID equals proj.ID
                        where userProj.UserID == UserID
                        select proj;
            }
            else
            {
                query = DB.Projects.AsNoTracking();
            }

            query = query.Include(o => o.ProjectStatus)
                         .Include(o => o.ProjectType)
                         .Include(o => o.BG)
                         .Include(o => o.SubBG)
                         .Include(o => o.ProductType);

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            query = query.Where(o => o.IsActive == true && (o.ProjectStatus.Key == "1" || o.ProjectStatus.Key == "0"));
            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH)
                    .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }


        public async Task<List<ProjectDropdownDTO>> GetNonAuthProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey, Guid? UserID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Database.Models.PRJ.Project> query = null;


            //if (UserID.HasValue)
            //{
            //    query = from userProj in DB.UserAuthorizeProjects.AsNoTracking()
            //            join proj in DB.Projects on userProj.ProjectID equals proj.ID
            //            where proj.IsActive == isActive && userProj.UserID == UserID
            //            select proj;
            //}
            //else
            //{
                query = DB.Projects.AsNoTracking();
            //}

            query = query.Include(o => o.ProjectStatus)
                            .Include(o => o.ProductType)

                            .Include(o => o.ProjectType)
                            .Include(o => o.BG)
                            .Include(o => o.SubBG)
                            .Where(o => o.IsActive == isActive && o.SubBG.SubBGNo.Substring(2, 1) != "0");

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace("-", "");
                name = name.ToLower();
                query = query.Where(o => ((o.ProjectNo ?? "") + (o.ProjectNameTH ?? "")).ToLower().Contains(name));
            }

            if (companyID != null && companyID != Guid.Empty)
                query = query.Where(o => o.CompanyID == companyID);

            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }

            var results = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo)
                    .Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);


            return results;
        }

    }
}
