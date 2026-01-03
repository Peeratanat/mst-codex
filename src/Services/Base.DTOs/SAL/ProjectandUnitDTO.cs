using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ใบจอง
    /// Model: Booking
    /// </summary>
    public class ProjectandUnitDTO : BaseDTO
    {
       
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }


        public bool Result { get; set; }
    }
}
