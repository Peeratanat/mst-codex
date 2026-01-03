
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services
{
    public class LogActionFilterRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<LogActionFilter>();

            services.AddControllers(options =>
            {
                options.Filters.Add<LogActionFilter>();
            });
        }
    }
}