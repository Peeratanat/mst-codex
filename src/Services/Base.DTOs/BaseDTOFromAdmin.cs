using System;
using static Database.Models.ExtensionAttributes;

namespace Base.DTOs
{
    public class BaseDTOFromAdmin
    {
        [DeviceInformation("Job No", "")]
        public string RequestNo { get; set; }

        [DeviceInformation("MenuCode", "")]
        public string MenuCode { get; set; }

        [DeviceInformation("MenuName", "")]
        public string MenuName { get; set; }

        [DeviceInformation("Remark", "")]
        public string Remark { get; set; }
    }
}
