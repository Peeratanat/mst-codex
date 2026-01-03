using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Database.Models
{
    public enum ErrorMessageType
    {
        FieldValidation = 0,
        PopupAlert = 1,
    }

    public enum BackgroundJobStatus
    {
        InProgress = 0,
        Completed = 1,
        Failed = 2,
        Waiting = 3,
        WaitingForResult = 4,
        Retrying = 5
    }

    public enum MinPriceStage
    {
        Booking = 0,
        ChangeUnitBooking = 1,
        ChangeUnitContract = 2
    }
}
