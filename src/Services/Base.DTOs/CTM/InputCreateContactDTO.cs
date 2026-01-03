using Database.Models.MasterKeys;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class InputCreateContactDTO
    {
        /// <summary>
        ///  ID ของ Contact
        /// </summary>
        public ContactDTO Contact { get; set; }
        /// <summary>
        /// รหัสของ Contact
        /// </summary>
        public ContactAddressDTO ContactAddress { get; set; }
        public ContactAddressDTO CitizenAddress { get; set; }

    }
}