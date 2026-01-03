using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using Database.Models.PRJ;
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
using Base.DTOs.PRJ;

namespace Base.DTOs.ACC
{
    public class SAPUnitStatusDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }

        /// <summary>
        /// PercentProgress
        /// </summary>
        [Description("PercentProgress")]
        public string PercentProgress { get; set; }

        /// <summary>
        /// สถานะปัจจุบัน
        /// </summary>
        [Description("สถานะปัจจุบัน")]
        public MST.MasterCenterDropdownDTO SAPProcessStatusNow { get; set; }

        [Description("วันที่ เวลา เริ่มประมวผล")]
        public DateTime? StartDate { get; set; }

        [Description("วันที่ เวลา ประมวผลเสร็จ")]
        public DateTime? FinishDate { get; set; }


        [Description("ข้อความ")]
        public string Message { get; set; }

        [Description("ชื่อคนสร้างรายการ")]
        public string User { get; set; }

        /// <summary>
        /// สถานะล่าสุด
        /// </summary>
        [Description("สถานะล่าสุด")]
        public MST.MasterCenterDropdownDTO SAPProcessStatusLast { get; set; }

        public static SAPUnitStatusDTO CreateFromModel(SAPUnitStatus model)
        {
            if (model != null)
            {
                var result = new SAPUnitStatusDTO()
                {
                    Project = ProjectDTO.CreateFromModel(model.Project),
                    SAPProcessStatusNow = MasterCenterDropdownDTO.CreateFromModel(model.SAPProcessStatus),
                    PercentProgress = model.PercentProgress != 0 ? model.PercentProgress.ToString() + "%" : null,
                    Message = model.Message,
                    StartDate  = model.StartDate,
                    FinishDate = model.FinishDate
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
    
    public class TranferListTmp
    {
        public Guid? ProjectId { get; set; }
        public int TotalTransferUnit { get; set; }
        public DateTime? LastActualTransferDate { get; set; }
    }
}
