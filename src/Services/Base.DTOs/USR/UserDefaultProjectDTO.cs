using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.USR
{
    public class UserDefaultProjectDTO
	{

        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }

        [Description("ลบ")]
        public bool IsDeleted { get; set; }
        public void ToModel(ref UserDefaultProject model)
        {
            model.ProjectID = this.ProjectID;
            model.IsDeleted = this.IsDeleted;
        }
    }
}
