using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Logging;

namespace RequestLogging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggingMiddleware> logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //context.Request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            var builder = new StringBuilder(Environment.NewLine);
            foreach (var header in context.Request.Headers)
            {
                builder.AppendLine($"{header.Key}:{header.Value}");
            }

            builder.AppendLine($"Request body:{requestBody}");

            //Console.WriteLine($"Finance API {DateTime.Now} : Pods : {Environment.MachineName} : '{context.Request?.Path}' : {context.Request?.QueryString} : {builder.ToString()}");
            logger.LogInformation(builder.ToString());

            await next(context);
        }
    }
}
