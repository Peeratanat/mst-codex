using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ActivityListSortByParam
    {
        public ActivityListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ActivityListSortBy
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
        DueDate,
        SubLeadType
    }
}
