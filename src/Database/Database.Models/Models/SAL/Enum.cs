using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Database.Models.SAL
{
    public enum PayTo
    {
        AP, Juristic
    }

    public enum BookingCancelType
    {
        [Description("ยกเลิกใบจองปกติ")]
        Cancel = 1, 
        [Description("ยกเลิกใบจองจากการย้ายแปลง")]
        CancelByChangeUnit = 2,
        [Description("ยกเลิกใบจองจากการยกเลิกสัญญา")]
        CancelByCancelContract = 3
    }
}
