using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class ChkErrorDTO
    {
        /// <summary>
        /// MSG POP UP
        /// </summary>
        public bool MsgPopUP { get; set; }
        /// <summary>
        ///  Msg
        /// </summary>
        public string Msg { get; set; }
    }
}
