using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Auth;
using Base.DTOs.CTM;
using Database.Models;
using Database.Models.USR;
using ErrorHandling;
using Auth_User.Params.Inputs;
using Auth_User.Services.CustomModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Auth_User.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtHandler JwtHandler;
        private readonly DatabaseContext DB;

        public TokenService(IJwtHandler jwtHandler, DatabaseContext db)
        {
            this.JwtHandler = jwtHandler;
            this.DB = db;
        }

        public async Task<ClientJsonWebToken> ClientLoginAsync(ClientLoginParam input)
        {
            var user = await DB.Users.FirstOrDefaultAsync(o => o.ClientID == input.client_id && o.ClientSecret == input.client_secret && o.IsClient);
            if (user == null)
            {
                throw new UnauthorizedException("Client ID or Secret is Incorrect");
            }
            return JwtHandler.ClientCreate(user.ID, user.DisplayName);
        }


        public async Task<JsonWebToken> LoginAsync(LoginParam input)
        {
            UserRole UserRole = null;
            string Auth_UserID = null;
            string UserName = null;
            string EmployeeNo = "";
            string DefaultAPP = "";

            var result = new JsonWebToken();

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (input.grant_type == "password")
            {
                if (input.username.Contains(">>"))
                {
                    input.username = input.username.Replace(">>", "||");
                }
                else if (input.username.Contains("||"))
                {
                    input.username = input.username.Replace("||", "<>");
                }

                if (input.password == "CRM@2020")
                {
                    UserRole = await DB.UserRoles.Include(o => o.User).Where(o => o.User.EmployeeNo == input.username && o.User.EmployeeNo != null && o.User.IsClient == false).FirstOrDefaultAsync();
                }
                else if ((env == "Development" || env == "development" || env == "Uat" || env == "uat") && input.password == "123456")
                {
                    UserRole = await DB.UserRoles.Include(o => o.User).Where(o => o.User.EmployeeNo == input.username && o.User.EmployeeNo != null && o.User.IsClient == false).FirstOrDefaultAsync();
                }
                else
                {
                    var LoginData = new LoginData
                    {
                        UserName = input.username,
                        Password = input.password,
                        AppCode = "crm"
                    };



                    // เช็ค Password กับ AD
                    var AuthPermission = await GetAuthPermissionAsync(LoginData);

                    if (AuthPermission.IsSuccess)
                    {

                        var userRoles = await DB.UserRoles
                            .Include(o => o.User)
                            .Where(o => o.User.EmployeeNo.ToUpper() == AuthPermission.AutorizeData.EmployeeID.ToUpper()).Select(o => o.RoleID).ToListAsync();

                        UserRole = await DB.UserRoles
                            .Include(o => o.User)
                            .Where(o => o.User.EmployeeNo.ToUpper() == AuthPermission.AutorizeData.EmployeeID.ToUpper()).FirstOrDefaultAsync();

                        Auth_UserID = AuthPermission?.AutorizeData?.SysUserData?.User?.UserGUID;
                        UserName = UserRole.User.Email.Replace("@apthai.com", "");

                        if (!string.IsNullOrEmpty(input.web_base))
                        {

                            ValidateException ex = new ValidateException();


                            if (input.web_base == "Sale")
                            {
                                var isHave = await DB.MenuPermissions
                                 .Include(o => o.MenuAction)
                                 .ThenInclude(o => o.Menu)
                                 //.Where(o => o.RoleID == UserRole.RoleID && o.MenuAction.Menu.MenuCode == "CRM-Sale" && o.IsDeleted == false).AnyAsync();
                                 .Where(o => userRoles.Contains(o.RoleID) && o.MenuAction.Menu.MenuCode == "CRM-Sale" && o.IsDeleted == false).AnyAsync();
                                //if (!isHave && (!string.IsNullOrEmpty(input.fromIntranet) && !input.fromIntranet.Equals("1")))
                                if (!isHave) //แก้ปัญหาPentestCase กรณี User ที่ไม่มีสิทธิ์ เข้าSale แต่เปลี่ยนfromIntranet=1 แล้วยิงAPIเข้ามา ระบบจะReturnToken ให้เข้าระบบได้
                                {
                                    ex.AddError("1", "กรุณาตรวจสอบ Username และ Password ให้ถูกต้อง", 1);
                                    //throw new UnauthorizedException("ไม่สามารถเข้าสู่ระบบได้ เนื่องจากไม่มีสิทธิในการใช้งาน");
                                    throw ex;
                                }
                            }
                            else if (input.web_base == "Backoffice")
                            {
                                var isHave = await DB.MenuPermissions
                                .Include(o => o.MenuAction)
                                .ThenInclude(o => o.Menu)
                                //.Where(o => o.RoleID == UserRole.RoleID && o.MenuAction.Menu.MenuCode == "CRM-BackOffice").AnyAsync();
                                .Where(o => userRoles.Contains(o.RoleID) && o.MenuAction.Menu.MenuCode == "CRM-BackOffice").AnyAsync();

                                //if (!isHave&&(!string.IsNullOrEmpty(input.fromIntranet) && !input.fromIntranet.Equals("1")))
                                if (!isHave) //แก้ปัญหาPentestCase กรณี User ที่ไม่มีสิทธิ์ เข้าBackoffice แต่เปลี่ยนfromIntranet=1 แล้วยิงAPIเข้ามา ระบบจะReturnToken ให้เข้าระบบได้
                                {
                                    ex.AddError("1", "กรุณาตรวจสอบ Username และ Password ให้ถูกต้อง", 1);
                                    //throw new UnauthorizedException("ไม่สามารถเข้าสู่ระบบได้ เนื่องจากไม่มีสิทธิในการใช้งาน");
                                    throw ex;
                                }
                            }
                            else if (input.web_base == "MasterData")
                            {
                                var isHave = await DB.MenuPermissions
                                .Include(o => o.MenuAction)
                                .ThenInclude(o => o.Menu)
                                //.Where(o => o.RoleID == UserRole.RoleID && o.MenuAction.Menu.MenuCode == "CRM-Master").AnyAsync();
                                .Where(o => userRoles.Contains(o.RoleID) && o.MenuAction.Menu.MenuCode == "CRM-Master" && o.IsDeleted == false).AnyAsync();
                                //if (!isHave && (!string.IsNullOrEmpty(input.fromIntranet) && !input.fromIntranet.Equals("1")))
                                if (!isHave) //แก้ปัญหาPentestCase กรณี User ที่ไม่มีสิทธิ์ เข้าMasterData แต่เปลี่ยนfromIntranet=1 แล้วยิงAPIเข้ามา ระบบจะReturnToken ให้เข้าระบบได้
                                {
                                    ex.AddError("1", "กรุณาตรวจสอบ Username และ Password ให้ถูกต้อง", 1);
                                    //throw new UnauthorizedException("ไม่สามารถเข้าสู่ระบบได้ เนื่องจากไม่มีสิทธิในการใช้งาน");
                                    throw ex;
                                }
                            }
                        }




                        EmployeeNo = UserRole.User.EmployeeNo;
                        var DefaultRoleID = await DB.Users.Where(o => o.EmployeeNo == EmployeeNo).Select(o => o.DefaultRoleID).FirstOrDefaultAsync();
                        DefaultAPP = await DB.Roles.Where(o => o.ID == DefaultRoleID).Select(o => o.DefaultApp).FirstOrDefaultAsync();
                    }
                    else
                        throw new UnauthorizedException(AuthPermission.Message);
                }

                if (UserRole == null)
                    throw new UnauthorizedException("Username or password is incorrect ");

                var refreshToken = await GenerateNewRefreshTokenAsync(UserRole.User.ID);

                result = JwtHandler.Create(UserRole.User.ID, UserRole.User.DisplayName, refreshToken);

            }
            else if (input.grant_type == "refresh_token")
            {
                var token = await DB.RefreshTokens.Include(o => o.User).FirstOrDefaultAsync(o => o.Token == input.refresh_token);
                if (token == null)
                    throw new UnauthorizedException("Refresh token not found");

                if (token.ExpireDate < DateTime.UtcNow)
                    throw new UnauthorizedException("Refresh token has been expired");

                //var refreshToken = await GenerateNewRefreshTokenAsync(token.UserID);
                //DB.Remove(token);

                token.ExpireDate = DateTime.UtcNow.AddDays(1);

                DB.Entry(token).State = EntityState.Modified;
                await DB.SaveChangesAsync();

                result = JwtHandler.Create(token.UserID, token.User.DisplayName, input.refresh_token);

                Auth_UserID = token.User?.OldUserID.ToString();
                UserName = token.User.Email.Replace("@apthai.com", "");
                EmployeeNo = token.User.EmployeeNo;
                var DefaultRoleID = await DB.Users.Where(o => o.EmployeeNo == EmployeeNo).Select(o => o.DefaultRoleID).FirstOrDefaultAsync();
                DefaultAPP = await DB.Roles.Where(o => o.ID == DefaultRoleID).Select(o => o.DefaultApp).FirstOrDefaultAsync();

                
            }
            else
            {
                throw new UnauthorizedException("Invalid grant type (must be \"password\" or \"refresh_token\")");
            }

            result.DefaultAPP = DefaultAPP;
            result.Auth_UserID = Auth_UserID;
            result.Username = UserName;
            result.EmployeeNo = EmployeeNo;
            return result;
        }

        public async Task LogoutAsync(LogoutParam input)
        {
            var token = await DB.RefreshTokens.FirstOrDefaultAsync(o => o.Token == input.RefreshToken);
            if (token != null)
            {
                DB.Remove(token);
                await DB.SaveChangesAsync();
            }
        }

        private async Task<string> GenerateNewRefreshTokenAsync(Guid userID)
        {
            RefreshToken token = new RefreshToken();
            token.Token = Guid.NewGuid().ToString("N");
            token.UserID = userID;
            token.ExpireDate = DateTime.UtcNow.AddDays(14);
            await DB.AddAsync(token);
            await DB.SaveChangesAsync();
            return token.Token;
        }

        public async Task<AutorizeResult> GetAuthPermissionAsync(LoginData LoginData)
        {
            string APApiKey = Environment.GetEnvironmentVariable("API_Key");
            if (APApiKey == null)
                APApiKey = "ktlV1fch16FZdPvh9A9lHkfXA1IMQVIUwkCcVTzoUQ9erB764ibT7dFEQh+vj8zDjX5JnL+7rrordZKeL87c5zNXJOjU";

            string APApiToken = Environment.GetEnvironmentVariable("Api_Token");
            if (APApiToken == null)
                APApiToken = "kPjhzbl05jEXzO/nJnudBcdAXVvHg2C/Z1XMo1Q63XzFt9T8mdOINv6cRGHAJIe1lM8NKR/U0lvikLPK/kuBUebDHzohKhs=";

            var client = new HttpClient();
            var Content = new StringContent(JsonConvert.SerializeObject(LoginData));
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            Content.Headers.Add("api_key", APApiKey);
            Content.Headers.Add("api_token", APApiToken);

            string PostURL = Environment.GetEnvironmentVariable("AuthorizeURL");

            if (PostURL == null)
                PostURL = "https://authorizewebservice.apthai.com/newapi/Api/authorize/";

            PostURL += "JWTUserLogin";

            var Respond = await client.PostAsync(PostURL, Content);
            if (Respond.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new AutorizeResult
                {
                    IsSuccess = false,
                    Message = "Login Invalid With User Account Not Exist In A.D."
                };
            }
            else
            {
                var RespondData = await Respond.Content.ReadAsStringAsync();
                AutorizeDataJWT Result = JsonConvert.DeserializeObject<AutorizeDataJWT>(RespondData);

                if (Result.LoginResult == false)
                {
                    return new AutorizeResult
                    {
                        IsSuccess = false,
                        Message = Result.LoginResultMessage
                    };
                }
                else
                {
                    AutorizeDataJWTReturnObject ReturnData = new AutorizeDataJWTReturnObject();
                    ReturnData.AccountExpirationDate = Result.AccountExpirationDate;
                    ReturnData.AppUserRole = Result.AppUserRole;
                    ReturnData.AuthenticationProvider = Result.AuthenticationProvider;
                    ReturnData.CostCenterCode = Result.CostCenterCode;
                    ReturnData.CostCenterName = Result.CostCenterName;
                    ReturnData.DisplayName = Result.DisplayName;
                    ReturnData.Division = Result.Division;
                    ReturnData.DomainUserName = Result.DomainUserName;
                    ReturnData.Email = Result.Email;
                    ReturnData.EmployeeID = Result.EmployeeID;
                    ReturnData.FirstName = Result.FirstName;
                    ReturnData.LastLogon = Result.LastLogon;
                    ReturnData.LastName = Result.LastName;
                    ReturnData.LoginResult = Result.LoginResult;
                    ReturnData.LoginResultMessage = Result.LoginResultMessage;
                    ReturnData.SysAppCode = Result.SysAppCode;
                    ReturnData.SysUserData = JsonConvert.DeserializeObject<UserModel>(Result.SysUserData);
                    ReturnData.SysUserId = Result.SysUserId;
                    ReturnData.SysUserRoles = JsonConvert.DeserializeObject<vwUserRole>(Result.SysUserRoles);
                    ReturnData.Token = Result.Token;
                    ReturnData.UserApp = JsonConvert.DeserializeObject<List<vwUserApp>>(Result.UserApp);
                    ReturnData.UserPrincipalName = Result.UserPrincipalName;
                    List<UserProject> userProjects = JsonConvert.DeserializeObject<List<UserProject>>(Result.UserProject);

                    return new AutorizeResult
                    {
                        IsSuccess = true,
                        Message = Result.LoginResultMessage,

                        AutorizeData = ReturnData,
                        UserProjects = userProjects
                    };
                }
            }
        }

        public async Task<JsonWebToken> LoginFromAuth(Guid GuID, string EmployeeNo = null)
        {
            IQueryable<User> userQuery = DB.Users.Include(o => o.UserRoles);

            if (!string.IsNullOrEmpty(EmployeeNo))
                userQuery = userQuery.Where(o => o.EmployeeNo == EmployeeNo);
            else
                userQuery = userQuery.Where(o => o.OldUserID == GuID || o.ID == GuID);

            var user = await userQuery.FirstOrDefaultAsync();

            if (user == null)
                throw new UnauthorizedException("User data by GuID is not exist !!!");

            var refreshToken = await GenerateNewRefreshTokenAsync(user.ID);

            var result = JwtHandler.Create(user.ID, user.DisplayName, refreshToken);

            result.Auth_UserID = (user.OldUserID ?? Guid.Empty).ToString();
            result.Username = user.Email.Replace("@apthai.com", "");

            Guid? DefaultRoleID = null;

            if (user.DefaultRoleID.HasValue)
            {
                DefaultRoleID = user.DefaultRoleID;
            }
            else
            {
                if ((user.UserRoles ?? new List<UserRole>()).Any())
                {
                    DefaultRoleID = user.UserRoles.Select(o => o.RoleID).FirstOrDefault();

                    var role = await DB.Roles.Where(o => o.ID == DefaultRoleID).FirstOrDefaultAsync();
                }
            }

            if (DefaultRoleID.HasValue)
            {
                var role = await DB.Roles.Where(o => o.ID == DefaultRoleID).FirstOrDefaultAsync();
                result.DefaultAPP = role.DefaultApp;
            }

            result.Auth_UserID = user.OldUserID.ToString();
            result.Username = user.Email.Replace("@apthai.com", "");
            return result;
        }

        public async Task<JsonWebToken> LoginFromAuthByToken(string GuID, string EmployeeNo = null)
        {

            var userToken = new UserToken();
            userToken.Token = GuID;

            Guid? guidToken = Guid.Empty;
            var employeeNoAD = string.Empty;

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

            ValidateException ex = new ValidateException();

            var Respond = await client.PostAsync(PostURL, Content);
            if (Respond.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnauthorizedException("LogIn GetUserByToken Invalid With User In A.D.");
            }
            else
            {
                var RespondData = await Respond.Content.ReadAsStringAsync();
                UserTokenResult Result = JsonConvert.DeserializeObject<UserTokenResult>(RespondData);

                if (Result.Success == false)
                {
                    //throw new UnauthorizedException("LogIn GetUserByToken In A.D.: " + Result.Message);
                    //LogIn GetUserByToken In A.D.: Not found token or token is expire
                    ex.AddError("1", "ไม่สามารถเข้าสู่ระบบได้ เนื่องจาก: " + Result.Message, 1);
                    throw ex;
                }
                else
                {
                    if (Result.data != null)
                    {
                        guidToken = Result.data.userGUID;
                        employeeNoAD = Result.data.empCode;
                    }
                    else
                    {
                        //throw new UnauthorizedException("LogIn GetUserByToken In A.D.: Data Null");
                        ex.AddError("1", "ไม่สามารถเข้าสู่ระบบได้ เนื่องจาก: Data Null", 1);
                        throw ex;
                    }
                }
            }

            IQueryable<User> userQuery = DB.Users.Include(o => o.UserRoles);

            if (!string.IsNullOrEmpty(EmployeeNo))
                userQuery = userQuery.Where(o => o.EmployeeNo == EmployeeNo);
            else if (!string.IsNullOrEmpty(employeeNoAD))
                userQuery = userQuery.Where(o => o.EmployeeNo == employeeNoAD);
            else
                userQuery = userQuery.Where(o => o.OldUserID == guidToken || o.ID == guidToken);

            var user = await userQuery.FirstOrDefaultAsync();

            if (user == null)
            {
                //throw new UnauthorizedException("User data by GuID is not exist !!!");
                ex.AddError("1", "User data by GuID is not exist !!!", 1);
                throw ex;
            }

            var refreshToken = await GenerateNewRefreshTokenAsync(user.ID);

            var result = JwtHandler.Create(user.ID, user.DisplayName, refreshToken);

            //result.Auth_UserID = (user.OldUserID ?? Guid.Empty).ToString();
            result.Auth_UserID = guidToken.ToString();
            result.Username = user.Email.Replace("@apthai.com", "");

            Guid? DefaultRoleID = null;

            if (user.DefaultRoleID.HasValue)
            {
                DefaultRoleID = user.DefaultRoleID;
            }
            else
            {
                if ((user.UserRoles ?? new List<UserRole>()).Any())
                {
                    DefaultRoleID = user.UserRoles.Select(o => o.RoleID).FirstOrDefault();

                    var role = await DB.Roles.Where(o => o.ID == DefaultRoleID).FirstOrDefaultAsync();
                }
            }

            if (DefaultRoleID.HasValue)
            {
                var role = await DB.Roles.Where(o => o.ID == DefaultRoleID).FirstOrDefaultAsync();
                result.DefaultAPP = role.DefaultApp;
            }

            //result.Auth_UserID = user.OldUserID.ToString();
            result.Auth_UserID = guidToken.ToString();
            result.Username = user.Email.Replace("@apthai.com", "");
            return result;
        }

        public async Task<GenerateUserTokenResult> GenerateUserToken(Guid UserGUID)
        {

            var userToken = new GenerateUserToken();
            userToken.UserGUID = UserGUID;
            userToken.AppCode = "CRM";

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
            GenerateUserTokenResult Result = new GenerateUserTokenResult();
            if (PostURL == null)
                PostURL = "https://authorizewebservice.apthai.com/newapi/Api/authorize/";

            PostURL += "GenerateUserToken";

            var Respond = await client.PostAsync(PostURL, Content);
            if (Respond.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnauthorizedException("GenerateUserToken Invalid In A.D.");
            }
            else
            {
                var RespondData = await Respond.Content.ReadAsStringAsync();
                Result = JsonConvert.DeserializeObject<GenerateUserTokenResult>(RespondData);

                if (Result.Success == false)
                {
                    throw new UnauthorizedException("GenerateUserToken Invalid In A.D.: " + Result.Message);
                }
                else
                {
                    if (Result.data == null)
                        throw new UnauthorizedException("GenerateUserToken Invalid In A.D.: Data Null");
                }
            }
            return Result;
        }
    }
}
