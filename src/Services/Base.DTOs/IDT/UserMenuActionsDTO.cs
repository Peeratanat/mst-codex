using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.IDT
{
    public class UserMenuActionsDTO : BaseDTO
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
        /// MenuAction
        /// </summary>
        [Description("MenuActionList")]
        public List<MenuActionPermission> MenuActionList { get; set; }


        public static UserMenuActionsDTO CreateFromQueryResult(UserMenuActionQueryResult model)
        {
            if (model != null)
            {
                var result = new UserMenuActionsDTO();

                result.UserID = model.UserID;
                result.RoleID = model.RoleID;
                result.UserRoleID = model.UserRoleID;

                result.ModuleID = model.ModuleID;
                result.MenuID = model.MenuID;
                result.MenuCode = model.MenuCode;

                result.MenuActionList = model.MenuActionPermissionQueryResult.OrderBy(o => o.MenuActionOrder).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class MenuActionPermission
    {
        /// <summary>
        /// MenuPermissionID
        /// </summary>
        [Description("MenuPermissionID")]
        public Guid? MenuPermissionID { get; set; }

        /// <summary>
        /// MenuActionID
        /// </summary>
        [Description("MenuActionID")]
        public Guid? MenuActionID { get; set; }

        /// <summary>
        /// ActionCode
        /// </summary>
        [Description("ActionCode")]
        public string MenuActionCode { get; set; }

        /// <summary>
        /// ActionName
        /// </summary>
        [Description("ActionName")]
        public string MenuActionName { get; set; }

        /// <summary>
        /// ActionOrder
        /// </summary>
        [Description("ActionOrder")]
        public int MenuActionOrder { get; set; }
    }

    public class UserMenuActionQueryResult
    {
        public Guid? UserID { get; set; }
        public Guid? RoleID { get; set; }
        public Guid? UserRoleID { get; set; }
        public Guid? ModuleID { get; set; }
        public Guid? MenuID { get; set; }
        public string MenuCode { get; set; }   
        public int MenuActionOrder { get; set; }   
        

        public List<MenuActionPermission> MenuActionPermissionQueryResult { get; set; }
    }
}
