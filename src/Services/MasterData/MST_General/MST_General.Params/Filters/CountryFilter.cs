using System;
using MST_General.Params.Filters;
namespace MST_General.Params.Filters
{
    public class CountryFilter : BaseFilter
    {
        public string Code { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
    }
}
