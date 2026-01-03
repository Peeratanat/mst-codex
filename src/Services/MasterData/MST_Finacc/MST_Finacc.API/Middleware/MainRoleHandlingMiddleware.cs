
using Common.Extensions.HeaderUril;
using Database.Models.USR;
using External.WebHttpClient;
using System.Security.Claims;

namespace MST_Finacc.API.Middleware
{
    public class MainRoleHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public MainRoleHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IHeadersUtils headersUtil)
        {
            var token = headersUtil.CurrentToken;
            if(token == null || string.IsNullOrEmpty(token))
            {
                await _next(context);
                return;
            }
            var empCode = headersUtil.EmpCode;
            var loginResult = await userService.ValidateMainRole(token, empCode);
            if (loginResult != null)
            { 
                context.Items["UserLogin"] = loginResult;
                if(context != null)
                {
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { 
                        new Claim("userid",loginResult?.UserID ?? "")
                    }));
                }  
            }
            Console.WriteLine("_next(context)");
            await _next(context);
        }
    }
}
