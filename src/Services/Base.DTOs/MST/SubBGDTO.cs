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
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class SubBGDTO : BaseDTO
    {
        /// <summary>
        ///  รหัส SubBG
        /// </summary>
        [Description("รหัส SubBG")]
        public string SubBGNo { get; set; }
        /// <summary>
        /// ชื่อ SubBG
        /// </summary>
        [Description("ชื่อ SubBG")]
        public string Name { get; set; }
        /// <summary>
        /// BG
        /// </summary>
        public BGListDTO BG { get; set; }

        public static SubBGDTO CreateFromModel(SubBG model)
        {
            if (model != null)
            {
                var result = new SubBGDTO()
                {
                    Id = model.ID,
                    SubBGNo = model.SubBGNo,
                    Name = model.Name,
                    BG = BGListDTO.CreateFromModel(model.BG),
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

        public static SubBGDTO CreateFromQueryResult(SubBGQueryResult model)
        {
            if (model != null)
            {
                var result = new SubBGDTO()
                {
                    Id = model.SubBG.ID,
                    SubBGNo = model.SubBG.SubBGNo,
                    Name = model.SubBG.Name,
                    BG = BGListDTO.CreateFromModel(model.BG),
                    Updated = model.SubBG.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SubBGSortByParam sortByParam, ref IQueryable<SubBGQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case SubBGSortBy.SubBGNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubBG.SubBGNo);
                        else query = query.OrderByDescending(o => o.SubBG.SubBGNo);
                        break;
                    case SubBGSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubBG.Name);
                        else query = query.OrderByDescending(o => o.SubBG.Name);
                        break;
                    case SubBGSortBy.BG:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BG.Name);
                        else query = query.OrderByDescending(o => o.SubBG.Name);
                        break;
                    case SubBGSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubBG.Updated);
                        else query = query.OrderByDescending(o => o.SubBG.Updated);
                        break;
                    case SubBGSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubBG.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.SubBG.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.SubBG.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.SubBG.Name);
            }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.SubBGNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubBGDTO.SubBGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.SubBGNo.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(SubBGDTO.SubBGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueSubBGNo = this.Id != (Guid?)null ? await db.SubBGs.Where(o => o.SubBGNo == this.SubBGNo && o.ID != this.Id).CountAsync() > 0 : await db.SubBGs.Where(o => o.SubBGNo == this.SubBGNo).CountAsync() > 0;
            if (checkUniqueSubBGNo)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubBGDTO.SubBGNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.SubBGNo);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubBGDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Name.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(SubBGDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref SubBG model)
        {
            model.SubBGNo = this.SubBGNo;
            model.Name = this.Name;
            model.BGID = this.BG?.Id;
        }
    }
    public class SubBGQueryResult
    {
        public SubBG SubBG { get; set; }
        public BG BG { get; set; }
        public User UpdatedBy { get; set; }
    }
}
