using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.MST
{
    public class GPSimulateDTO : BaseDTO
    {
        public string VersionCode { get; set; }
        public string Remark { get; set; }
        public Guid? RoiID { get; set; }
        public ProjectDTO Project { get; set; }
        public DateTime? Created { get; set; }
        public string BGNo { get; set; }
        // input Gp Varsion 
        public string GPVersionType { get; set; }
        public Guid? RefGPVersion { get; set; }
        public string RefGPVersionCode { get; set; }
        public DateTime? SyncDate { get; set; } 
        public Guid? GPSyncOriginalID { get; set; }
        public Guid? GPOriginalProjectID { get; set; }
        public Guid? GPOriginalUnitID { get; set; }
        public int? Status { get; set; }
        public int? Y { get; set; }
        public int? M { get; set; }
        public int? Q { get; set; }

        // OriProject
        public GPProjectDTO GPProjectDTO { get; set; }
        public List<GPUnitDTO> GPUnitDTOs { get; set; }
        public List<GPBlockDTO> GPBlockDTOs { get; set; }

        public bool? IsCanPrint { get; set; }

        public static GPSimulateDTO CreateFromQueryResult(GPSumulateQueryResult model)
        {
            if (model != null)
            {
                var result = new GPSimulateDTO()
                {
                    Id = model.GPVersion.ID,
                    VersionCode = model.GPVersion.VersionCode,
                    Remark = model.GPVersion.VersionRemark,
                    Project = ProjectDTO.CreateFromModel(model.Projetc),
                    Updated = model.GPVersion.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName ?? model.CreatedBy?.DisplayName,
                    Created = model.GPVersion.Created,
                    Status = model.GPVersion.Status,
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static GPSimulateDTO CreateFromQuery(GPVersion LastGPVersion, GPProject gpProject,List< GPUnit> gpUnits,List< GPOriginalQuery> gpOriginalQuery , Project project, GPSyncOriginal gPSyncOriginal)
        {
            var result = new GPSimulateDTO()
            {
                Created = LastGPVersion?.Created,
                GPVersionType = LastGPVersion?.GPVersionType,
                Id = LastGPVersion?.ID,
                GPSyncOriginalID = LastGPVersion?.GPSyncOriginalID, 
                M = LastGPVersion?.M,
                Q = LastGPVersion?.Q,
                Y = LastGPVersion?.Y,
                RoiID = LastGPVersion?.RoiID,
                RefGPVersion = LastGPVersion?.RefGPVersion,
                Remark = LastGPVersion?.VersionRemark,
                Status = LastGPVersion?.Status,
                SyncDate = LastGPVersion?.SyncDate,
                Updated = LastGPVersion?.Updated,
                VersionCode = LastGPVersion?.VersionCode,
            };
            if(LastGPVersion == null)
            {
                result.SyncDate = gPSyncOriginal?.LogSyncDate;
                result.GPSyncOriginalID = gPSyncOriginal?.ID;
            }
            result.Project = ProjectDTO.CreateFromModel(project);
 
            // Cache GPOriginalProject to avoid repeated FirstOrDefault calls
            var firstGPOriginalProject = gpOriginalQuery.FirstOrDefault()?.GPOriginalProject;
            result.GPProjectDTO = GPProjectDTO.CreateFromResult(gpProject, firstGPOriginalProject);
            
            // Create Dictionary for O(1) lookup instead of O(n) Where().FirstOrDefault()
            var gpUnitsDict = gpUnits?.ToDictionary(u => u.UnitID) ?? new Dictionary<Guid?, GPUnit>();
            
            // Pre-allocate List capacity for better performance
            var GPUnitDTOs = new List<GPUnitDTO>(gpOriginalQuery.Count);
            foreach(var item in gpOriginalQuery)
            {
                var GpOri = item.GPOriginalUnit;
                gpUnitsDict.TryGetValue(GpOri.UnitID, out var gpunit);
                var unit = item.Unit;
                var unitStatus = item.UnitStatus;
                GPUnitDTO gpUnitDTO = GPUnitDTO.CreateFromResult(gpunit , GpOri , unit , unitStatus);
                GPUnitDTOs.Add(gpUnitDTO);
            }
            result.GPUnitDTOs = GPUnitDTOs.OrderBy(x=>x.Unit?.UnitNo).ToList();
            
            // Cache BGNo to avoid repeated property access
            var bgNo = project.BG.BGNo;
            result.BGNo = bgNo;
            
            // ถ้า TH มีแบบ block
            if (bgNo == "2")
            {
                result.GPBlockDTOs = result.GPUnitDTOs.GroupBy(x=>x.BlockNumber)
                    .Select(x=> GPBlockDTO.CreateFromResult(x.ToList()))
                    .OrderBy(x=>x.WBSBlock)
                    .ToList();
            }
            return result;
        }
        public static GPSimulateDTO CreateFromQuery(GPVersion LastGPVersion, GPProject gpProject, List<GPUnit> gpUnits, List<GPOriginalQuery> gpOriginalQuery, Project project)
        {
            var result = new GPSimulateDTO()
            {
                Created = LastGPVersion?.Created,
                GPVersionType = LastGPVersion?.GPVersionType,
                Id = LastGPVersion?.ID,
                GPSyncOriginalID = LastGPVersion?.GPSyncOriginalID,
                M = LastGPVersion?.M,
                Q = LastGPVersion?.Q,
                Y = LastGPVersion?.Y,
                RoiID = LastGPVersion?.RoiID,
                RefGPVersion = LastGPVersion?.RefGPVersion,
                Remark = LastGPVersion?.VersionRemark,
                Status = LastGPVersion?.Status,
                SyncDate = LastGPVersion?.SyncDate,
                Updated = LastGPVersion?.Updated,
                VersionCode = LastGPVersion?.VersionCode,
            }; 
            result.Project = ProjectDTO.CreateFromModel(project);

            // Cache GPOriginalProject to avoid repeated FirstOrDefault calls
            var firstGPOriginalProject = gpOriginalQuery.FirstOrDefault()?.GPOriginalProject;
            result.GPProjectDTO = GPProjectDTO.CreateFromResult(gpProject, firstGPOriginalProject);
            
            // Create Dictionary for O(1) lookup instead of O(n) Where().FirstOrDefault()
            var gpUnitsDict = gpUnits?.ToDictionary(u => u.UnitID) ?? new Dictionary<Guid?, GPUnit>();
            
            // Pre-allocate List capacity for better performance
            var GPUnitDTOs = new List<GPUnitDTO>(gpOriginalQuery.Count);
            foreach (var item in gpOriginalQuery)
            {
                var GpOri = item.GPOriginalUnit;
                gpUnitsDict.TryGetValue(GpOri.UnitID, out var gpunit);
                var unit = item.Unit;
                var unitStatus = item.UnitStatus;
                GPUnitDTO gpUnitDTO = GPUnitDTO.CreateFromResult(gpunit, GpOri, unit, unitStatus);
                GPUnitDTOs.Add(gpUnitDTO);
            }
            result.GPUnitDTOs = GPUnitDTOs.OrderBy(x => x.Unit?.UnitNo).ToList();
            
            // Cache BGNo to avoid repeated property access
            var bgNo = project.BG.BGNo;
            result.BGNo = bgNo;
            
            // ถ้า TH มีแบบ block
            if (bgNo == "2")
            {
                result.GPBlockDTOs = result.GPUnitDTOs.GroupBy(x => x.BlockNumber)
                    .Select(x => GPBlockDTO.CreateFromResult(x.ToList()))
                    .OrderBy(x => x.WBSBlock)
                    .ToList();
            }
            return result;
        }

        public static void SortBy(GPSimulateSortByParam sortByParam, ref IQueryable<GPSumulateQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case GPSimulateSortBy.Version:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.VersionCode);
                        else query = query.OrderByDescending(o => o.GPVersion.VersionCode);
                        break;
                    case GPSimulateSortBy.Remark:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.VersionRemark);
                        else query = query.OrderByDescending(o => o.GPVersion.VersionRemark);
                        break;
                    case GPSimulateSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Projetc.ProjectNo);
                        else query = query.OrderByDescending(o => o.Projetc.ProjectNo);
                        break;
                    case GPSimulateSortBy.Created:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.Created);
                        else query = query.OrderByDescending(o => o.GPVersion.Created);
                        break;
                    case GPSimulateSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.Updated);
                        else query = query.OrderByDescending(o => o.GPVersion.Updated);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.GPVersion.Created);
                        break;

                }
            }
            else
            {
                query = query.OrderByDescending(o => o.GPVersion.Created);
            }
        }

        public void ToModel(ref MasterCenter model)
        {
            //model.Key = this.Key; 
        }

    }

    public class GPSumulateQueryResult
    {
        public Project Projetc { get; set; }
        public string Version { get; set; }
        public string Remark { get; set; }
        public User UpdatedBy { get; set; }
        public User CreatedBy { get; set; }
        public GPVersion GPVersion { get; set; }
    }
    public class GPOriginalQuery
    {
        public GPOriginalProject GPOriginalProject { get; set; }
        public GPOriginalUnit GPOriginalUnit { get; set; }
        public Unit Unit { get; set; }
        public VWUnitStatus UnitStatus { get; set;}
    }
}
