using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// GetContactIDbyFGFCodeDTO
    /// </summary>
    public class ContactIDbyFGFCodeDTO
    {
        public string Status_code { get; set; }

        public string Status_message { get; set; }

        public string ContactID { get; set; }
    }
}
