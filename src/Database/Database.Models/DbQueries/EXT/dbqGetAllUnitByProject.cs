using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqGetAllUnitByProject
    {
        public string Unit_No { get; set; }
        public string HouseNumber { get; set; }
        public double? Area { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Unit_Type { get; set; }
        public string AssetTypeName { get; set; }
        public string TitledeedNo { get; set; }

    }
}


