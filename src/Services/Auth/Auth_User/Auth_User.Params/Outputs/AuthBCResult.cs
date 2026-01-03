using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth_User.Params.Outputs
{
    public class AuthBCResult 
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expire_in { get; set; }

    }
}
