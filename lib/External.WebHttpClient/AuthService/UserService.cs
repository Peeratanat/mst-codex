using Base.DTOs.AppAuth; 
using Common.Extensions.HeaderUril;
using Common.Helper.Logging;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace External.WebHttpClient
{
    public class UserService : IUserService
    {
        public LogModel LogModel { get; set; } 
        private readonly IHeadersUtils _headersUtil;
        private readonly IAuthenticationService _authenService;
        private readonly DatabaseContext DB;
        public UserService( IHeadersUtils headersUtil, IAuthenticationService authenService  , DatabaseContext db)
        {
            LogModel = new LogModel("UserService", null); 
            _headersUtil = headersUtil;
            _authenService = authenService;
            this.DB = db;
        }

          
        public async Task<ValidateLoginDTO> ValidateMainRole(string token, string empCode)
        {
            //intro spec
            //var userInfo = await _authenService.GetIntrospect(_headersUtil.CurrentToken);
            var userID = await DB.Users.Where(x => x.EmployeeNo.Equals(empCode)).AsNoTracking().Select(x=>x.ID).FirstOrDefaultAsync();
            //get main role
            var result = new ValidateLoginDTO()
            {
                UserLogin = empCode,
                UserID = userID.ToString()
            };

            return result;
        }
    }
}