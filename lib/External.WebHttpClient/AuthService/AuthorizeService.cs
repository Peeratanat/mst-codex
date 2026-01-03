using Base.DTOs;
using Base.DTOs.Auth; 
using Common.Extensions.HeaderUril;
using Common.Helper.Logging;
using Database.Models.USR;
using External.Kafka.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions; 

namespace External.WebHttpClient
{
    public class AuthorizeService : IAuthorizeService
    {
        public LogModel logModel { get; set; }
        public List<EndPointsModel> _endpoints;
        private readonly IHeadersUtils _headersUtil;
        private readonly IKakfaProducer _kakfaProducer; 
       // private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IAuthenticationService _authenticationService;
        //private readonly APEQuestionnaireContext _context;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeService(IHeadersUtils headersUtil, // IAuthorizeRepository authorizeRepository, 
            IAuthenticationService authenticationService, //APEQuestionnaireContext context, 
            IHttpContextAccessor httpContextAccessor)
        {
            logModel = new LogModel("AuthorizeService", null);
            _headersUtil = headersUtil;
            _endpoints = new List<EndPointsModel>(); 
            //_context = context;
            //_authorizeRepository = authorizeRepository;
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }


        //public async Task<JsonWebToken> LoginFromAuth(string GuID)
        //{

        //}

        public async Task<AuthorizeDataResp> LoginAsnc(LoginParam data)
        {

            logModel.AddLogAlive("Start LoginAsnc");
            string Auth_UserID = null;
            string UserName = null;
            string EmployeeNo = "";
            string DefaultAPP = "";

            var result = new AuthorizeDataResp();

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
             
                var users = new User();
                var refreshToken = await GenerateNewRefreshTokenAsync(users.ID);

                result.data = CreateJsonWebToken(users.ID, users.DisplayName, refreshToken);
                result.data.EmployeeNo = "AP006006";
                result.data.display_name = users.DisplayName;
                result.data.refresh_token = refreshToken;
                //result.data.Auth_UserID = guidToken.ToString();
                result.data.Username = users.Email.Replace("@apthai.com", "");

                result.IsSuccess = true;
                result.Message = "Success";
            logModel.AddLogAlive("End LoginAsnc");

            //}
            return result;
        }

        public async Task<AuthorizeDataResp> LoginFromAuthByTokenAsnc(string uToken)
        {
            logModel.AddLogAlive("Start LoginFromAuthByTokenAsnc");
            var userToken = new UserToken();
            userToken.Token = uToken;

            Guid? guidToken = Guid.Empty;
            var employeeNo_AD = string.Empty;

            string APApiKey = Environment.GetEnvironmentVariable("API_Key");
            if (APApiKey == null)
                APApiKey = "z66PDOx/wrdRcfI38UAWy+eb6pw7ivpNdpz+eYJQScNX19mbFiA87KZvF2/qnLx+6JWUaNbBSrUtC4rYjOB4HIayTQU=";

            string APApiToken = Environment.GetEnvironmentVariable("Api_Token");
            if (APApiToken == null)
                APApiToken = "CtOcOl54qoQVGAp6XaRnfI/PC/77cjN6c4tVj76uXwT+sEjnimRK8j2Dw+7uWPtqSSpt+rr/vZcswsd1o+V1phuvsBv0Ag==";

            var client = new HttpClient();
            var Content = new StringContent(JsonConvert.SerializeObject(userToken));
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Content.Headers.Add("api_key", APApiKey);
            Content.Headers.Add("api_token", APApiToken);

            string PostURL = Environment.GetEnvironmentVariable("AuthorizeURL");

            if (PostURL == null)
                PostURL = "https://authorizewebservice.apthai.com/newapi/Api/authorize/";

            PostURL += "GetUserByToken";

            var result = new AuthorizeDataResp();

            var Respond = await client.PostAsync(PostURL, Content);
            if (Respond.StatusCode != System.Net.HttpStatusCode.OK)
            {
                result.IsSuccess = false;
                result.Message = "LogIn GetUserByToken Invalid With User In A.D.";
            }
            else
            {
                var RespondData = await Respond.Content.ReadAsStringAsync();
                UserTokenResult Result = JsonConvert.DeserializeObject<UserTokenResult>(RespondData);

                if (Result.Success == false)
                {
                    result.IsSuccess = false;
                    result.Message = "ไม่สามารถเข้าสู่ระบบได้ เนื่องจาก: " + Result.Message;
                }
                else
                {
                    if (Result.data != null)
                    {
                        guidToken = Result.data.userGUID;
                        employeeNo_AD = Result.data.empCode;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "ไม่สามารถเข้าสู่ระบบได้ เนื่องจาก: Data Null";
                    }
                }
            }

            // var users = (await _authorizeRepository.GetUserRoleAsync(employeeNo_AD)).FirstOrDefault();
            var users = new User();
            if (users == null)
            {
                result.IsSuccess = false;
                result.Message = "User data by GuID is not exist !!!";
            }
            else
            {

                var refreshToken = await GenerateNewRefreshTokenAsync(users.ID);

                result.data = CreateJsonWebToken(users.ID, users.DisplayName, refreshToken);

                result.data.EmployeeNo = employeeNo_AD;
                result.data.display_name = users.DisplayName;
                result.data.refresh_token = refreshToken;
                result.data.Auth_UserID = guidToken.ToString();
                result.data.Username = users.Email.Replace("@apthai.com", "");

                result.IsSuccess = true;
                result.Message = "Success";
            }
            logModel.AddLogAlive("End LoginFromAuthByTokenAsnc");
            return result;
        }

        private async Task<string> GenerateNewRefreshTokenAsync(Guid userID)
        {
            try
            {
                logModel.AddLogAlive("Start GenerateNewRefreshTokenAsync");
                var r = false;
                var t = Guid.NewGuid().ToString("N");
                //var ur = new UsrRefreshToken()
                //{
                //    Token = t,
                //    UserId = userID,
                //    ExpireDate = DateTime.UtcNow.AddDays(14),
                //    IsActive = true,
                //    IsDeleted = false,
                //    Created = DateTime.Now,
                //    Updated = DateTime.Now
                //};


                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //r = await _authorizeRepository.AddRefreshTokenAsync(ur);
                    scope.Complete();
                }
                logModel.AddLogAlive("end GenerateNewRefreshTokenAsync");
                return r ? t : "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonWebToken CreateJsonWebToken(Guid userId, string displayName, string refreshToken = null)
        {
            logModel.AddLogAlive("Start CreateJsonWebToken");
            string jwt_ExpiryMinutes = Environment.GetEnvironmentVariable("jwt_ExpiryMinutes");
            string jwt_Issuer = Environment.GetEnvironmentVariable("jwt_Issuer");
            string jwt_SecretKey = Environment.GetEnvironmentVariable("jwt_SecretKey");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwt_Issuer,
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwt_ExpiryMinutes)),
                Subject = new ClaimsIdentity(new List<Claim> {
                                                                new Claim("userid", userId.ToString()),
                                                                new Claim("authtype", "user")
                                                             }
                                            ),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_SecretKey)), SecurityAlgorithms.HmacSha256)
            };

            var jwt = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(tokenDescriptor.Expires.Value.Ticks - centuryBegin.Ticks).TotalSeconds);
            var expIn = (long)TimeSpan.FromMinutes(Convert.ToDouble(jwt_ExpiryMinutes)).TotalSeconds;
            logModel.AddLogAlive("End CreateJsonWebToken");
            return new  JsonWebToken
            {
                token = token,
                expires = exp,
                expires_in = expIn,
                refresh_token = refreshToken,
                user_id = userId,
                display_name = displayName
            };
        }
        
    
        public async Task<IntrospecDTO> GetIntroSpect(IntrospecReqDTO data)
        {
            logModel.SubModule = "GetIntroSpect";
            logModel.AddLogAlive("Start GetIntroSpect");
            var result = new IntrospecDTO();

            // string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //string? token = data?.Token.ToString();
            result = await _authenticationService.GetIntrospect(data.Token);

            //เพิ่ม get user ภายในถังเรา   return ออกไปให้หน้าบ้าน อม
            //var userData = await _authorizeRepository.GetUserAsync(result.EmpCode);
            var userData = new User();
            //set data HeadersUtil 
            //_headersUtil.SetUserData(userData.ID.ToString(), userData.EmployeeNo);

            //var userHttpContext = _httpContextAccessor.HttpContext.Request?.Headers["userid"].ToString() ?? "";
            result.user_id = Guid.Parse(userData.ID.ToString());

            //fix store outsource ห้ามหาย

            logModel.AddLogAlive("End GetIntroSpect");
            return result;
        }
    }
}
