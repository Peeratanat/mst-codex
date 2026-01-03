using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Common.Services
{
    public class LogActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
             var actionName = context.ActionDescriptor.RouteValues["action"];
            _logger.LogInformation($"{actionName} : start");
             await next();
            _logger.LogInformation($"{actionName} : end");
        }
    }

}
