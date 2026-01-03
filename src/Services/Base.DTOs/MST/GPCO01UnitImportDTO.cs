using Base.DTOs.FIN;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.ROI;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.MST
{
    public class GPCO01UnitImportDTO : BaseDTO
    {
        public int Total { get; set; }
        public int Valid { get; set; }
        public int Invalid { get; set; }
        public List<GPUnitDTO> ImportUnitList { get; set; }
        public GPProjectDTO Project { get; set; }

    } 
}
