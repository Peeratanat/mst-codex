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
    public class TransferChequeDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        [Description("เลขที่เช็ค")]
        public string ChequeNo { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public BankDTO Bank { get; set; }

        /// <summary>
        /// สั่งจ่าย บริษัท
        /// </summary>
        [Description("สั่งจ่าย บริษัท")]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// วันที่เช็ค
        /// </summary>
        [Description("วันที่เช็ค")]
        public DateTime ChequeDate { get; set; }
    }
}
