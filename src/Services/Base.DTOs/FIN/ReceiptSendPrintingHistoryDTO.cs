using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;
using Base.DTOs.USR;

namespace Base.DTOs.FIN
{
    public class ReceiptSendPrintingHistoryDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูล ReceiptHeader
        /// </summary>
        public ReceiptHeaderDTO ReceiptHeader { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string LotNo { get; set; }

        /// <summary>
        /// จำนวนเใบเสร็จ
        /// </summary>
        public int TotalReceipts { get; set; }

        /// <summary>
        /// วันที่ Exportใบเสร็จ
        /// </summary>
        public DateTime ExportDate { get; set; }

        /// <summary>
        /// Export โดย
        /// </summary>
        public USR.UserDTO ExportBy { get; set; }

        /// <summary>
        /// วันที่ส่งโรงพิมพ์
        /// </summary>
        public DateTime? Senddate { get; set; }

        public static void SortBy(ReceiptSendPrintingHistorySortByParam sortByParam, ref IQueryable<ReceiptSendPrintingHistory> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ReceiptSendPrintingHistorySortBy.LotNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LotNo);
                        else query = query.OrderByDescending(o => o.LotNo);
                        break;
                    case ReceiptSendPrintingHistorySortBy.TotalReceipts:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TotalRecord);
                        else query = query.OrderByDescending(o => o.TotalRecord);
                        break;
                    case ReceiptSendPrintingHistorySortBy.ExportDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ExportDate);
                        else query = query.OrderByDescending(o => o.ExportDate);
                        break;
                    case ReceiptSendPrintingHistorySortBy.ExportBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.CreatedBy.DisplayName);
                        break;
                    case ReceiptSendPrintingHistorySortBy.SendDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SendDate);
                        else query = query.OrderByDescending(o => o.SendDate);
                        break;
                    default:
                        query = query.OrderBy(o => o.ExportDate).ThenByDescending(o => o.LotNo);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.ExportDate).ThenByDescending(o=>o.LotNo);
            }
        }


        public static ReceiptSendPrintingHistoryDTO CreateFromQueryResult(ReceiptSendPrintingHistory model)
        {
            if (model != null)
            {
                var result = new ReceiptSendPrintingHistoryDTO()
                {
                    Id = model.ID,
                    LotNo = model.LotNo,
                    ExportBy = UserDTO.CreateFromModel(model.CreatedBy),
                    ExportDate = model.ExportDate,
                    Senddate = model.SendDate,
                    TotalReceipts = model.TotalRecord ?? 0,
                };
                return result;
            }
            else
            {
                return null;
            }
        }


    }
}
