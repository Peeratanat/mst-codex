using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class CompanyDropdownFilter
    {
        public string Name { get; set; }

        public Guid? CompanyID { get; set; }

        public bool? IsWrongCompany { get; set; }
    }
}
