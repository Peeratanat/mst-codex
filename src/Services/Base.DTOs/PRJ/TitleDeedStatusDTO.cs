using Database.Models;
using Database.Models.DbQueries.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class TitleDeedStatusDTO : BaseDTO
    {
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string TitledeedNo { get; set; }
        public double? TitledeedArea { get; set; }
        public string AllOwnerName { get; set; }
        public double? TotalPrice { get; set; }
        public double? RepayLoan { get; set; }

        public static TitleDeedStatusDTO CreateFromModel(dbqTitleDeedStatusList model)
        {
            if (model != null)
            {
                var result = new TitleDeedStatusDTO();

                result.ProjectNo = model.ProjectNo;
                result.ProjectNameTH = model.ProjectNameTH;
                result.UnitNo = model.UnitNo;
                result.TitledeedNo = model.TitledeedNo;
                result.TitledeedArea = model.TitledeedArea;
                result.AllOwnerName = model.AllOwnerName;
                result.TotalPrice = model.TotalPrice;
                result.RepayLoan = model.RepayLoan;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
