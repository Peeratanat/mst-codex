using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferBankTransferDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลโอน
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// จ่ายให้กับ
        /// </summary>
        public MST.MasterCenterDTO BankTransferPayTo { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongTransferDate { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// จ่ายให้กับนิติ
        /// </summary>
        public MST.LegalEntityDTO PayToLegalEntity { get; set; }

        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongTransfer { get; set; }

        /// <summary>
        /// บัญชี
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// สั่งจ่ายบริษัท
        /// </summary>
        public MST.CompanyDropdownDTO PayToCompany { get; set; }

        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongCompany { get; set; }

        /// <summary>
        /// สั่งจ่ายในนาม
        /// </summary>
        public string PayToName { get; set; }

        public static TransferBankTransferDTO CreateFromModel(TransferBankTransfer model)
        {
            if (model != null)
            {
                var AccountName = "";
                if (model.BankTransferPayTo?.Key == BankTransferPayToKeys.company) { AccountName = model.BankAccount?.DisplayName; }
                else if (model.BankTransferPayTo?.Key == BankTransferPayToKeys.corporation) { AccountName = model.PayToLegalEntity?.NameTH; }

                var PayToName = "";
                if (model.BankTransferPayTo?.Key == BankTransferPayToKeys.company) { PayToName = model.PayToCompany?.NameTH; }
                else if (model.BankTransferPayTo?.Key == BankTransferPayToKeys.corporation) { PayToName = model.PayToLegalEntity?.NameTH; }

                var result = new TransferBankTransferDTO()
                {
                    Id = model.ID,
                    Transfer = TransferDropdownDTO.CreateFromModel(model.Transfer),
                    BankTransferPayTo = MST.MasterCenterDTO.CreateFromModel(model.BankTransferPayTo),
                    BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    PayDate = model.PayDate,
                    IsWrongTransferDate = model.IsWrongTransferDate,
                    Amount = model.Amount,
                    PayToLegalEntity = MST.LegalEntityDTO.CreateFromModel(model.PayToLegalEntity),
                    IsWrongTransfer = model.IsWrongTransfer,
                    AccountName = AccountName,
                    PayToCompany = MST.CompanyDropdownDTO.CreateFromModel(model.PayToCompany),
                    IsWrongCompany = model.IsWrongCompany,
                    PayToName = PayToName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferBankTransfer model)
        {
            model = model ?? new TransferBankTransfer();

            model.TransferID = this.Transfer.Id ?? this.Transfer.Id.Value;
            model.BankTransferPayToMasterCenterID = this.BankTransferPayTo?.Id;
            model.BankAccountID = this.BankAccount?.Id;
            model.PayDate = this.PayDate;
            model.IsWrongTransferDate = this.IsWrongTransferDate;
            model.Amount = this.Amount;
            model.PayToLegalEntityID = this.PayToLegalEntity?.Id;
            model.IsWrongTransfer = this.IsWrongTransfer;
            model.PayToCompanyID = this.PayToCompany?.Id;
            model.IsWrongCompany = this.IsWrongCompany;
        }
    }
}
