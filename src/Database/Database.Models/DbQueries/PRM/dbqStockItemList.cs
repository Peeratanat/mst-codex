using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class dbqStockItemList //: BaseDbQueries
    {
        /// <summary>
        /// nOrder
        /// </summary>
        public long? nOrder { get; set; }
        /// <summary>
        /// StockClearId  Id สำหรับส่งกลับ Stock
        /// </summary>
        public string StockClearId { get; set; }
        /// <summary>
        /// MasterItemsId
        /// </summary>
        public int? MasterItemsId { get; set; }
        /// <summary>
        /// MaterialNo   
        /// </summary>
        public string MaterialNo { get; set; }
        /// <summary>
        /// SerialNumber
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// ItemNo
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// ItemName
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// itemCountingType
        /// </summary>
        public string itemCountingType { get; set; }
        /// <summary>
        /// CountMethod
        /// </summary>
        public string CountMethod { get; set; }

        /// <summary>
        /// IsSubstitution 1=เทียบเท่า 0=ปกติ
        /// </summary>
        public bool IsSubstitution { get; set; }
        /// <summary>
        /// Tranfertype
        /// </summary>
        public string Tranfertype { get; set; }

        /// <summary>
        /// TransferAmt  จำนวนที่จ่าย
        /// </summary>
        public int TransferAmt { get; set; }

        /// <summary>
        /// FinalPricePerUnit  ราคาสุกท้าย
        /// </summary>
        public Decimal FinalPricePerUnit { get; set; }


        /// <summary>
        /// รหัสสินค้าเก่า
        /// </summary>
        public string MaterialNoOld { get; set; }
    }
}
