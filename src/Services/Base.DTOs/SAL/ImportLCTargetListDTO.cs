using Base.DTOs.USR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models;
using System;
using ErrorHandling;
using Database.Models.PRJ;
using Base.DTOs.PRJ;

namespace Base.DTOs.SAL
{
    public class ImportLCTargetListDTO : BaseDTO
    {
        public int No { get; set; }
        public UserListDTO Employee { get; set; } 
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? UnitBook { get; set; }
        public int? UnitTransfer { get; set; }
        public ProjectDropdownDTO Project { get; set; }
        public decimal? BookingAmount { get; set; }
        public decimal? TransferAmount { get; set; }
        public string Createby { get; set; }
        public string Status { get; set; }
        public bool IsError { get; set; }

        public void ToModel(ref LCTarget model, Guid? userLogin)
        {
            model.ProjectID = this.Project?.Id ?? Guid.Empty;
            model.EmployeeID = this.Employee?.Id ?? Guid.Empty;
            model.Y = this.Y ?? 0;
            model.Q = this.Q ?? 0;
            model.BookUnit = this.UnitBook ?? 0;
            model.TransferUnit = this.UnitTransfer ?? 0;
            model.BookingAmount = this.BookingAmount ?? 0;
            model.TransferAmount = this.TransferAmount ?? 0 ;
            model.CreatedByUserID = userLogin ;
        }
    }
     
}
