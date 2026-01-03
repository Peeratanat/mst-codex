using System;
using System.Collections.Generic;
using System.Text;

namespace Auth_User.Params.Filters
{
    public class UserFilter
    {
        /// <summary>
        /// รหัสพนักงาน
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// RoleCodes (comma saparated)
        /// </summary>
        public string RoleCodes { get; set; }
        /// <summary>
        /// Project ที่มีสิทธิ์เข้าถึง (comma saparated)
        /// </summary>
        public string AuthorizeProjectIDs { get; set; }
        /// <summary>
        /// IgnoreQueryFilters
        /// </summary>
        public bool IgnoreQueryFilters { get; set; }
        /// <summary>
        /// IgnoreUser
        /// </summary>
        public bool IgnoreUser { get; set; }

        /// <summary>
        /// IgnoreTemp
        /// </summary>
        public bool IgnoreTemp { get; set; }
        public bool IgnoreLead { get; set; }

        /// <summary>
        /// PositionCodes (comma saparated)
        /// </summary>
        public string PositionCodes { get; set; }

    }
}
