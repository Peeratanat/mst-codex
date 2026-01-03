using Common.Helper.Logging;
using Common.Helper.Logging.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Common.Services.Interfaces;
using System.Threading.Tasks;
using Base.DTOs.Common;
using Base.DTOs.SystemMessage;
using System.Net.Sockets;

namespace Common.Helper.HttpResultHelper
{
    public class HttpResultHelper : IHttpResultHelper
    {
        private static LogHelper? logger;
        private readonly ILogger _logger;
        private readonly ILogService _logService;

        public HttpResultHelper(ILogger<HttpResultHelper> logger, ILogService logService)
        {
            _logger = logger;
            _logService = logService;
        }

        public static void InitLogHelper()
        {
            logger = new LogHelper();

        }

        public async Task<IActionResult> SuccessCustomResult(dynamic? data, LogModel logModel, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var response = new ResponseModel<dynamic>()
            {
                RequestID = logModel.RequestID,
                Message = MessageResponse(httpStatusCode),
                Data = data
            };

            string MessageResponse(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
            { 
                switch (httpStatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return CommonMessage.NotFound;
                    default:
                        return CommonMessage.Success;
                }
            }
            logModel.StatusCode = httpStatusCode;
            logModel.ResponseData = response;
            await _logService.WriteLog(logModel);
            return new ResponseResult(response, httpStatusCode);
        }

        public async Task<IActionResult> ResponseCustomResult(dynamic? data, LogModel logModel)
        {
            var response = new ResponseModel<dynamic>()
            {
                RequestID = logModel.RequestID,
                Message = CommonMessage.Success,
                Data = data
            };
            logModel.StatusCode = HttpStatusCode.OK;
            logModel.ResponseData = response;
            await _logService.WriteLog(logModel);
            return new ResponseResult(response, HttpStatusCode.OK);
        }

        public async Task<IActionResult> ExceptionCustomResult(HttpStatusCode statuscode, ResponseModel data, LogModel logModel)
        {
            logModel.StatusCode = statuscode;
            logModel.ResponseData = data;
            await _logService.WriteLog(logModel);
            return new ResponseResult(data, statuscode);
        }
        public class ResponseResult : JsonResult
        {

            public ResponseResult(object data, HttpStatusCode code)
                           : base(data)
            {
                StatusCode = (int)code;
            }
        }
    }
}
