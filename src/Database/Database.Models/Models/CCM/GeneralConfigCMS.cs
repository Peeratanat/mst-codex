using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    
    [Table("GeneralConfigCMS", Schema = Schema.CMS)]
    public class GeneralConfigCMS : BaseEntityWithoutMigrate
    {


        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        public string LogoImageFilePath { get; set; }
        public string LogoImageFileName { get; set; }
        public string BackgroundImageFilePath { get; set; }
        public string BackgroundImageFileName { get; set; }
        public string BannerImageFilePath { get; set; }
        public string BannerImageFileName { get; set; }
        public string AnnimationFilePath { get; set; }
        public string AnnimationFileName { get; set; }
        public string PanoramaVieweFilePath { get; set; }
        public string PanoramaVieweFileName { get; set; }
        public string Remark { get; set; }
        public string MasterPlanFilePath { get; set; }
        public string MasterPlanFileName { get; set; }
        public int? MasterPlanRadius { get; set; }
        public int? MasterPlanRadiusBorder { get; set; }
        public int? MasterPlanThickness { get; set; }
        public int? MasterPlanThicknessBorder { get; set; }
        public int? MasterPlanTextTopAdjustX { get; set; }
        public int? MasterPlanTextTopAdjustY { get; set; }
        public double? MasterPlanTextTopFontScale { get; set; }
        public int? MasterPlanTextTopThickness { get; set; }
        public int? MasterPlanTextBottomAdjustX { get; set; }
        public int? MasterPlanTextBottomAdjustY { get; set; }
        public double? MasterPlanTextBottomFontScale { get; set; }
        public int? MasterPlanTextBottomThickness { get; set; }
        public int? UnitPointRadius { get; set; }
        public int? MasterPlanCoordinatesAdjustX { get; set; }
        public int? MasterPlanCoordinatesAdjustY { get; set; }


    }
}
