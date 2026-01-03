using Common.Helper.Logging.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper.Logging
{
    public class SummaryLog : ISummaryLog
    {
        public SummaryLog()
        {
            //Console.WriteLine("CTOR SummaryLog");
        }

        private const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss.fff";

        public string systemTimestamp { get; set; } = DateTime.Now.ToString(DateTimeFormat);
        public string logType { get; private set; } = "Summary";
        public string containerId { get; private set; } = Environment.MachineName;
        public string @namespace { get; private set; } = "CRM3_Aftersale";
        public string applicationName { get; private set; } = AppDomain.CurrentDomain.FriendlyName;
        public string reqTimestamp { get; private set; } = DateTime.Now.ToString(DateTimeFormat);
        public string sessionId { get; set; } = Guid.NewGuid().ToString();
        public string tid { get; set; }
        public string initInvoke { get; set; }
        public string cmdName { get; set; }
        public string identity { get; private set; }
        public string resultCode { get; set; }
        public string resultDesc { get; set; }
        public string custom { get; private set; }
        public string resTimestamp { get; private set; }
        public string usageTime { get; set; }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Setcustom(object obj)
        {
            custom = JsonConvert.SerializeObject(obj);
        }

        public void SetIdentity(object identity)
        {
            this.identity = JsonConvert.SerializeObject(identity);
        }

        public void SetreqTimestamp(DateTime dateTime)
        {
            reqTimestamp = dateTime.ToString(DateTimeFormat);
        }

        public void SetresTimestamp(DateTime dateTime)
        {
            resTimestamp = dateTime.ToString(DateTimeFormat);
        }
    }
}
