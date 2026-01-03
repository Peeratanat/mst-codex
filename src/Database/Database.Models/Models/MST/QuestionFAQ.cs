using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("Question FAQ")]
    [Table("QuestionFAQ", Schema = Schema.MASTER)]
    public class QuestionFAQ : BaseEntityWithoutMigrate
    {
        public string QuestionDesc { get; set; }
        public string Remark { get; set; }
        public bool? IsSubmit { get; set; }
        public Guid? FAQCategoryMasterCenterID { get; set; }
        [ForeignKey("FAQCategoryMasterCenterID")]
        public MasterCenter FAQCategoryMasterCenter { get; set; }
        public DateTime? UpsertPineconeDate { get; set; }
        public string PineconeListID { get; set; }
    }
}
