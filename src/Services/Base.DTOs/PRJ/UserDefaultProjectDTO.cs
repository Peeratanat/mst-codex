using System;
using System.Collections.Generic;
using System.Text;
using Database.Models.PRJ;

namespace Base.DTOs.PRJ
{
	public class UserDefaultProjectDTO : BaseDTO
    {
        public Guid UserID { get; set; }

        public USR.UserDTO User { get; set; }

        public Guid ProjectID { get; set; }

        public PRJ.ProjectDTO Project { get; set; }

    }
}
