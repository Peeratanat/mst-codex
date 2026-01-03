using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Params.Outputs
{
    public class AuthBCResult
    {
        public string access_token { get; set; } 

        public string token_type { get; set; }

        public int expire_in { get; set; }

    }
}
