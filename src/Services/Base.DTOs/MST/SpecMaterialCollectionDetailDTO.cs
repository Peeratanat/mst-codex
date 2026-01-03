using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Base.DTOs.MST
{
    public class SpecMaterialCollectionDetailDTO : BaseDTO
    {
        public MasterCenterDropdownDTO SpecMaterialGroup { get; set; }
        public SpecMaterialItemDTO SpecMaterialItem { get; set; }
        public SpecMaterialCollectionDTO SpecMaterialCollection { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSelcted { get; set; }
        public ModelDropdownDTO Model { get; set; }

        public static SpecMaterialCollectionDetailDTO CreatedFromModel(SpecMaterialCollectionDetail model, DatabaseContext db)
        {
            if (model != null)
            {
                //var detail = db.SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == model.SpecMaterialCollection.ID).FirstOrDefault();
                SpecMaterialCollectionDetailDTO result = new SpecMaterialCollectionDetailDTO()
                {
                    Id = model.ID,
                    IsActive = model.IsActive,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,

                };
                return result;
            }
            else
            {
                return null;
            }
        }


        public static SpecMaterialCollectionDetailDTO CreateFromQueryResult(SpecMaterialCollectionDetailQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var detail = db.SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == model.SpecMaterialCollection.ID).FirstOrDefault();
                //SpecMaterialCollectionDetailDTO result = new SpecMaterialCollectionDetailDTO()
                //{
                //    Id = model.SpecMaterialCollectionDetail.ID,
                //    SpecMaterialGroup = MasterCenterDropdownDTO.CreateFromModel(model.Group),
                //    SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(model.SpecMaterialItem),
                //    SpecMaterialCollection = SpecMaterialCollectionDTO.CreateFromModel(model.SpecMaterialCollection, db),
                //    IsActive = model.SpecMaterialCollectionDetail.IsActive,
                //    Updated = model.SpecMaterialCollectionDetail.Updated,
                //    UpdatedBy = model.SpecMaterialCollectionDetail.UpdatedBy.DisplayName,
                //};
                SpecMaterialCollectionDetailDTO result = new SpecMaterialCollectionDetailDTO();
                //{
                result.Id = model.SpecMaterialCollectionDetail.ID;
                result.SpecMaterialGroup = MasterCenterDropdownDTO.CreateFromModel(model.Group);
                result.SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(model.SpecMaterialItem);
                result.SpecMaterialCollection = SpecMaterialCollectionDTO.CreateFromModel(model.SpecMaterialCollection, db);
                result.IsActive = model.SpecMaterialCollectionDetail.IsActive;
                result.Updated = model.SpecMaterialCollectionDetail.Updated;
                result.UpdatedBy = model.SpecMaterialCollectionDetail.UpdatedBy?.DisplayName;
                //result.IsSelcted
                //};
                return result;
            }
            else
            {
                return null;
            }
        }


        public static void SortBy(SpecMaterialCollectionDetailSortByParam sortByParam, ref IQueryable<SpecMaterialCollectionDetailQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case SpecMaterialCollectionDetailSortBy.Group:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Group.Name);
                        else query = query.OrderByDescending(o => o.SpecMaterialCollectionDetail.SpecMaterialGroup.Name);
                        break;
                    case SpecMaterialCollectionDetailSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.Name);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.Name);
                        break;
                    case SpecMaterialCollectionDetailSortBy.ItemDesc:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.ItemDescription);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.ItemDescription);
                        break;
                    case SpecMaterialCollectionDetailSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.NameEN);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.NameEN);
                        break;
                    case SpecMaterialCollectionDetailSortBy.ItemDescEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.ItemDescriptionEN);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.ItemDescriptionEN);
                        break;
                    case SpecMaterialCollectionDetailSortBy.updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.Updated);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.Updated);
                        break;
                    case SpecMaterialCollectionDetailSortBy.updatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.UpdatedBy.DisplayName);
                        break;

                }
            }
            else
            {
                query = query.OrderBy(o => o.SpecMaterialItem.SpecMaterialGroup.Order).ThenBy(o => o.SpecMaterialItem.Name);
            }
        }
    }


    public class SpecMaterialCollectionDetailQueryResult
    {
        public SpecMaterialCollection SpecMaterialCollection { get; set; }
        public MasterCenter SpecMaterialGroup { get; set; }
        public SpecMaterialItem SpecMaterialItem { get; set; }
        public Model Model { get; set; }
        public MasterCenter Group { get; set; }
        public SpecMaterialCollectionDetail SpecMaterialCollectionDetail { get; set; }
        public User UpdatedBy { get; set; }
    }
}
