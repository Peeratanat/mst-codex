using Database.Models.DbQueries.IDT;
using Database.Models.USR;
using System;
using System.ComponentModel;

namespace Base.DTOs.IDT
{
    public class UserMenuDTO
    {

        /// <summary>
        /// UserID
        /// </summary>
        [Description("UserID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// UserRoleID
        /// </summary>
        [Description("UserRoleID")]
        public Guid? UserRoleID { get; set; }

        /// <summary>
        /// RoleID
        /// </summary>
        [Description("RoleID")]
        public Guid? RoleID { get; set; }

        /// <summary>
        /// ModuleID
        /// </summary>
        [Description("ModuleID")]
        public Guid? ModuleID { get; set; }

        /// <summary>
        /// ModuleCode
        /// </summary>
        [Description("ModuleCode")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// ModuleNameTH
        /// </summary>
        [Description("ModuleNameTH")]
        public string ModuleNameTH { get; set; }

        /// <summary>
        /// ModuleNameEN
        /// </summary>
        [Description("ModuleNameEN")]
        public string ModuleNameEN { get; set; }

        /// <summary>
        /// MenuID
        /// </summary>
        [Description("MenuID")]
        public Guid? MenuID { get; set; }

        /// <summary>
        /// MenuCode
        /// </summary>
        [Description("MenuCode")]
        public string MenuCode { get; set; }

        /// <summary>
        /// MenuNameTH
        /// </summary>
        [Description("MenuNameTH")]
        public string MenuNameTH { get; set; }

        /// <summary>
        /// MenuNameEN
        /// </summary>
        [Description("MenuNameEN")]
        public string MenuNameEN { get; set; }

        public static UserMenuDTO CreateFromDBQuery(dbqGetUserMenu model)
        {
            if (model != null)
            {
                var result = new UserMenuDTO();

                result.UserID = model.UserID;
                result.RoleID = model.RoleID;
                result.UserRoleID = model.UserRoleID;

                result.ModuleID = model.ModuleID;
                result.ModuleCode = model.ModuleCode;
                result.ModuleNameTH = model.ModuleNameTH;
                result.ModuleNameEN = model.ModuleNameEN;

                result.MenuID = model.MenuID;
                result.MenuCode = model.MenuCode;
                result.MenuNameTH = model.MenuNameTH;
                result.MenuNameEN = model.MenuNameEN;

                return result;
            }
            else
            {
                return null;
            }
        }

        //public static UserMenuDTO CreateFromQueryResult(UserMenuQueryResult model)
        //{
        //    if (model != null)
        //    {
        //        var result = new UserMenuDTO();

        //        result.UserID = model.User.ID;
        //        result.EmployeeNo = model.User.EmployeeNo;

        //        result.UserRoleID = model.Role.ID;

        //        result.RoleID = model.Role.ID;
        //        result.RoleName = model.Role.Name;
        //        result.RoleCode = model.Role.Code;

        //        result.ModuleID = model?.Module.ID;
        //        result.ModuleCode = model?.Module.Code;
        //        result.ModuleNameTH = model?.Module.NameTH;
        //        result.ModuleNameEN = model?.Module.NameEng;

        //        result.MenuID = model.Menu?.ID;
        //        result.MenuCode = model.Menu?.MenuCode;
        //        result.MenuNameTH = model.Menu?.MenuNameTH;
        //        result.MenuNameEN = model.Menu?.MenuNameEng;

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }

    //public class UserMenuQueryResult
    //{
    //    public User User { get; set; }
    //    public Role Role { get; set; }
    //    public Menu Menu { get; set; }
    //    public Module Module { get; set; }
    //}
}
