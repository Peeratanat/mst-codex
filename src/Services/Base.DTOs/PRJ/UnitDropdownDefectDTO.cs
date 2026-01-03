using Database.Models;
using Database.Models.DbQueries.PRJ;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitDropdownDefectDTO
    {
        /// <summary>
        /// Identity UnitID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }


        public static UnitDropdownDefectDTO CreateFromSQLQueryResult(dbqUnitDropdownDefect model )
        {
            if (model != null)
            {

                var result = new UnitDropdownDefectDTO
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo
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
