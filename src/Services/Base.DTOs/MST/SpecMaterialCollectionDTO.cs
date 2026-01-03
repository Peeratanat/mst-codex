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
using static Base.DTOs.MST.LetterOfGuaranteeDTO;

namespace Base.DTOs.MST
{
    public class SpecMaterialCollectionDTO : BaseDTO
    {

        public Guid? ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public MasterCenterDropdownDTO Group { get; set; }
        public ModelDropdownDTO Model { get; set; }
        public SpecMaterialItemDTO SpecMaterialItem { get; set; }
        public string ModelUse { get; set; }

        public static SpecMaterialCollectionDTO CreateFromModel(SpecMaterialCollection model, DatabaseContext DB)
        {
            if (model != null)
            {

                var Models = DB.Models.Where(o => o.SpecMaterialCollectionID == model.ID).FirstOrDefault();
                var Group = DB.SpecMaterialCollectionDetails.Include(o => o.SpecMaterialGroup).Where(o => o.SpecMaterialCollectionID == model.ID).Select(o => o.SpecMaterialGroup).FirstOrDefault();
                SpecMaterialCollectionDTO result = new SpecMaterialCollectionDTO()
                {
                    ID = model.ID,
                    Name = model.Name,
                    Model = ModelDropdownDTO.CreateFromModel(Models),
                    IsActive = model.IsActive,
                    Group = MasterCenterDropdownDTO.CreateFromModel(Group),
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static SpecMaterialCollectionDTO CreateFromQueryResult(SpecMaterialCollectionQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var detail = db.SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == model.SpecMaterialCollection.ID).FirstOrDefault();

                var models = db.Models.Where(o => o.SpecMaterialCollectionID == model.SpecMaterialCollection.ID).GroupBy(o => new { o.NameTH }).Select(o => o.FirstOrDefault()).ToList();
                SpecMaterialCollectionDTO result = new SpecMaterialCollectionDTO()
                {
                    ID = model.SpecMaterialCollection.ID,
                    Name = model.SpecMaterialCollection.Name,

                    //Model = ModelDropdownDTO.CreateFromModelList(models),
                    IsActive = model.SpecMaterialCollection.IsActive,
                    Group = MasterCenterDropdownDTO.CreateFromModel(model.Group),
                    SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(model.SpecMaterialItem),
                    Updated = model.SpecMaterialCollection.Updated,
                    UpdatedBy = model.SpecMaterialCollection?.UpdatedBy?.DisplayName,

                };


                var s = "";
                var c = 0;
                foreach (var m in models)
                {
                    if (m is not null)
                    {
                        if (models.Count > 0)
                        {
                            s = ", ";
                        }
                        result.ModelUse += m.NameTH ?? string.Empty;
                        if (c < models.Count - 1)
                        {
                            result.ModelUse += s;
                        }
                        c++;
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }
        public static SpecMaterialCollectionDTO CreateFromQueryAllResult(SpecMaterialCollectionQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                //var detail = db.SpecMaterialCollectionDetails.Where(o => o.SpecMaterialCollectionID == model.SpecMaterialCollection.ID).FirstOrDefault();
                SpecMaterialCollectionDTO result = new SpecMaterialCollectionDTO()
                {
                    Group = MasterCenterDropdownDTO.CreateFromModel(model.Group),
                    SpecMaterialItem = SpecMaterialItemDTO.CreateFromModel(model.SpecMaterialItem),
                    Updated = model.SpecMaterialItem.Updated,
                    UpdatedBy = model.SpecMaterialItem?.UpdatedBy?.DisplayName,

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SpecMaterialCollectionSortByParam sortByParam, ref IQueryable<SpecMaterialCollectionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case SpecMaterialCollectionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialCollection.Name);
                        else query = query.OrderByDescending(o => o.SpecMaterialCollection.Name);
                        break;
                    case SpecMaterialCollectionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialCollection.Updated);
                        else query = query.OrderByDescending(o => o.SpecMaterialCollection.Updated);
                        break;
                    case SpecMaterialCollectionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialCollection.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.SpecMaterialCollection.UpdatedBy.DisplayName);
                        break;

                }
            }
            else
            {
                query = query.OrderByDescending(o => o.SpecMaterialCollection.Created);
            }
        }
        public static void AllSortBy(SpecMaterialCollectionSortByParam sortByParam, ref IQueryable<SpecMaterialCollectionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case SpecMaterialCollectionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.Name);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.Name);
                        break;
                    case SpecMaterialCollectionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.Updated);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.Updated);
                        break;
                    case SpecMaterialCollectionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SpecMaterialItem.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.SpecMaterialItem.UpdatedBy.DisplayName);
                        break;

                }
            }
            else
            {
                query = query.OrderByDescending(o => o.SpecMaterialItem.Created).ThenBy(o => o.SpecMaterialItem.Created);
            }
        }

    }




    public class SpecMaterialCollectionQueryResult
    {
        public SpecMaterialCollection SpecMaterialCollection { get; set; }
        public MasterCenter SpecMaterialGroup { get; set; }
        public SpecMaterialItem SpecMaterialItem { get; set; }
        public Model Model { get; set; }
        public MasterCenter Group { get; set; }
        public SpecMaterialCollectionDetail SpecMaterialCollectionDetail { get; set; }
    }
}
