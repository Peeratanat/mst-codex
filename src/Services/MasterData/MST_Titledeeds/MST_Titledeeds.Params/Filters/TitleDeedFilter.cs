using Base.DTOs.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_Titledeeds.Params.Filters
{
    public class TitleDeedFilter : BaseFilter
    {
        public string UnitNo { get; set; }
        public string TitledeedNo { get; set; }
        public string HouseNo { get; set; }
        public Guid? LandOfficeID { get; set; }
        public string HouseName { get; set; }
        public double? TitledeedAreaFrom { get; set; }
        public double? TitledeedAreaTo { get; set; }
        public double? UsedAreaFrom { get; set; }
        public double? UsedAreaTo { get; set; }
        public string LandNo { get; set; }
        public string LandSurveyArea { get; set; }
        public string LandPortionNo { get; set; }

        public string LandStatusKey { get; set; }
        public string PreferStatusKey { get; set; }
        public string UnitStatusKey { get; set; }

        public DateTime? LandStatusDateFrom {get; set; }
        public DateTime? LandStatusDateTo { get; set; }

        public double? BuildingPermitAreaFrom { get; set; }
        public double? BuildingPermitAreaTo { get; set; }

    }
    public class TitleDeedFilterInput
    {
        public List<TitleDeedDTO> Items { get; set; }

        TitleDeedFilterInput()
        {
            Items = new List<TitleDeedDTO>();
        }
    }
}
