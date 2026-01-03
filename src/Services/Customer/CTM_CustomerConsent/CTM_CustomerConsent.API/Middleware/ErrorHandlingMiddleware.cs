using Base.DTOs.Common;
using Common.Helper.HttpResultHelper;
using Common.Helper.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using AppException = Common.Exceptions.AppException;
using Base.DTOs;
using ErrorHandling;
using ErrorResponse = ErrorHandling.ErrorResponse;
using NPOI.SS.Formula.Functions;

namespace CTM_CustomerConsent.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private const string MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, IHttpResultHelper httpResultHelper)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context);
                sw.Stop();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, sw, httpResultHelper);
            }
        }

        private static async Task<Task> HandleExceptionAsync(HttpContext context, Exception exception, Stopwatch sw, IHttpResultHelper httpResultHelper)
        {
            var logModel = new LogModel(context.Request.Path, null);
            string message;

            while (exception.InnerException != null)
                exception = exception.InnerException;

            message = exception.Message;
            Type exceptionType = exception.GetType();
            HttpStatusCode statusCode;

            ResponseModel Response = null;
            Response = new ResponseModel<T>
            {
                Message = message,
                RequestID = logModel.RequestID,
            };



            if (exceptionType == typeof(AppException))
            {
                var ex = exception as AppException;
                logModel = ex.logModel;
                statusCode = ex.StatusCode;
            }
            else if (exceptionType == typeof(SqlException))
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(NotSupportedException))
            {
                statusCode = HttpStatusCode.UnsupportedMediaType;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(ValidateException))
            {
                var validate = (ValidateException)exception;
                Response = new ResponseModel<ErrorResponse>
                {
                    Data = validate.ErrorResponse,
                    Message = message,
                    RequestID = logModel.RequestID,
                };
                statusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            logModel.ExceptionMessage = new LogExceptionModel(exception);
            await httpResultHelper.ExceptionCustomResult(statusCode, Response, logModel);
            string result = JsonConvert.SerializeObject(Response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

    }
}
