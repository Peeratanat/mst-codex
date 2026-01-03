using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class EDCSortByParam
    {
        public EDCSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum EDCSortBy
    {
        Code,
        CardMachineType,
        BankAccountNo,
        Company,
        Project_ProjectNo,
        Project_Status,
        ReceiveBy,
        ReceiveDate,
        CardMachineStatus,
        Updated,
        UpdatedBy,
        Bank,
        TelNo
    }
}
