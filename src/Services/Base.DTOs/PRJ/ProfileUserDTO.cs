using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
	public class ProfileUserDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// โปรไฟล์
        /// </summary>
        public PRJ.UserDefaultProjectDTO UserDefaultProject { get; set; }

        public USR.UserDTO User { get; set; }
    }
}
