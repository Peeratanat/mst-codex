using Base.DTOs.MST;
using Database.Models;
using Database.Models.ACC;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.ACC
{
    public class TransferPostKRDTO
    {

        /// <summary>
        /// รหัส-ชื่อ GLAccount
        /// </summary>
        [Description("รหัส-ชื่อ GLAccount")]
        public MST.BankAccountDTO GLAccount { get; set; }

        /// <summary>
        /// ชื่อเจ้าหน้าที่โอน กรณี Credit เจ้าหนี้พนักงาน
        /// </summary>
        [Description("ชื่อเจ้าหน้าที่โอน กรณี Credit เจ้าหนี้พนักงาน")]
        public string TransferEmpName { get; set; }

        /// <summary>
        /// EmpNo กรณี Credit เจ้าหนี้พนักงาน
        /// </summary>
        [Description("EmpNo กรณี Credit เจ้าหนี้พนักงาน")]
        public string TransferEmpNo { get; set; }

        /// <summary>
        /// จำนวนเงิน Credit
        /// </summary>
        [Description("จำนวนเงิน Credit")]
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// จำนวนเงิน Debit
        /// </summary>
        [Description("จำนวนเงิน Debit")]
        public decimal DebitAmount { get; set; }

        public string Assignment { get; set; }

        public static TransferPostKRDTO CreatePostGLDetailModel(PostGLDetail model)
        {
            if (model != null)
            {
                var result = new TransferPostKRDTO()
                {
                    GLAccount = BankAccountDTO.CreateFromModel(model.GLAccount),
                    //TransferEmpName =model.,
                    CreditAmount = model.PostGLType.Equals("CR") ? model.Amount : 0,
                    DebitAmount = model.PostGLType.Equals("DR") ? model.Amount : 0,
                };
                if (result.GLAccount == null)
                    result.GLAccount = new BankAccountDTO();
                result.GLAccount.GLAccountNo = model.AccountCode;
                if (!string.IsNullOrEmpty( model.AccountName))
                    result.GLAccount.GLAccountType.Name = model.AccountName; 
                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class PostGLDetailTmp
    {
        public PostGLDetail postGLDetail { get; set; }
        public int runing { get; set; }
    }
}