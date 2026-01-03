using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class TranferPromotionListDTO
    {

        public Guid? ID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectDisplayName { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string MainOwnerName { get; set; }
        public string TranferPromotionNo { get; set; }


        public static TranferPromotionListDTO CreateFromModel(TransferPromotion model , DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TranferPromotionListDTO()
                {
                    ID = model.ID,
                    ProjectNo = model.Booking.Project.ProjectNo,
                    ProjectNameTH = model.Booking.Project.ProjectNameTH,
                    ProjectDisplayName = model.Booking.Project.ProjectNo + "-" + model.Booking.Project.ProjectNameTH,
                    UnitNo = model.Booking.Unit.UnitNo,
                    HouseNo = model.Booking.Unit.HouseNo,
                    MainOwnerName = DB.Agreements.Where(o => o.BookingID == model.Booking.ID).Select(o => o.MainOwnerName).FirstOrDefault(),
                    TranferPromotionNo = model.TransferPromotionNo
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
