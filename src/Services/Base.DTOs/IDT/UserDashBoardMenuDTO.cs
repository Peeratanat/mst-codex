using Database.Models.DbQueries.IDT;
using System;
using System.ComponentModel;

namespace Base.DTOs.IDT
{
    public class UserDashboardMenuDTO
    {
        /// <summary>
        /// UserID
        /// </summary>
        [Description("UserID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// MenuID
        /// </summary>
        [Description("MenuID")]
        public Guid? MenuID { get; set; }

        /// <summary>
        /// DashboardCode
        /// </summary>
        [Description("DashboardCode")]
        public string DashboardCode { get; set; }

        /// <summary>
        /// DashboardNameTH
        /// </summary>
        [Description("DashboardNameTH")]
        public string DashboardNameTH { get; set; }

        /// <summary>
        /// DashboardURL
        /// </summary>
        [Description("DashboardURL")]
        public string DashboardURL { get; set; }

        public static UserDashboardMenuDTO CreateFromDBQuery(dbqGetUserDashboardMenu model)
        {
            if (model != null)
            {
                var result = new UserDashboardMenuDTO();

                result.UserID = model.UserID;
                result.MenuID = model.MenuID;
                result.DashboardCode = model.DashboardCode;
                result.DashboardNameTH = model.DashboardNameTH;
                result.DashboardURL = model.DashboardURL;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}