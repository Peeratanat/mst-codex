using Base.DTOs.Common;
using Common.Helper.Logging.Interface; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Common.Helper.Logging
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LogModel : ILogModel
    {

        public DateTime StartTime;
        public LogModel()
        {
            StartTime = DateTime.Now;
            EndPoint = new List<EndPointsModel>();
        }
        public LogModel(string command, object requestObject)
        {
            StartTime = DateTime.Now;
            EndPoint = new List<EndPointsModel>();
            Command = command;
            RequestObject = requestObject;
        } 

        public LogModel(string command, RequestObjectModel? requestObject)
        {
            StartTime = DateTime.Now;
            EndPoint = new List<EndPointsModel>();
            Command = command;
            RequestObject = requestObject;
        } 
        public string RequestID { get; set; } = Guid.NewGuid().ToString();
        public string Command { get; set; }
        public string SubModule { get; set; }
        public string Status { get; set; }
        public string ParamURI { get; set; }
        public object RequestObject { get; set; }
        public object ResponseObject { get; set; }
        public ResponseModel ResponseData { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ActivityLogModel ActivityLog { get; set; }
        public List<EndPointsModel> EndPoint { get; set; }
        public List<string> LogAlive { get; set; } = new List<string>();
        public LogExceptionModel ExceptionMessage { get; set; }

        public ActivityLogModel GetActivityLog()
        {
            DateTime time = DateTime.Now;
            return new ActivityLogModel()
            {
                StartTime = StartTime,
                EndTime = time,
                ProcessTime = Math.Round((time - StartTime).TotalSeconds, 4)
            };
        }

        public bool SetRequestData(string command , object requestObject)
        {
            Command = command; 
            RequestObject = requestObject;
            return true;
        }

        public bool SetRequestData(string command , RequestObjectModel requestObject)
        {
            Command = command; 
            RequestObject = requestObject;
            return true;
        }

        public void AddLogAlive(string text)
        {
            this.LogAlive.Add(DateTime.Now.ToString() + " : " + text);   
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class EndPointsModel
    {
        private DateTime StartTime;
        public EndPointsModel()
        {
            StartTime = DateTime.Now;
        }
        public EndPointsModel(string endpointname)
        {
            StartTime = DateTime.Now;
            //NodeName NodeName = nodename;
            EndPointName = endpointname;
        }

        public string EndPointName { get; set; }
        public object RequestObject { get; set; }
        public object ResponseObject { get; set; }
        public LogExceptionModel ExceptionMessage { get; set; }
        public ActivityLogModel ActivityLog { get; set; }
        public ActivityLogModel GetActivityLog()
        {
            DateTime time = DateTime.Now;
            return new ActivityLogModel()
            {
                StartTime = StartTime,
                EndTime = time,
                ProcessTime = Math.Round((time - StartTime).TotalSeconds, 4)
            };
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ActivityLogModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double ProcessTime { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SetRequestModel
    {
        public string url { get; set; }
        public string method { get; set; }
        public object headers { get; set; }
        public Dictionary<string, string> routeParamteters { get; set; }
        public object queryString { get; set; }
        public object body { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RequestObjectModel
    {
        public RequestObjectModel(object _headers, object _body, string? _url = null)
        {
            headers = _headers;
            url = _url;
            body = _body;
        }

        public string? url { get; set; }

        public object headers { get; set; }

        public Dictionary<string, string> routeParamteters { get; set; }

        public object queryString { get; set; }

        public object body { get; set; }
    }



    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LogExceptionModel
    {
        public LogExceptionModel()
        {

        }
        public LogExceptionModel(Exception exception)
        {
            exception = GetInnerException(exception);
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            //if (exception.InnerException != null) InnerException = exception.InnerException.ToString();
        }

        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        //public string InnerException { get; set; }

        public Exception GetInnerException(Exception ex)
        {
            return ex.InnerException == null ? ex : GetInnerException(ex.InnerException);
        }
    }
}
