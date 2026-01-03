using System;

namespace Database.Models.DbQueries.IDT
{
    public class dbqGetUserRole
    {
        public Guid? UserRoleID { get; set; }

        public Guid? UserID { get; set; }

        public string EmployeeNo { get; set; }
     
        public Guid? RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleCode { get; set; }

        public bool IsDefault { get; set; }
    }

}
