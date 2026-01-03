using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ค่าทำเนียมโอน-สูตรปัดเศษ")]
    [Table("RoundFee", Schema = Schema.PROJECT)]
    public class RoundFee : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("ที่ตั้งสำนักงานที่ดิน")]
        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public MST.LandOffice LandOffice { get; set; }

        [Description("ค่าธรรมเนียมจิปาถะ")]
        [Column(TypeName = "Money")]
        public decimal? OtherFee { get; set; }


        [Description("สูตรปัดค่าธรรมเนียมการโอน")]
        public Guid? TransferFeeRoundFormulaMasterCenterID { get; set; }
        [ForeignKey("TransferFeeRoundFormulaMasterCenterID")]
        public MST.MasterCenter TransferFeeRoundFormula { get; set; }

        [Description("สูตรปัดเศษธุรกิจเฉพาะ")]
        public Guid? BusinessTaxRoundFormulaMasterCenterID { get; set; }
        [ForeignKey("BusinessTaxRoundFormulaMasterCenterID")]
        public MST.MasterCenter BusinessTaxRoundFormula { get; set; }

        [Description("สูตรปัดเศษภาษีท้องถิ่น")]
        public Guid? LocalTaxRoundFormulaMasterCenterID { get; set; }
        [ForeignKey("LocalTaxRoundFormulaMasterCenterID")]
        public MST.MasterCenter LocalTaxRoundFormula { get; set; }

        [Description("สูตรปัดเศษภาษีเงินได้นิติบุคคล")]
        public Guid? IncomeTaxRoundFormulaMasterCenterID { get; set; }
        [ForeignKey("IncomeTaxRoundFormulaMasterCenterID")]
        public MST.MasterCenter IncomeTaxRoundFormula { get; set; }

    }
}
