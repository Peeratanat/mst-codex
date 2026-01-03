using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Table("UserBackgroundJob", Schema = Schema.USER)]
    public class UserBackgroundJob : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public double Progress { get; set; }
        public BackgroundJobStatus Status { get; set; }
        public string Params { get; set; }
        public string ResponseMessage { get; set; }
    }
}
