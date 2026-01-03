using System;

namespace Database.Models.DbQueries.IDT
{
    public class dbqGetUserMenu
    {
        public Guid? UserID { get; set; }
        public Guid? RoleID { get; set; }
        public Guid? UserRoleID { get; set; }

        public Guid? ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleNameTH { get; set; }
        public string ModuleNameEN { get; set; }


        public Guid? MenuID { get; set; }
        public string MenuCode { get; set; }
        public string MenuNameTH { get; set; }
        public string MenuNameEN { get; set; }
    }

}
