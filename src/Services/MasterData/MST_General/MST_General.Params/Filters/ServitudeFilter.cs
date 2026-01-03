using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class ServitudeFilter
    {
        public string ProjectNo { get; set; }
        public string MsgTH { get; set; }
        public string MsgEN { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
