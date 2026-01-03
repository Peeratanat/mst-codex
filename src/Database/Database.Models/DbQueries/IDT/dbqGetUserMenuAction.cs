using System;

namespace Database.Models.DbQueries.IDT
{
    public class dbqGetUserMenuAction
    {
        public Guid? UserID { get; set; }
        public Guid? RoleID { get; set; }
        public Guid? UserRoleID { get; set; }

        public Guid? ModuleID { get; set; }    
        public Guid? MenuID { get; set; }
        public string MenuCode { get; set; }

        public Guid? MenuPermissionID { get; set; }

        public Guid? MenuActionID { get; set; }

        public string MenuActionCode { get; set; }
        public string MenuActionName { get; set; }
        public int? MenuActionOrder { get; set; }
    }

}
