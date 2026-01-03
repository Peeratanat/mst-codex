using Common.Helper.Logging.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper.Logging
{
    public class TraceLog 
    {
        public string RequestID { get; set; }
        public DateTime StartTime { get; set; } 
        public object RequestObject { get; set; }
        public object ResponseData { get; set; }
        public object Endpoint { get; set; }
        public object LogAlive { get; set; }
        public object ActivityLog { get; set; } 
        public object ExceptionMessage { get; set; }


    }
}
