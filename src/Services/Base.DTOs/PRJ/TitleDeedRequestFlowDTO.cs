using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
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
    public class TitleDeedRequestFlowDTO : BaseDTO
    {
        public Guid ID { get; set; }
        public DateTime? ScheduleTransferDate { get;set;}
        public DateTime? RequestDate { get; set; }

        public static TitleDeedRequestFlowDTO CreateFromModel(TitledeedRequestFlow model)
        {
            if (model != null)
            {
                var result = new TitleDeedRequestFlowDTO();
                result.ID = model.ID;
                result.ScheduleTransferDate = model.ScheduleTransferDate;
                result.RequestDate = model.RequestDate;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;
                //result.ID = model.ID;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
