using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class AllUnitByProjectDTO
    {
        public string Unit_No { get; set; }
        public string HouseNumber { get; set; }
        public double? Area { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Unit_Type { get; set; }
        public string AssetTypeName { get; set; }
        public string TitledeedNo { get; set; }

        public void ToModel(ref dbqGetAllUnitByProject model)
        {
            model.Unit_No = this.Unit_No;
            model.HouseNumber = this.HouseNumber;
            model.Area = this.Area;
            model.Building = this.Building;
            model.Floor = this.Floor;
            model.Unit_Type = this.Unit_Type;
            model.AssetTypeName = this.AssetTypeName;
            model.TitledeedNo = this.TitledeedNo;
        }

        public static AllUnitByProjectDTO CreateFromQuery(dbqGetAllUnitByProject model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new AllUnitByProjectDTO();
                result.Unit_No = model.Unit_No;
                result.HouseNumber = model.HouseNumber;
                result.Area = model.Area;
                result.Building = model.Building;
                result.Floor = model.Floor;
                result.Unit_Type = model.Unit_Type;
                result.AssetTypeName = model.AssetTypeName;
                result.TitledeedNo = model.TitledeedNo;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
