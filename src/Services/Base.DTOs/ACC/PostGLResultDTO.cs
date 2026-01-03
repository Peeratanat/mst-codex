using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Base.DTOs.ACC
{
    public class PostGLResultDTO : BaseDTO
    {
        /// <summary>
        /// Company
        /// </summary>
        [Description("Company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// จำนวนรายการที่ Post สำเร็จ
        /// </summary>
        [Description("จำนวนรายการที่ Post สำเร็จ")]
        public int SuccessRecord { get; set; }

        /// <summary>
        /// จำนวนรายการที่ Post ซ้ำ
        /// </summary>
        [Description("จำนวนรายการที่ Post ซ้ำ")]
        public int ExistPostGLRecord { get; set; }

        public List<Guid> PostGLHeaderIDs { get; set; }
    }
}
