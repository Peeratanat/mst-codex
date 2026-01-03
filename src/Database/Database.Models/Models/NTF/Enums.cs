using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.NTF
{
    public enum MobileDeviceType
    {
        iOS = 0, 
        Android = 1
    }

    public enum SendStatus
    {
        Create = -1,
        Open = 0,
        Sent = 1,
        Read = 2,
        Failed = 3,
    }
}
