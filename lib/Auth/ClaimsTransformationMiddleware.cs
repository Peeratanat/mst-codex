using Database.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Common.Middleware
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
                var userID = (await DB.Users.Where(x => x.EmployeeNo.Equals(EmpCode)).AsNoTracking().FirstOrDefaultAsync())?.ID;
                claimsIdentity.AddClaim(new Claim(claimType, userID?.ToString()));
            }

            principal.AddIdentity(claimsIdentity);
            return await Task.FromResult(principal);
        }
    }
}
