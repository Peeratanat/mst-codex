using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class dbqTransferPromotionRequestItemForDelivery
    {
        public Guid? ID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }
        public Guid? TransferPromotionRequestID { get; set; }
        public Guid? TransferPromotionItemID { get; set; }
        public Guid? TransferPromotionID { get; set; }
        public Guid? QuotationTransferPromotionItemID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? EstimateRequestDate { get; set; }
        public string PRNo { get; set; }
        public string DenyRemark { get; set; }
        public string RequestNo { get; set; }
        public Guid? MainPromotionItemID { get; set; }
        public string NameTH { get; set; }
        public int? TransferPromotionQuantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public string MaterialGroupKey { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string UnitNo { get; set; }
        public string ProjectNo { get; set; }
        public bool? NoRequest { get; set; }
        public bool? IsSelected { get; set; }
        public int? RequestQuantity { get; set; }
        public int? RemainingRequestQuantity { get; set; }
        public string DeliveryNo { get; set; }
        public string UpdatedByDisplayName { get; set; }
        public string UnitTH { get; set; }
        public string ListTrId { get; set; }

    }
}
