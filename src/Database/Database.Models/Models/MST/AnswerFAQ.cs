using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("Answer FAQ")]
    [Table("AnswerFAQ", Schema = Schema.MASTER)]
    public class AnswerFAQ : BaseEntityWithoutMigrate
    {
        public Guid? QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public MST.QuestionFAQ QuestionFAQ { get; set; }
        public string AnswerDesc { get; set; }
        public int? Order { get; set; }
        public string Remark { get; set; }
        public bool? IsSubmit { get; set; } 
    }
}
