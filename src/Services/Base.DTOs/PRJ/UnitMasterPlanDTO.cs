using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Database.Models.MST;
using Base.DTOs.EXT;
using Base.DTOs.USR;
using Database.Models.USR;
using Database.Models.DbQueries.PRJ;

namespace Base.DTOs.PRJ
{
    public class UnitMasterPlanDTO
    {
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public double? SalePrice { get; set; }
        public double? SaleArea { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public DateTime? QCPassDate { get; set; }
        public DateTime? InspectionDate { get; set; }

        public static UnitMasterPlanDTO CreateFromModel(dbqGetMasterPlanUnitDetail model)
        {
            if (model != null)
            {
                var result = new UnitMasterPlanDTO()
                {
                    ProjectNameTH = model.ProjectNameTH,
                    UnitNo = model.UnitNo,
                    HouseNo = model.HouseNo,
                    SalePrice = model.SalePrice,
                    SaleArea = model.SaleArea,
                    CustomerName = model.CustomerName,
                    BookingDate = model.BookingDate,
                    AgreementDate = model.AgreementDate,
                    TransferDate = model.TransferDate, 
                    QCPassDate = model.QCPassDate,
                    InspectionDate = model.InspectionDate

                };
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
