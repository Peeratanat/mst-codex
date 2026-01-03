using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferCashDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลโอน
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// จ่ายให้กับ
        /// </summary>
        public MST.MasterCenterDTO CashPayTo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// จ่ายให้กับนิติ
        /// </summary>
        public MST.LegalEntityDTO PayToLegalEntity { get; set; }

        public static TransferCashDTO CreateFromModel(TransferCash model)
        {
            if (model != null)
            {
                var result = new TransferCashDTO()
                {
                    Id = model.ID,
                    Transfer = TransferDropdownDTO.CreateFromModel(model.Transfer),
                    CashPayTo = MST.MasterCenterDTO.CreateFromModel(model.CashPayTo),
                    Amount = model.Amount,
                    PayToLegalEntity = MST.LegalEntityDTO.CreateFromModel(model.PayToLegalEntity),
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferCash model)
        {
            model = model ?? new TransferCash();

            model.TransferID = this.Transfer.Id ?? this.Transfer.Id.Value;
            model.CashPayToMasterCenterID = this.CashPayTo?.Id;
            model.Amount = this.Amount;
            model.PayToLegalEntityID = this.PayToLegalEntity?.Id;
        }
    }
}
