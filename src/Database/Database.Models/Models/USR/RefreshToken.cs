using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("RefreshToken (JWT)")]
    [Table("RefreshToken", Schema = Schema.USER)]
    public class RefreshToken : BaseEntityWithoutKey
    {
        [Key]
        [MaxLength(50)]
        public string Token { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [Description("วันที่หมดอายุ (UTC)")]
        public DateTime ExpireDate { get; set; }
    }
}
