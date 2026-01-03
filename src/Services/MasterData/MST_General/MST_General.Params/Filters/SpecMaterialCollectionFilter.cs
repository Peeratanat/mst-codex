using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class SpecMaterialCollectionFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public string Name { get; set; }
        public string ModelUse { get; set; }
        public bool? IsActive { get; set; }
    }
}
