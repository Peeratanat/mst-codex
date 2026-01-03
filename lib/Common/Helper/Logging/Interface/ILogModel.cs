using Common.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper.Logging.Interface
{
    public interface ILogModel
    {
        bool SetRequestData(string command, object requestObject);
        bool SetRequestData(string command, RequestObjectModel requestObject);
        void AddLogAlive(string text);
        string Command { get; set; }
        string Status { get; set; } 
        object RequestObject { get; set; }
        object ResponseObject { get; set; }
        ActivityLogModel ActivityLog { get; set; }
        List<EndPointsModel> EndPoint { get; set; }
        LogExceptionModel ExceptionMessage { get; set; }
        List<string> LogAlive { get; set; }
        ActivityLogModel GetActivityLog();
    }
}
