using Database.Models.DbQueries.IDT;
using System;
using System.ComponentModel;

namespace Base.DTOs.IDT
{
    public class UserRoleDTO
    {
        /// <summary>
        /// UserRoleID
        /// </summary>
        [Description("UserRoleID")]
        public Guid? UserRoleID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        [Description("UserID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// EmployeeNo
        /// </summary>
        [Description("EmployeeNo")]
        public string EmployeeNo { get; set; }

        /// <summary>
        /// RoleID
        /// </summary>
        [Description("RoleID")]
        public Guid? RoleID { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>
        [Description("RoleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// RoleCode
        /// </summary>
        [Description("RoleCode")]
        public string RoleCode { get; set; }

        /// <summary>
        /// role ตั้งตัน
        /// </summary>
        [Description("role ตั้งตัน")]
        public bool IsDefault { get; set; }

        public static UserRoleDTO CreateFromDBQuery(dbqGetUserRole model)
        {
            if (model != null)
            {
                var result = new UserRoleDTO();

                result.UserID = model.UserID;
                result.EmployeeNo = model.EmployeeNo;
                result.RoleID = model.RoleID;
                result.UserRoleID = model.UserRoleID;

                result.RoleName = model.RoleName;
                result.RoleCode = model.RoleCode;

                result.IsDefault = model.IsDefault;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}