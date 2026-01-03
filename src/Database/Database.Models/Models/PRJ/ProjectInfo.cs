using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Table("ProjectInfo", Schema = Schema.PROJECT)]
    public class ProjectInfo 
    {
        public Guid ID { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string BrandName { get; set; }
        public string ThumbnailURL { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string AdminDescription { get; set; }
        public string WebURL { get; set; }
        public string MapImageUrl { get; set; }
        public string ProjectType { get; set; }
        public string UnitTypeDisplay { get; set; }
        public string SellingPriceDisplay { get; set; }
        public decimal? SellingPriceMin { get; set; }
        public decimal? SellingPriceMax { get; set; }
        public string SellingPricePerSQM { get; set; }
        public string ProjectArea { get; set; }
        public string TotalUnit { get; set; }
        public string TotalParking { get; set; }
        public string UnitTypeDescription { get; set; }
        public string UsableArea { get; set; }
        public string AddressDisplay { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }
    }

}
