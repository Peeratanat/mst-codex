using Common.Helper;
using Base.DTOs.AppAuth;
using Base.DTOs; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using System.Reactive.Subjects;
using System.Security.Claims;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using PagingExtensions;
using System;
using System.Linq;
using Base.DTOs.Common;
using PageOutput = Base.DTOs.Common.PageOutput;

namespace Common.Extensions.HeaderUril
{
    public class HeadersUtil : IHeadersUtils
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeadersUtil(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetUserData(string UserID, string Empcode)
        {
            _httpContextAccessor.HttpContext.Request.Headers.Add("userid", UserID);

            //var claimsIdentity = new ClaimsIdentity(new List<Claim> {
            //                                                    new Claim("userid", UserID.ToString()),
            //                                                    new Claim("EmpCode", Empcode.ToString())
            //                                                 }
            //                                          );
        }

        public Guid? GetUserID()
        {
            Guid? CurrentUserID;
 


            if (Guid.TryParse(_httpContextAccessor?.HttpContext?.User?.Claims.Where(x => x.Type == "Userid").Select(o => o.Value).SingleOrDefault(), out Guid parsedUserID))
            {
                CurrentUserID = parsedUserID;
            }
            else
                CurrentUserID = null; 
            return CurrentUserID;
        }



       public string GetUserEmcode()
        {
            string CurrentEmpcode;
#if DEBUG
            CurrentEmpcode = "AP006006";
#else
        var parsedEmpCode = _httpContextAccessor.HttpContext.Request.Headers["empcode"];

        CurrentEmpcode = parsedEmpCode;
#endif
            return CurrentEmpcode;
        }
        public string GetMethod()
        {
            return _httpContextAccessor.HttpContext.Request.Method.ToString();
        }

        public void SetMethod(string SetMethod)
        {
            _httpContextAccessor.HttpContext.Request.Headers.Add("Method", SetMethod);
        }

        public void SetPagingHeader(PageOutput? output)
        {
            if (output != null)
            {
                _httpContextAccessor.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Paging-PageNo, X-Paging-PageSize, X-Paging-PageCount, X-Paging-TotalRecordCount");
                _httpContextAccessor.HttpContext.Response.Headers.Add("X-Paging-PageNo", output.Page.ToString());
                _httpContextAccessor.HttpContext.Response.Headers.Add("X-Paging-PageSize", output.PageSize.ToString());
                _httpContextAccessor.HttpContext.Response.Headers.Add("X-Paging-PageCount", output.PageCount.ToString());
                _httpContextAccessor.HttpContext.Response.Headers.Add("X-Paging-TotalRecordCount", output.RecordCount.ToString());
            }
        }

        public string GeturlPath()
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.ToString();
            string path = _httpContextAccessor.HttpContext.Request.Path.ToString();
            string PathBase = _httpContextAccessor.HttpContext.Request.PathBase.ToString();
            string fullPath = host + PathBase + path;

            return fullPath;
        }

        public string GetQueryString()
        {
            return _httpContextAccessor.HttpContext.Request.QueryString.ToString();
        }

        public async Task<string> GetBodyRequest()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            if (request.ContentType == null)
            {
                return "";
            }
            else if (request.ContentType.Contains("multipart/form-data"))
            {
                return "";
            }
            else
            {
                var bodyStream = new StreamReader(request.Body);
                bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                string paramRequest = await bodyStream.ReadToEndAsync();
                if (Extension.IsValidJson(paramRequest) && !string.IsNullOrWhiteSpace(paramRequest))
                {
                    paramRequest = JValue.Parse(paramRequest).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                }

                return paramRequest;
            }
        }

        public HttpRequest GetHttpRequest()
        {
            return this._httpContextAccessor.HttpContext.Request;
        }

        public string EmpCode
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request?.Headers["EmpCode"].ToString() ?? "";
            }
        }

        public ValidateLoginDTO UserLoginInfo
        {
            get { return (ValidateLoginDTO)_httpContextAccessor.HttpContext.Items["UserLogin"] ?? new ValidateLoginDTO(); }
        }

        public string ControllerName
        {
            get { return _httpContextAccessor.HttpContext.GetRouteValue("controller").ToString() ?? ""; ; }
        }

        public string UserID
        {
            //get { return _httpContextAccessor.HttpContext.GetRouteValue("Userid").ToString() ?? ""; ; }
            get { return _httpContextAccessor.HttpContext.Request.Headers["Userid"].FirstOrDefault()?.Split(" ").Last() ?? ""; }
        }

        public string ActionName
        {
            get { return _httpContextAccessor.HttpContext.GetRouteValue("action").ToString() ?? ""; ; }
        }

        public string CurrentToken
        {
            get { return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? ""; }
        }


        public async Task<UserWithMainRoleModel> GetUserWithMainRoleModel()
        {
#if DEBUG
            var userWithMainRoleModel = new UserWithMainRoleModel()
            {
                EmpCode = "AP005271",
                MainAppRoleCode = "FIXIT_PC",
                RoleCategoryCode = "FIXIT",
                PositionName = "Officer: Web Application Development",
                Email = "phiranat_c@apthai.com"
            };
#else
            var userWithMainRoleModel = new UserWithMainRoleModel()
            {
                EmpCode = _httpContextAccessor.HttpContext.Request?.Headers["EmpCode"].ToString() ?? "",
                MainAppRoleCode = "FIXIT_PC",
                RoleCategoryCode = "FIXIT",
                PositionName = "Officer: Web Application Development",
                Email = "phiranat_c@apthai.com"
            };
#endif
            return userWithMainRoleModel;
        }

        private string PublicAPIKeyEQN
        {
            get { return _httpContextAccessor.HttpContext.Request.Headers["Public_API_Key"].FirstOrDefault()?.Split(" ").Last() ?? ""; }
        }
        private string PublicAPISecretEQN
        {
            get { return _httpContextAccessor.HttpContext.Request.Headers["Public_API_Secret"].FirstOrDefault()?.Split(" ").Last() ?? ""; }
        }

        public void VerifyHeaderEQN()
        {
            var localKey = EnvironmentHelper.GetEnvironment("Public_API_Key");
            var localSecret = EnvironmentHelper.GetEnvironment("Public_API_Secret");
            if (string.IsNullOrEmpty(PublicAPIKeyEQN) && string.IsNullOrEmpty(PublicAPISecretEQN))
            {
                throw new UnauthorizedAccessException("Missing Authorization Header !!");
            }
            else if ((localKey != PublicAPIKeyEQN) || (localSecret != PublicAPISecretEQN))
            {
                throw new UnauthorizedAccessException("Missing Authorization Header !!");
            }
            return;
        }

        //private bool IsEnvProd()
        //{
        //    bool isEnvProd = false;

        //    if (EnvironmentHelper.GetEnvironment("ASPNETCORE_ENVIRONMENT").Equals("Production")
        //        || EnvironmentHelper.GetEnvironment("ASPNETCORE_ENVIRONMENT").Equals("production"))
        //    {
        //        isEnvProd = true;
        //    }

        //    return isEnvProd;
        //}
    }
}