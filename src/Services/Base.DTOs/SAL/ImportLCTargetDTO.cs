using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using System;
using ErrorHandling;
using System.Collections.Generic;

namespace Base.DTOs.SAL
{
    public class ImportLCTargetDTO : BaseDTO
    {
        public int Total { get; set; }  
        public int Valid { get; set; }  
        public int Invalid { get; set; }  
        public List<ImportLCTargetListDTO> ImportLCTargetList { get; set; }   
        public static async Task ValidateAsync(DatabaseContext db, Guid? UserID)
        {
            ValidateException ex = new ValidateException();
            DateTime today = DateTime.Now.Date;
            var userRoleImportAllday = await db.UserRoles.Include(o => o.User).Include(o => o.Role)
                        .Where(o => o.UserID == UserID //&& o.Role.Code.Equals("")
                        ).FirstOrDefaultAsync();
            if (userRoleImportAllday == null) 
            {
                var month = today.Month;
                var day = today.Day;
                if (month == 1 || month == 4 || month == 7 || month == 10 )
                {
                    if(day <1 || day > 15)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0178").FirstOrDefaultAsync();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                }
                else if(month == 3 || month == 6 || month == 9 || month == 12)
                {
                    if (day < 20 || day > 25)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0178").FirstOrDefaultAsync();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
     
}
