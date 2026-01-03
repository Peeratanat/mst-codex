using Database.Models;
using Database.Models.PRJ;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class TowerDTO : BaseDTO
    {
        /// <summary>
        /// รหัสอาคาร
        /// </summary>
        [Description("รหัสอาคาร")]
        public string Code { get; set; }
        /// <summary>
        /// อาคารเลขที่ (TH)
        /// </summary>
        [Description("อาคารเลขที่ (TH)")]
        public string NoTH { get; set; }
        /// <summary>
        /// อาคารเลขที่ (EN)
        /// </summary>
        [Description("อาคารเลขที่ (EN)")]
        public string NoEN { get; set; }
        /// <summary>
        /// ชื่ออาคารชุด
        /// </summary>
        public string CondominiumName { get; set; }
        /// <summary>
        /// ทะเบียนอาคารชุดเลขที่
        /// </summary>
        [Description("ทะเบียนอาคารชุดเลขที่")]
        public string CondominiumNo { get; set; }
        /// <summary>
        /// คำอธิบายตึก
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// จำนวนชั้นทั้งหมด
        /// </summary>
        public double? FloorCount { get; set; }

        public async static Task<TowerDTO> CreateFromModelAsync(Tower model,DatabaseContext db)
        {
            if (model != null)
            {
                var result = new TowerDTO()
                {
                    Id = model.ID,
                    Code = model.TowerCode,
                    NoTH = model.TowerNoTH,
                    NoEN = model.TowerNoEN,
                    Description = model.TowerDescription,
                    CondominiumName=model.CondominiumName,
                    CondominiumNo=model.CondominiumNo,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    FloorCount = await db.Floors.Where(o => o.TowerID == model.ID && o.ProjectID == model.ProjectID).CountAsync()
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TowerDTO CreateFromQueryResult(TowerQueryResult model)
        {
            if (model != null)
            {
                var result = new TowerDTO()
                {
                    Id = model.Tower.ID,
                    Code = model.Tower.TowerCode,
                    NoEN = model.Tower.TowerNoEN,
                    NoTH = model.Tower.TowerNoTH,
                    Description = model.Tower.TowerDescription,
                    CondominiumName = model.Tower.CondominiumName,
                    CondominiumNo = model.Tower.CondominiumNo,
                    FloorCount = model.FloorCount,
                    Updated = model.Tower.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(TowerSortByParam sortByParam, ref IQueryable<TowerQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TowerSortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerCode);
                        else query = query.OrderByDescending(o => o.Tower.TowerCode);
                        break;
                    case TowerSortBy.FloorCount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FloorCount);
                        else query = query.OrderByDescending(o => o.FloorCount);
                        break;
                    case TowerSortBy.NoTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerNoTH);
                        else query = query.OrderByDescending(o => o.Tower.TowerNoTH);
                        break;
                    case TowerSortBy.NoEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerNoEN);
                        else query = query.OrderByDescending(o => o.Tower.TowerNoEN);
                        break;
                    case TowerSortBy.CondominiumName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.CondominiumName);
                        else query = query.OrderByDescending(o => o.Tower.CondominiumName);
                        break;
                    case TowerSortBy.CondominiumNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.CondominiumNo);
                        else query = query.OrderByDescending(o => o.Tower.CondominiumNo);
                        break;
                    case TowerSortBy.Description:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerDescription);
                        else query = query.OrderByDescending(o => o.Tower.TowerDescription);
                        break;
                    case TowerSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case TowerSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.Updated);
                        else query = query.OrderByDescending(o => o.Tower.Updated);
                        break;
                    default:
                        query = query.OrderBy(o => o.Tower.TowerCode);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Tower.TowerCode);
            }
        }

        public void ToModel(ref Tower model)
        {
            model.TowerCode = this.Code;
            model.TowerNoEN = this.NoEN;
            model.TowerNoTH = this.NoTH;
            model.TowerDescription = this.Description;
            model.CondominiumName = this.CondominiumName;
            model.CondominiumNo = this.CondominiumNo;
        }
    }

    public class TowerQueryResult
    {
        public Tower Tower { get; set; }
        public int FloorCount { get; set; }
        public User UpdatedBy { get; set; }
    }
}
