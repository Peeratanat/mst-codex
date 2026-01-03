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
    public class MasterCenterGroupDTO : BaseDTO
    {
        /// <summary>
        /// รหัส กลุ่มของข้อมูลทั่วไป
        /// </summary>
        [Description("รหัส กลุ่มของข้อมูลทั่วไป")]
        public string Key { get; set; }
        /// <summary>
        /// ชื่อ กลุ่มของข้อมูลทั่วไป
        /// </summary>
        [Description("ชื่อ กลุ่มของข้อมูลทั่วไป")]
        public string Name { get; set; }

        public static MasterCenterGroupDTO CreateFromModel(MasterCenterGroup model)
        {
            if (model != null)
            {
                var result = new MasterCenterGroupDTO()
                {
                    Key = model.Key,
                    Name = model.Name,
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

        public static MasterCenterGroupDTO CreateFromQueryResult(MasterCenterGroupQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterCenterGroupDTO()
                {
                    Key = model.MasterCenterGroup.Key,
                    Name = model.MasterCenterGroup.Name,
                    Updated = model.MasterCenterGroup.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterCenterGroupSortByParam sortByParam, ref IQueryable<MasterCenterGroupQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterCenterGroupSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenterGroup.Name);
                        else query = query.OrderByDescending(o => o.MasterCenterGroup.Name);
                        break;
                    case MasterCenterGroupSortBy.Key:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenterGroup.Key);
                        else query = query.OrderByDescending(o => o.MasterCenterGroup.Key);
                        break;
                    case MasterCenterGroupSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterCenterGroup.Updated);
                        else query = query.OrderByDescending(o => o.MasterCenterGroup.Updated);
                        break;
                    case MasterCenterGroupSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterCenterGroup.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterCenterGroup.Name);
            }
        }

        public async Task ValidateAsync(DatabaseContext db, bool isEdit = false)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Key))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterCenterGroupDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!isEdit)
            {
                var checkUniqueKey = await db.MasterCenterGroups.Where(o => o.Key == this.Key).CountAsync() > 0;
                if (checkUniqueKey)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterCenterGroupDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Key);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterCenterGroupDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!isEdit)
            {
                var checkUniqueName = await db.MasterCenterGroups.Where(o => o.Name == this.Name).CountAsync() > 0;
                if (checkUniqueName)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterCenterGroupDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Key);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterCenterGroup model)
        {
            model.Key = this.Key;
            model.Name = this.Name;
        }
    }

    public class MasterCenterGroupQueryResult
    {
        public MasterCenterGroup MasterCenterGroup { get; set; }
        public User UpdatedBy { get; set; }
    }
}
