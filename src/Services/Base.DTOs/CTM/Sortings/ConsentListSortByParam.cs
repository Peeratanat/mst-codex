using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ConsentListSortByParam
    {
        public ConsentListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ConsentListSortBy
    {
        ActivityTaskTopic,
        LeadType,
        ActivityTaskType,
        Project,
        FirstName,
        LastName,
        PhoneNumber,
        OverdueDays,
        Owner,
        ActivityTaskStatus,
        DueDate
    }
}
