using Database.Models.MST;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class TransferBankTransferDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        [Description("เลขที่บัญชี")]
        public BankAccountDTO BankAccount { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        [Description("วันที่เงินเข้า")]
        public DateTime BankTransferDate { get; set; }
    }
}
