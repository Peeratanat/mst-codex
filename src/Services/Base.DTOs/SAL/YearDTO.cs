using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;
using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using System.Linq;
using Base.DTOs.PRJ;
using Database.Models.DbQueries.SAL;

namespace Base.DTOs.SAL
{
    public class YearDTO
    {
        /// <summary>
        /// ปี
        /// </summary>
        public int Y { get; set; }

		public static YearDTO CreateFromQuery(dbqGetYearsList model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new YearDTO();
				result.Y = model.Y;
				return result;
			}
			else
			{
				return null;
			}
		}


	}
}
