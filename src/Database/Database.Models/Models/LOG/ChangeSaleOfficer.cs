using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log LC ปิดการขาย")]
    [Table("ChangeSaleOfficer", Schema = Schema.LOG)]
    public class ChangeSaleOfficer : BaseEntityWithoutMigrate
    {
        public Guid? BookingID { get; set; }
        public Guid? SaleOfficerTypeMasterCenterID { get; set; }
        public Guid? SaleUserID { get; set; }
        public Guid? ProjectSaleUserID { get; set; }
        public Guid? AgentID { get; set; }
        public Guid? AgentEmployeeID { get; set; } 
    }
}
