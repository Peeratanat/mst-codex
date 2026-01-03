using Common.Helper.Logging;
using System;
using System.Net;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public readonly HttpStatusCode StatusCode;
        public readonly LogModel logModel;

        public AppException(HttpStatusCode StatusCode)
        {
            this.StatusCode = StatusCode;
        }

        public AppException(HttpStatusCode StatusCode, LogModel logModel)
        {
            this.StatusCode = StatusCode;
            this.logModel = logModel;
        }

        public AppException(HttpStatusCode StatusCode, string message) : base(message)
        {
            this.StatusCode = StatusCode;
        }

        public AppException(HttpStatusCode StatusCode, string message, LogModel logModel) : base(message)
        {
            this.StatusCode = StatusCode;
            this.logModel = logModel;
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string StatusCode, string message, LogModel logModel) : base(message)
        {
            this.StatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), StatusCode);
            this.logModel = logModel;
        }
    }
}