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

namespace Base.DTOs.ACC
{
    public class TransferPostRRDTO
    {
        /// <summary>
        /// Project-Unit
        /// </summary>
        [Description("Project-Unit")]
        public PRJ.UnitDTO Unit { get; set; }

        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        [Description("วันที่โอนกรรมสิทธิ์")]
        public DateTime TransferActualDate { get; set; }

        /// <summary>
        /// ราคาบ้านสุทธิ ณ วันโอน
        /// </summary>
        [Description("ราคาบ้านสุทธิ ณ วันโอน")]
        public decimal TransferPrice { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }
    }
}
