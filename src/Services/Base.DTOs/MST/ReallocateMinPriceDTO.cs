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
    public class ReallocateMinPriceDTO : BaseDTO
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

        public bool? IsCanPrint { get; set; }

        public static ReallocateMinPriceDTO CreateFromQueryResult(GPReallocateMinPriceQueryResult model)
        {
            if (model != null)
            {
                var result = new ReallocateMinPriceDTO()
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
        public static ReallocateMinPriceDTO CreateFromQuery(GPVersion LastGPVersion, GPProject gpProject,List< GPUnit> gpUnits,List<GPReallocateMinPriceQuery> gpOriginalQuery , Project project, GPSyncOriginal gPSyncOriginal)
        {
            var result = new ReallocateMinPriceDTO()
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
            result.GPProjectDTO = GPProjectDTO.CreateFromResult(gpProject, gpOriginalQuery.FirstOrDefault()?.GPOriginalProject);
            var GPUnitDTOs = new List<GPUnitDTO>();
            foreach(var item in gpOriginalQuery)
            { 
                var GpOri = item.GPOriginalUnit;
                var gpunit = gpUnits != null ? gpUnits.Where(o => o.UnitID == GpOri.UnitID).FirstOrDefault() : null;
                var unit = item.Unit;
                var unitStatus = item.UnitStatus;
                GPUnitDTO gpUnitDTO = GPUnitDTO.CreateFromResult(gpunit , GpOri , unit , unitStatus);
                GPUnitDTOs.Add(gpUnitDTO);
            }
            result.GPUnitDTOs = GPUnitDTOs.OrderBy(x=>x.Unit?.UnitNo).ToList();
            result.BGNo = project.BG.BGNo;
            return result;
        }
        public static ReallocateMinPriceDTO CreateFromQuery(GPVersion LastGPVersion, GPProject gpProject, List<GPUnit> gpUnits, List<GPReallocateMinPriceQuery> gpOriginalQuery, Project project)
        {
            var result = new ReallocateMinPriceDTO()
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

            result.GPProjectDTO = GPProjectDTO.CreateFromResult(gpProject, gpOriginalQuery.FirstOrDefault()?.GPOriginalProject);
            var GPUnitDTOs = new List<GPUnitDTO>();
            foreach (var item in gpOriginalQuery)
            {
                var GpOri = item.GPOriginalUnit;
                var gpunit = gpUnits != null ? gpUnits.Where(o => o.UnitID == GpOri.UnitID).FirstOrDefault() : null;
                var unit = item.Unit;
                var unitStatus = item.UnitStatus;
                GPUnitDTO gpUnitDTO = GPUnitDTO.CreateFromResult(gpunit, GpOri, unit, unitStatus);
                GPUnitDTOs.Add(gpUnitDTO);
            }
            result.GPUnitDTOs = GPUnitDTOs.OrderBy(x => x.Unit?.UnitNo).ToList(); 
            result.BGNo = project.BG.BGNo;
            return result;
        }

        public static void SortBy(ReallocateMinPriceSortByParam sortByParam, ref IQueryable<GPReallocateMinPriceQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ReallocateMinPriceSortBy.Version:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.VersionCode);
                        else query = query.OrderByDescending(o => o.GPVersion.VersionCode);
                        break;
                    case ReallocateMinPriceSortBy.Remark:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.VersionRemark);
                        else query = query.OrderByDescending(o => o.GPVersion.VersionRemark);
                        break;
                    case ReallocateMinPriceSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Projetc.ProjectNo);
                        else query = query.OrderByDescending(o => o.Projetc.ProjectNo);
                        break;
                    case ReallocateMinPriceSortBy.Created:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GPVersion.Created);
                        else query = query.OrderByDescending(o => o.GPVersion.Created);
                        break;
                    case ReallocateMinPriceSortBy.Updated:
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
    }

    public class GPReallocateMinPriceQueryResult
    {
        public Project Projetc { get; set; }
        public string Version { get; set; }
        public string Remark { get; set; }
        public User UpdatedBy { get; set; }
        public User CreatedBy { get; set; }
        public GPVersion GPVersion { get; set; }
    }
    public class GPReallocateMinPriceQuery
    {
        public GPOriginalProject GPOriginalProject { get; set; }
        public GPOriginalUnit GPOriginalUnit { get; set; }
        public Unit Unit { get; set; }
        public VWUnitStatus UnitStatus { get; set;}
    }
}
