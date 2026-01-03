using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.PRJ;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// Model=PreSalePromotionRequest
    /// </summary>
    public class PreSalePromotionRequestListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// รายการเลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public List<PRJ.UnitDropdownDTO> Units { get; set; }
        /// <summary>
        /// Master Promotion ก่อนขาย
        /// Promotion/api/MasterPreSalePromotions/DropdownList
        /// </summary>
        public MasterPreSalePromotionDropdownDTO MasterPreSalePromotion { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? PRCompletedDate { get; set; }
        /// <summary>
        /// สถานะ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PromotionRequestPRStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionRequestPRStatus { get; set; }
        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? RequestDate { get; set; }

        public static PreSalePromotionRequestListDTO CreateFromQuery(dbqPreSalePromotionRequestList model, DatabaseContext DB)
        {
            if (model != null)
            {
                //var modelProject = DB.Projects.Where(o => o.ID == model.ProjectID).FirstOrDefault();
                var modelMasterPreSalePromotion = DB.MasterPreSalePromotions.Where(o => o.ID == model.MasterPreSalePromotionID).FirstOrDefault();
                var modelPromotionRequestPRStatus = DB.MasterCenters.Where(o => o.ID == model.PromotionRequestPRStatusMasterCenterID).FirstOrDefault();

                var result = new PreSalePromotionRequestListDTO
                {
                    Id = model.PreSalePromotionRequestID,
                    //Project = ProjectDropdownDTO.CreateFromModel(modelProject),
                    Project = new ProjectDropdownDTO()
                    {
                        Id = model.ProjectID,
                        ProjectNo = model.ProjectNo,
                        ProjectNameTH = model.ProjectNameTH
                    },
                    MasterPreSalePromotion = MasterPreSalePromotionDropdownDTO.CreateFromModel(modelMasterPreSalePromotion),
                    PromotionRequestPRStatus = MST.MasterCenterDropdownDTO.CreateFromModel(modelPromotionRequestPRStatus),
                    RequestDate = model.RequestDate,
                    PRCompletedDate = model.PRCompletedDate,
                    Updated = model.Updated,
                    UpdatedBy = model.DisplayName
                };

                List<UnitDropdownDTO> units = new List<UnitDropdownDTO>();
                string[] lstUnitNo = model.UnitNo.Split(',');
                string[] lstUnitId = model.UnitID.Split(',');
                for (int i = 0; i <= lstUnitNo.Length - 1; i++)
                {
                    //var modelUnit = DB.Units.Where(o => o.ID == new Guid(lstUnit[i])).FirstOrDefault();
                    var unit = new UnitDropdownDTO
                    {
                        Id = new Guid(lstUnitId[i]),
                        UnitNo = lstUnitNo[i]
                    };

                    units.Add(unit);
                }

                result.Units = units;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static PreSalePromotionRequestListDTO CreateFromQueryResult(PreSalePromotionQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestListDTO
                {
                    Id = model.PreSalePromotionRequest.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.PreSalePromotionRequest.Project),
                    //Units = await DB.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == model.ID).Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToListAsync(),
                    MasterPreSalePromotion = MasterPreSalePromotionDropdownDTO.CreateFromModel(model.PreSalePromotionRequest.MasterPreSalePromotion),
                    PRCompletedDate = model.PreSalePromotionRequest.PRCompletedDate,
                    PromotionRequestPRStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PreSalePromotionRequest.PromotionRequestPRStatus),
                    RequestDate = model.PreSalePromotionRequest.RequestDate,
                    Updated = model.PreSalePromotionRequest.Updated,
                    UpdatedBy = model.PreSalePromotionRequest.UpdatedBy?.DisplayName
                };

                var units = DB.PreSalePromotionRequestUnits.Include(o => o.Unit).Where(o => o.PreSalePromotionRequestID == model.PreSalePromotionRequest.ID).ToList();
                result.Units = units.Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<PreSalePromotionRequestListDTO> CreateFromModelAsync(PreSalePromotionRequest model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestListDTO
                {
                    Id = model.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Units = await DB.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == model.ID).Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToListAsync(),
                    MasterPreSalePromotion = MasterPreSalePromotionDropdownDTO.CreateFromModel(model.MasterPreSalePromotion),
                    PRCompletedDate = model.PRCompletedDate,
                    PromotionRequestPRStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionRequestPRStatus),
                    RequestDate = model.RequestDate,
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

        public static void SortBy(PreSalePromotionRequestListSortByParam sortByParam, ref IQueryable<PreSalePromotionQueryResult> query)
        {
            IOrderedQueryable<PreSalePromotionQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PreSalePromotionRequestListSortBy.Project:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.ProjectID);
                        break;
                    case PreSalePromotionRequestListSortBy.Unit:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.RequestUnits[0].UnitID);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.RequestUnits[0].UnitID);
                        break;
                    case PreSalePromotionRequestListSortBy.MasterPreSalePromotions_PromotionNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.MasterPreSalePromotion.PromotionNo);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.MasterPreSalePromotion.PromotionNo);
                        break;
                    case PreSalePromotionRequestListSortBy.PRCompletedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.PRCompletedDate);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.PRCompletedDate);
                        break;
                    case PreSalePromotionRequestListSortBy.PromotionRequestPRStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.PromotionRequestPRStatus.Key);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.PromotionRequestPRStatus.Key);
                        break;
                    case PreSalePromotionRequestListSortBy.RequestDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.RequestDate);
                        else orderQuery = query.OrderByDescending(o => o.PreSalePromotionRequest.RequestDate);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.ProjectID);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.PreSalePromotionRequest.ProjectID);
            }

            orderQuery.ThenBy(o => o.PreSalePromotionRequest.ID);
            query = orderQuery;
        }

        public static void SortBy(PreSalePromotionRequestListSortByParam sortByParam, ref List<PreSalePromotionRequestListDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PreSalePromotionRequestListSortBy.Project:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Project?.ProjectNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Project?.ProjectNo).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.Unit:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Units?.Select(p => p.UnitNo).FirstOrDefault()).ToList().OrderBy(q => q.Units.Select(r => r.UnitNo).FirstOrDefault()).ToList();
                        else listDTOs = listDTOs.OrderBy(o => o.Units?.Select(p => p.UnitNo).FirstOrDefault()).ToList().OrderByDescending(q => q.Units.Select(r => r.UnitNo).FirstOrDefault()).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.MasterPreSalePromotions_PromotionNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.MasterPreSalePromotion.PromotionNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.MasterPreSalePromotion.PromotionNo).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.PRCompletedDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PRCompletedDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PRCompletedDate).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.PromotionRequestPRStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionRequestPRStatus?.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionRequestPRStatus?.Name).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.RequestDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.RequestDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.RequestDate).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UpdatedBy).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        listDTOs = listDTOs.OrderByDescending(o => o.RequestDate).ToList();
                        break;
                }
            }
            else
            {
                listDTOs = listDTOs.OrderByDescending(o => o.RequestDate).ToList();
            }
        }

        public class PreSalePromotionQueryResult
        {
            public PreSalePromotionRequest PreSalePromotionRequest { get; set; }
        }
    }
}

