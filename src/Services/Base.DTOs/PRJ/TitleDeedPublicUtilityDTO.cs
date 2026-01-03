using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.PRJ
{
	public class TitleDeedPublicUtilityDTO : BaseDTO
	{
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        //[Description("รหัสโครงการ")]
        //public Guid? ProjectID { get; set; }
        ///// <summary>
        ///// รหัสโครงการ
        ///// </summary>
        //public Project Project { get; set; }
        ///// <summary>
        ///// แปลง
        ///// </summary>
        //[Description("แปลง")]
        //public string UnitNo { get; set; }
        /// <summary>
        /// เลขที่ดิน
        /// </summary>
        [Description("เลขที่ดิน")]
        public string LandNo { get; set; }
        /// <summary>
        /// หน้าสำรวจ
        /// </summary>
        [Description("หน้าสำรวจ")]
        public string LandSurveyArea { get; set; }
        /// <summary>
        /// เลขระวาง
        /// </summary>
        [Description("เลขระวาง")]
        public string LandPortionNo { get; set; }
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        [Description("เลขที่โฉนด")]
        public string TitledeedNo { get; set; }
        /// <summary>
        /// เล่ม
        /// </summary>
        [Description("เล่ม")]
        public string BookNo { get; set; }
        /// <summary>
        /// หน้า
        /// </summary>
        [Description("หน้า")]
        public string PageNo { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        [Description("พื้นที่โฉนด")]
        public double? TitledeedArea { get; set; }
        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        //[Description("พื้นที่ใช้สอย")]
        //public double? UsedArea { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        [Description("Order")]
        public int? Order { get; set; }
 

    public void ToModel(ref TitleDeedPublicUtilityDTO model)
    {
            model.LandNo = this.LandNo;
            model.LandSurveyArea = this.LandSurveyArea;
            model.LandPortionNo = this.LandPortionNo;
            model.TitledeedNo = this.TitledeedNo;
            model.BookNo = this.BookNo;
            model.PageNo = this.PageNo;
            model.TitledeedArea = this.TitledeedArea;
            model.Order = this.Order;
        }
    }
}
