using Database.Models;
using External.WebHttpClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record.PivotTable;
using System.Reflection.Emit;
using System.Security.Claims;

namespace Auth_User.API.Middleware
{
    public class ClaimsTransformationMiddleware : IClaimsTransformation
    {
        private readonly DatabaseContext DB;
        public ClaimsTransformationMiddleware(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "userid";
            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                var EmpCode = principal.FindFirst("ap_empCode")?.Value;
                var userID = (await DB.Users.FirstOrDefaultAsync(x => x.EmployeeNo.Equals(EmpCode)))?.ID;
                claimsIdentity.AddClaim(new Claim(claimType, userID.ToString()));
            }

            principal.AddIdentity(claimsIdentity);
            return await Task.FromResult(principal);
        }
    }
}
