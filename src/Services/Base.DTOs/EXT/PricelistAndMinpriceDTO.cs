using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class PricelistAndMinpriceDTO 
    {
        /// <summary>
        /// Minprice
        /// </summary>
        public ResultMinAndPricelist Minprice { get; set; }
        /// <summary>
        /// Minprice
        /// </summary>
        public ResultMinAndPricelist Pricelist { get; set; }

        /// <summary>
        /// Postpone
        /// </summary>
        public ResultMinAndPricelist Postpone { get; set; }

        /// <summary>
        /// CancelMemo
        /// </summary>
        public ResultMinAndPricelist CancelMemo { get; set; }

        /// <summary>
        /// TranferPromotion
        /// </summary>
        public ResultMinAndPricelist TranferPromotion { get; set; }
        public ResultMinAndPricelist ChangeUnitAgreement { get; set; }
        public static async Task<Projectlist> CreateFromQueryResultAsync(MinPriceBudgetWorkflowDTO model, DatabaseContext db, Guid? userID)
        {
            if (model != null)
            {
                var result = new Projectlist()
                {
                    ProjectNo = model.Project.ProjectNo,
                    ProjectName = model.Project.ProjectNameTH
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class ResultMinAndPricelist 
    {
        /// <summary>
        /// ลิสของProject
        /// </summary>
        public List<Projectlist> ProjectList { get; set; }
        /// <summary>
        /// ยอดรวม
        /// </summary>
        public int SumByModule { get; set; }

    }

    public class Projectlist 
    {
        /// <summary>
        /// เลขโปรเจค
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโปรเจค
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// ยอดรวม
        /// </summary>
        public int SumByProject { get; set; }

    }


   
    public class BookingQueryResult
    {
        public models.SAL.Booking Booking { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.MST.MasterCenter BookingStatus { get; set; }
        public models.MST.MasterCenter CreateBookingFrom { get; set; }
        public models.SAL.BookingOwner Owner { get; set; }
        public models.USR.User ConfirmBy { get; set; }
    }
}
