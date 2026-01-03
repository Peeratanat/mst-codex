using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class FloorDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อชี่น (TH)
        /// </summary>
        [Description("ชื่อชี่น (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อชี่น (EN)
        /// </summary>
        [Description("ชื่อชี่น (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// รายละเอียด
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Due โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? DueTransferDate { get; set; }

        public static FloorDTO CreateFromModel(Floor model)
        {
            if (model != null)
            {
                var result = new FloorDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    Description= model.Description,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    DueTransferDate = model.DueTransferDate
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static FloorDTO CreateFromQueryResult(FloorQueryResult model)
        {
            if (model != null)
            {
                var result = new FloorDTO()
                {
                    Id = model.Floor.ID,
                    NameEN = model.Floor.NameEN,
                    NameTH = model.Floor.NameTH,
                    Description = model.Floor.Description,
                    Updated = model.Floor.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    DueTransferDate = model.Floor.DueTransferDate
                    
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(FloorSortByParam sortByParam, ref IQueryable<FloorQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FloorSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameTH);
                        else query = query.OrderByDescending(o => o.Floor.NameTH);
                        break;
                    case FloorSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameEN);
                        else query = query.OrderByDescending(o => o.Floor.NameEN);
                        break;
                    case FloorSortBy.Description:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.Description);
                        else query = query.OrderByDescending(o => o.Floor.Description);
                        break;
                    case FloorSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.Updated);
                        else query = query.OrderByDescending(o => o.Floor.Updated);
                        break;
                    case FloorSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Floor.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Floor.NameTH);
            }
        }

        public void ToModel(ref Floor model)
        {
            model.NameEN = this.NameEN;
            model.NameTH = this.NameTH;
            model.Description = this.Description;
            model.DueTransferDate = this.DueTransferDate;
        }
    }
    public class FloorQueryResult
    {
        public Floor Floor { get; set; }
        public Tower Tower { get; set; }
        public Project Project { get; set; }
        public User UpdatedBy { get; set; }
    }
}
