using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Base.DTOs.FIN;
using Database.Models;
using Database.Models.FIN;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.FIN.BillPaymentDetailDTO;

namespace Base.DTOs.FIN
{
    public class BookingForBillPaymentDTO : BaseDTO
    {

        /// <summary>
        /// Account
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        public List<SplitForBillPaymentDTO> SplitForBillPayment { get; set; }

        /// <summary>
        /// วันที่ลูกค้าชำระเงิน 
        /// </summary>
        [Description("วันที่ลูกค้าชำระเงิน")]
        public DateTime ReceiveDate { get; set; }


        /// <summary>
        /// ชื่อบริษัท
        /// </summary>
        [Description("ชื่อบริษัท")]
        public MST.CompanyDropdownDTO Company { get; set; }

        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        public bool? IsWrongAccount { get; set; }
        public static BookingForBillPaymentDTO CreateFromSplitModel(BillPaymentDetail model, DatabaseContext DB)
        {
            if (model != null)
            {
                BookingForBillPaymentDTO result = new BookingForBillPaymentDTO()
                {
                    ReceiveDate = model.ReceiveDate,
                    Account = model.BillPayment.BankAccount?.DisplayName,
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.BillPayment.BankAccount?.Company),
                    Amount = model.PayAmount,
                    Id = model.ID,
                    SplitForBillPayment = new List<SplitForBillPaymentDTO>(),
                    BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model.BillPayment.BankAccount),
                    IsWrongAccount = model.IsWrongAccount
                };
                string DisplayNameBank = model.BillPayment.BankAccount?.Bank?.Alias + " " + model.BillPayment.BankAccount?.BankAccountType?.Name + " " + model.BillPayment.BankAccount?.BankAccountNo;
                result.Account = !string.IsNullOrEmpty(model.BillPayment.BankAccount?.DisplayName) ? model.BillPayment.BankAccount?.DisplayName : DisplayNameBank;

                IQueryable<QueryResult> Query = from o in DB.PaymentMethods.Where(x => x.BillPaymentDetailID == result.Id && x.Payment.IsCancel == false)
                        .Include(x => x.Payment)
                            .ThenInclude(x => x.Booking)
                                .ThenInclude(x => x.Unit)
                                    .ThenInclude(x => x.Project)
                                        .ThenInclude(x => x.Company)
                           .Include(x => x.PaymentMethodType)
                           .Where(x => x.PaymentMethodType.Key == PaymentMethodKeys.BillPayment)

                                                join AgreementData in DB.Agreements on o.Payment.BookingID equals AgreementData.BookingID into AgreementGroup
                                                from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                select new QueryResult
                                                {
                                                    PaymentBillPayment = o,
                                                    Agreement = AgreementModel ?? new Agreement()
                                                };
                var QueryTolist = Query.ToList();
                if (QueryTolist.Count > 0)
                {
                    result.SplitForBillPayment = QueryTolist.Select(o => SplitForBillPaymentDTO.CreateFromSplitModel(o,DB)).ToList();
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
