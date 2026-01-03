using External.Kafka.Models.Interface;
using Newtonsoft.Json;

namespace External.Kafka.Models
{
    public class ApplicationLog : IApplicationLog
    {

        public ApplicationLog()
        {
            //Console.WriteLine("CTOR SummaryLog");
        }

        public string HttpMethod { get; set; }
        public string HttpStatusCode { get; set; }
        public string LogLevel { get; set; } = "INFO";
        public string LogSystem { get; set; }
        public string LogMessage { get; set; }
        public string Module { get; set; }
        public string SubModule { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.Now;
        public string User { get; set; }


        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public void SetLogMessage(object obj)
        {
            this.LogMessage = JsonConvert.SerializeObject(obj);
        }
    }
}
