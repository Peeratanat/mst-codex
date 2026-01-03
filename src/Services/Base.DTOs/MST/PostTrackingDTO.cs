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
    public class PostTrackingDTO : BaseDTO
    {
        /// <summary>
        /// รหัส PostTracking
        /// </summary>
        [Description("รหัส PostTracking")]
        public string PostTrackingNo { get; set; }
        /// <summary>
        /// ถูกใช้งานแล้ว?
        /// </summary>
        public bool? IsUsed { get; set; }

        public static PostTrackingDTO CreateFromModel(PostTracking model)
        {
            if (model != null)
            {
                var result = new PostTrackingDTO()
                {
                    Id = model.ID,
                    PostTrackingNo = model.PostTrackingNo,
                    IsUsed = model.IsUsed
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static PostTrackingDTO CreateFromQueryResult(PostTrackingQueryResult model)
        {
            if (model != null)
            {
                var result = new PostTrackingDTO()
                {
                    Id = model.PostTracking.ID,
                    PostTrackingNo = model.PostTracking.PostTrackingNo,
                    IsUsed = model.PostTracking.IsUsed
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(PostTrackingSortByParam sortByParam, ref IQueryable<PostTrackingQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PostTrackingSortBy.PostTrackingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PostTracking.PostTrackingNo);
                        else query = query.OrderByDescending(o => o.PostTracking.PostTrackingNo);
                        break;
                    case PostTrackingSortBy.IsUsed:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PostTracking.IsUsed);
                        else query = query.OrderByDescending(o => o.PostTracking.IsUsed);
                        break;
                    default:
                        query = query.OrderBy(o => o.PostTracking.PostTrackingNo);
                        break;

                }
            }
            else
            {
                query = query.OrderBy(o => o.PostTracking.PostTrackingNo);
            }
        }
        public void ToModel(ref PostTracking model)
        {
            model.PostTrackingNo = this.PostTrackingNo;
            model.IsUsed = this.IsUsed;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.PostTrackingNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(PostTrackingDTO.PostTrackingNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.PostTrackingNo.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(PostTrackingDTO.PostTrackingNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            var checkUniquePostTrackingNo = this.Id != (Guid?)null ? await db.PostTrackings.Where(o => o.PostTrackingNo == this.PostTrackingNo && o.ID != this.Id).CountAsync() > 0 : await db.PostTrackings.Where(o => o.PostTrackingNo == this.PostTrackingNo).CountAsync() > 0;
            if (checkUniquePostTrackingNo)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(PostTrackingDTO.PostTrackingNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.PostTrackingNo);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
           
            if (this.IsUsed == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(PostTrackingDTO.IsUsed)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
    public class PostTrackingQueryResult
    {
        public PostTracking PostTracking { get; set; }
    }
}
