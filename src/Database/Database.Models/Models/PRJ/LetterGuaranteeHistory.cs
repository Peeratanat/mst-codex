using Database.Models.MST;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("FIle หนังสือสัญญาค้ำประกัน")]
    [Table("LetterGuaranteeFile", Schema = Schema.PROJECT)]
    public class LetterGuaranteeFile : BaseEntityWithoutMigrate
    {

        public Guid LetterGuaranteeID { get; set; }
        [ForeignKey("LetterGuaranteeID")]
        public LetterGuarantee LetterGuarantee { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Remark { get; set; }
        public string FileType { get; set; }
    }


}
