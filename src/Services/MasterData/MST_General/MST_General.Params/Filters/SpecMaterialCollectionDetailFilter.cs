using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class SpecMaterialCollectionDetailFilter : BaseFilter
    {
        public Guid? GroupID { get; set; }
        public string Name { get; set; }
        public string ItemDescription { get; set; }
        public List<string> BGNo { get; set; }
        public string NameEN { get; set; }
        public string ItemDescriptionEN { get; set; }


    }
}
