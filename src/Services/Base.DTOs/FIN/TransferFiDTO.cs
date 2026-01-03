using Database.Models.MST;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.SAL;
using Database.Models.FIN;
using Database.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class TransferFiDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลการโอนกรรมสิทธิ์
        /// </summary>
        [Description("ข้อมูลการโอนกรรมสิทธิ์")]
        public SAL.TransferDTO Transfer { get; set; }

        public bool isError { get; set; }
        public string ErrorMsg { get; set; }

        public List<Guid> paymentIdList { get; set; }

        public async Task<TransferFiDTO> ValidateAsync(DatabaseContext db, Transfer transfer, Guid PaymentStateTranferID)
        { 
            Payment payment = await db.Payments.Where(x => x.BookingID == (transfer != null ? transfer.Agreement.BookingID : new Guid()) && x.PaymentStateMasterCenterID == PaymentStateTranferID && x.IsCancel == false).FirstOrDefaultAsync();
            if (transfer != null && transfer.IsPaymentConfirmed == true && payment != null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0127").FirstOrDefaultAsync();
                var msg = errMsg.Message;
                TransferFiDTO result = new TransferFiDTO { isError = true, ErrorMsg = msg };
                return result;
            }
            return null;
        } 
    }
    public class EqualizePayment
    {
        public bool haveCash { get; set; }
        public bool haveCheque { get; set; }
        public bool haveBankTransfer { get; set; }
        public decimal sumCash { get; set; }
        public decimal sumCheque { get; set; }
        public decimal sumBankTransfer { get; set; }
        //จ่ายค่าที่ดิน
        public List<TransferExpense> PayToLandList { get; set; }
        //จ่ายค่านิติ
        public List<TransferExpense> PayToLegalList { get; set; }
        //จ่ายค่า AP
        public List<TransferExpense> PayToAPList { get; set; }
    }

    public class TransferCashTemp
    {
        public Guid Id { get; set; }
        public Guid TransferID { get; set; }
        public Guid? CashPayToMasterCenterID { get; set; }
        public MasterCenter CashPayTo { get; set; }
        public decimal Amount { get; set; }
        public Guid paymentMethodID { get; set; }
    }

    public class TransferChequeTemp
    {
        public Guid Id { get; set; }
        public Guid TransferID { get; set; }
        public Guid? ChequePayToMasterCenterID { get; set; }
        public MasterCenter ChequePayTo { get; set; }
        public Guid? BankID { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? PayDate { get; set; }
        public Guid? PayToCompanyID { get; set; }
        public bool IsWrongCompany { get; set; }
        public decimal Amount { get; set; }
        public Guid paymentMethodID { get; set; }
    }

    public class TransferBankTransferTemp
    {
        public Guid Id { get; set; }
        public Guid TransferID { get; set; }
        public Guid? BankTransferPayToMasterCenterID { get; set; }
        public Guid? BankAccountID { get; set; }
        public DateTime? PayDate { get; set; }
        public bool IsWrongTransferDate { get; set; }
        public bool IsWrongCompany { get; set; }
        public decimal Amount { get; set; }
        public Guid paymentMethodID { get; set; }
        public MasterCenter BankTransferPayTo { get; set; }

    }
}
