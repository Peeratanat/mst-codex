using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using Database.Models.DbQueries.ACC; 
using Database.Models.SAL; 
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models.ACC;
using Database.Models.MasterKeys;

namespace Base.DTOs.RFN
{
    public class TransferDTO
    {
        /// <summary>
        /// TransferID
        /// </summary>
        [Description("TransferID")]
        public Guid? TransferID { get; set; }
        /// <summary>
        /// AgreementID
        /// </summary>
        [Description("AgreementID")]
        public Guid? AgreementID { get; set; }

    }
}
