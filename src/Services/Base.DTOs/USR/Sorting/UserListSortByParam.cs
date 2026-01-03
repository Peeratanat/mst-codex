using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.USR
{
    public class UserListSortByParam
    {
        public UserListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UserListSortBy
    {
        EmployeeNo,
        FirstName,
        LastName,
        DisplayName
    }
}
