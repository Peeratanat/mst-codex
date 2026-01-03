using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_User.Params.Filters
{
    public class BaseFilter
    {
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
}
