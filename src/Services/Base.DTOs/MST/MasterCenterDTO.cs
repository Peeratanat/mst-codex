using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class MasterCenterDTO : BaseDTO
    {
        /// <summary>
        /// กลุ่มของข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public MasterCenterGroupListDTO MasterCenterGroup { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        [Description("ชื่อ")]
        public string Name { get; set; }
        /// <summary>
        /// ชื่อ (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อ (ภาษาอังกฤษ)")]
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป FullName
        /// </summary>
        [Description("ชื่อ FullName")]
        public string FullName { get; set; }
        /// <summary>
        /// รหัส
        /// </summary>
        [Description("รหัส")]
        public string Key { get; set; }
        /// <summary>
        /// Active อยู่หรือไม่
        /// </summary>
        public bool IsActive { get; set; }

        public static MasterCenterDTO CreateFromModel(MasterCenter model)
        {
            if (model != null)
            {
                var result = new MasterCenterDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    NameEN = model.NameEN,
                    FullName = model.Name + ((!string.IsNullOrEmpty(model.NameEN)) ? "/" + model.NameEN : ""),
                    Key = model.Key,
                    MasterCenterGroup = MasterCenterGroupListDTO.CreateFromModel(model.MasterCenterGroup),
                    Order = model.Order,
                    IsActive = model.IsActive,
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

        public static MasterCenterDTO CreateFromQueryResult(MasterCenterQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterCenterDTO()
                {
                    Id = model.MasterCenter.ID,
                    Name = model.MasterCenter.Name,
                    NameEN = model.MasterCenter.NameEN,
                    Key = model.MasterCenter.Key,
                    MasterCenterGroup = MasterCenterGroupListDTO.CreateFromModel(model.MasterCenterGroup),
                    Order = model.MasterCenter.Order,
                    IsActive = model.MasterCenter.IsActive,
                    Updated = model.MasterCenter.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterCenterSortByParam sortByParam, ref IQueryable<MasterCenterQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterCenterSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.Name);
                        else query = query.OrderByDescending(o => o.MasterCenter.Name);
                        break;
                    case MasterCenterSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.NameEN);
                        else query = query.OrderByDescending(o => o.MasterCenter.NameEN);
                        break;
                    case MasterCenterSortBy.Key:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.Key);
                        else query = query.OrderByDescending(o => o.MasterCenter.Key);
                        break;
                    case MasterCenterSortBy.MasterCenterGroup:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenterGroup.Name);
                        else query = query.OrderByDescending(o => o.MasterCenterGroup.Name);
                        break;
                    case MasterCenterSortBy.Order:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.Order);
                        else query = query.OrderByDescending(o => o.MasterCenter.Order);
                        break;
                    case MasterCenterSortBy.IsActive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.IsActive);
                        else query = query.OrderByDescending(o => o.MasterCenter.IsActive);
                        break;
                    case MasterCenterSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenter.Updated);
                        else query = query.OrderByDescending(o => o.MasterCenter.Updated);
                        break;
                    case MasterCenterSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterCenter.Name);
                        break;

                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterCenter.Name);
            }
        }

        public void ToModel(ref MasterCenter model)
        {
            model.Key = this.Key;
            model.Name = this.Name;
            model.NameEN = this.NameEN;
            model.IsActive = this.IsActive;
            model.MasterCenterGroupKey = this.MasterCenterGroup.Key;
            model.Order = this.Order;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Key))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterCenterDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                var checkUnique = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == this.MasterCenterGroup.Key && o.Key == this.Key && o.ID != this.Id).AnyAsync();
                if (checkUnique)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterCenterDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Key);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterCenterDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                var checkUniqueName = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == this.MasterCenterGroup.Key && o.Name == this.Name && o.ID != this.Id).AnyAsync();
                if (checkUniqueName)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterCenterDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Name);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }

    public class MasterCenterQueryResult
    {
        public MasterCenter MasterCenter { get; set; }
        public MasterCenterGroup MasterCenterGroup { get; set; }
        public User UpdatedBy { get; set; }
    }
}
