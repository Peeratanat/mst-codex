using Database.Models.DbQueries.IDT;
using System;
using System.Collections.Generic;
using System.Data;

namespace Base.DTOs.IDT
{
    public class PermissionByRoleDTO
    {
        /// <summary>
        /// หัวตาราง
        /// </summary>
        public List<string> PermissionByRoleHeader { get; set; }

        /// <summary>
        /// Detail
        /// </summary>
        public DataTable PermissionByRoleDetail { get; set; }
    }

    public class MenuActionDTO
    {
        public Guid? Id { get; set; }

        public string Module { get; set; }

        public string Menu { get; set; }

        public string MenuAction { get; set; }

        public static MenuActionDTO CreateFromModel(sqlMenuAction.QueryResult model)
        {
            if (model != null)
            {
                var result = new MenuActionDTO()
                {
                    Id = model.MenuActionID,
                    Module = $"{model.ModuleCode} : {model.ModuleNameTH}",
                    Menu = $"{model.MenuCode} : {model.MenuNameTH}",
                    MenuAction = $"{model.MenuActionCode} : {model.MenuActionName}",
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class UpdatePermissionByRoleDTO
    {
        public string RoleCode { get; set; }
        public Guid? MenuActionID { get; set; }
        public bool? IsActive { get; set; }
    }
}
