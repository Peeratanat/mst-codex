using Database.Models;
using Database.Models.LOG;
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
    public class LineITSupportDTO : BaseDTO
    {
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string ReplyToken { get; set; }
        public string SourceUserId { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public string MessageReply { get; set; }

        public static LineITSupportDTO CreateFromQueryResult(LineITSupportQueryResult model)
        {
            if (model != null)
            {
                var result = new LineITSupportDTO()
                {
                    CreateBy = model.CreateBy != null ? model.CreateBy?.DisplayName : model.LineItSuppportAi.createby,
                    CreateDate = model.LineItSuppportAi.createdate,
                    Id = model.LineItSuppportAi.id,
                    MessageReply = model.LineItSuppportAi.message_reply,
                    MessageText = model.LineItSuppportAi.message_text,
                    MessageType = model.LineItSuppportAi.message_type,
                    ReplyToken = model.LineItSuppportAi.reply_token,
                    SourceUserId = model.LineItSuppportAi.source_userId
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LineITSupportSortByParam sortByParam, ref IQueryable<LineITSupportQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LineITSupportSortBy.reply_token:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LineItSuppportAi.reply_token);
                        else query = query.OrderByDescending(o => o.LineItSuppportAi.reply_token);
                        break;
                    case LineITSupportSortBy.createdBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreateBy.DisplayName);
                        else query = query.OrderByDescending(o => o.CreateBy.DisplayName);
                        break;
                    case LineITSupportSortBy.created:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LineItSuppportAi.createdate);
                        else query = query.OrderByDescending(o => o.LineItSuppportAi.createdate);
                        break;
                    case LineITSupportSortBy.message_reply:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LineItSuppportAi.message_reply);
                        else query = query.OrderByDescending(o => o.LineItSuppportAi.message_reply);
                        break;
                    case LineITSupportSortBy.message_text:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LineItSuppportAi.message_text);
                        else query = query.OrderByDescending(o => o.LineItSuppportAi.message_text);
                        break;
                    case LineITSupportSortBy.message_type:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LineItSuppportAi.message_type);
                        else query = query.OrderByDescending(o => o.LineItSuppportAi.message_type);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.LineItSuppportAi.createdate);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.LineItSuppportAi.createdate);
            }
        }
          
    }

    public class LineITSupportQueryResult
    {
        public LineItSupportAi LineItSuppportAi { get; set; }
        public User CreateBy { get; set; }
    }
}
