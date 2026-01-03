using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs.ACC;
using Database.Models.ACC;
using Base.DTOs.MST;
using Base.DTOs.USR;
using System.Globalization;

namespace Base.DTOs.ACC
{
    public class CalendarLockDTO
    {

        /// <summary>
        /// วันที่ Lock ในปฏิทิน
        /// </summary>
        [Description("วันที่ Lock ในปฏิทิน")]
        public DateTime LockDate { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public MST.CompanyDTO Company { get; set; }

        /// <summary>
        /// Lock อยู่หรือไม่
        /// </summary>
        [Description("Lock อยู่หรือไม่")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// ผู้ดำเนินการ Lock
        /// </summary>
        [Description("ผู้ดำเนินการ Lock")]
        public USR.UserDTO User { get; set; }

        /// <summary>
        /// ข้อมูลประวัติการ Lock
        /// </summary>
        [Description("ข้อมูลประวัติการ Lock")]
        public List<CalendarLockHistoryDTO> CalendarLockHistory { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [Description("ID ของ Company")]
        public Guid? CompanyID { get; set; }


        /// <summary>
        /// รอลบหลังจากทำ authorize
        /// </summary>
        [Description("ID ของ Company")]
        public Guid? UserID { get; set; }


        public static CalendarLockDTO CreateFromModel(CalendarLock model)
        {
            if (model != null)
            {
                var result = new CalendarLockDTO()
                {
                    LockDate = model.LockDate,
                    IsLocked = model.IsLocked,
                    CompanyID = model.CompanyID,
                    UserID = model.UserID,
                    Company = CompanyDTO.CreateFromModel(model.Company),
                    User = UserDTO.CreateFromModel(model.User)

                    // CalendarLockHistory = CalendarLockHistoryDTO.CreateFromModel(model.)
                };
                return result;
            }
            else
            {
                return null;
            }
        }


        public static CalendarLockDTO CreateFromQueryResultByGetHistory(CalendarLockQueryResult model)
        {
            if (model != null)
            {
                var result = new CalendarLockDTO()
                {
                    LockDate = model.CalendarLock.LockDate,
                    IsLocked = model.CalendarLock.IsLocked,
                    Company = CompanyDTO.CreateFromModel(model.Company)

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static CalendarLockDTO CreateFromQueryResult(CalendarLockQueryResult model)
        {
            if (model != null)
            {
                var result = new CalendarLockDTO()
                {
                    LockDate = model.CalendarLock.LockDate,
                    IsLocked = model.CalendarLock.IsLocked,
                    Company = CompanyDTO.CreateFromModel(model.Company),
                    // CalendarLockHistory = CalendarLockHistoryDTO.CreateFromModel(model.CalendarLockHistory)

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref CalendarLockQueryResult model)
        {
            model.CalendarLock.LockDate = this.LockDate;
            model.CalendarLock.IsLocked = this.IsLocked;
            model.CalendarLock.CompanyID = this.CompanyID;
            model.CalendarLock.Updated = DateTime.Now;
        }

        public class CalendarLockQueryResult
        {
            public CalendarLock CalendarLock { get; set; }
            public Company Company { get; set; }
            public List<CalendarLockHistoryDTO> CalendarLockHistory { get; set; }
            public User UpdatedBy { get; set; }

        }
        public class CalendarLockResult
        {
            public DateTime Date { get; set; }
            public string Guid { get; set; }
            public DateTime CalendarLock { get; set; }
            public int staus { get; set; }
        }

        public void ToModel(ref CalendarLock model)
        {
            model.LockDate = this.LockDate;
            model.IsLocked = this.IsLocked;
            model.CompanyID = this.Company.Id;
        }

    }
}
