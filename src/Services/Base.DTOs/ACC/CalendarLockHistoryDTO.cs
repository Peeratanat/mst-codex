using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using Database.Models.ACC;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Base.DTOs.USR;
using static Base.DTOs.ACC.CalendarLockDTO;

namespace Base.DTOs.ACC
{
    public class CalendarLockHistoryDTO
    {
        /// <summary>
        /// บริษัทที่ Lock
        /// </summary>
        [Description("ชื่อย่อบริษัท")]
        public string Company { get; set; }

        /// <summary>
        /// วันที่ Lock ในปฏิทิน
        /// </summary>
        [Description("วันที่ Lock ในปฏิทิน")]
        public DateTime LockDate { get; set; }

        /// <summary>
        /// ผู้ดำเนินการ Lock
        /// </summary>
        [Description("ผู้ดำเนินการ Lock")]
        public string User { get; set; }

        /// <summary>
        /// วันที่บันทึกรายการ
        /// </summary>
        [Description("วันที่บันทึกรายการ")]
        public DateTime? UpdatedDate { get; set; }


        public static CalendarLockHistoryDTO CreateFromModel(CalendarLockQueryResult model)
        {
            if (model != null)
            {
                var result = new CalendarLockHistoryDTO();
                    result = new CalendarLockHistoryDTO()
                    {
                        Company = model.Company?.SAPCompanyID,
                        LockDate = model.CalendarLock.LockDate,
                        User =( model.UpdatedBy.FirstName ?? null )+ " " + (model.UpdatedBy.LastName) ?? null,
                        UpdatedDate = model.CalendarLock?.Updated
                    };
                //else
                //{
                //    result = new CalendarLockHistoryDTO()
                //    {
                //        Company = model.Company.Code,
                //        LockDate = model.CalendarLock.LockDate,
                //        UpdatedDate = model.CalendarLock.Updated
                //    };
                //}
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
