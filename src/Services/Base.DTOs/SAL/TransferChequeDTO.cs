using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferChequeDTO : BaseDTO
    {/// <summary>
     /// ข้อมูลโอน
     /// </summary>
        public TransferDropdownDTO Transfer { get; set; }

        /// <summary>
        /// จ่ายให้กับ
        /// </summary>
        public MST.MasterCenterDTO ChequePayTo { get; set; }

        /// <summary>
        /// สั่งจ่ายบริษัท
        /// </summary>
        public MST.CompanyDropdownDTO PayToCompany { get; set; }

        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        public string ChequeNo { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// วันที่เช็ค
        /// </summary>
        public DateTime PayDate { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongCompany { get; set; }

        /// <summary>
        /// จ่ายให้กับนิติ
        /// </summary>
        public MST.LegalEntityDTO PayToLegalEntity { get; set; }

        /// <summary>
        /// สั่งจ่ายในนาม
        /// </summary>
        public string PayToName { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        public string BankAccountName { get; set; }

        public static TransferChequeDTO CreateFromModel(TransferCheque model)
        {
            if (model != null)
            {
                var PayToName = "";
                if (model.ChequePayTo?.Key == ChequePayToKeys.company) { PayToName = model.PayToCompany?.NameTH; }
                else if (model.ChequePayTo?.Key == ChequePayToKeys.corporation) { PayToName = model.PayToLegalEntity?.NameTH; }

                var result = new TransferChequeDTO()
                {
                    Id = model.ID,
                    Transfer = TransferDropdownDTO.CreateFromModel(model.Transfer),
                    ChequePayTo = MST.MasterCenterDTO.CreateFromModel(model.ChequePayTo),
                    PayToCompany = MST.CompanyDropdownDTO.CreateFromModel(model.PayToCompany),
                    ChequeNo = model.ChequeNo,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    PayDate = model.PayDate ?? model.PayDate.Value,
                    IsWrongCompany = model.IsWrongCompany,
                    Amount = model.Amount,
                    PayToLegalEntity = MST.LegalEntityDTO.CreateFromModel(model.PayToLegalEntity),
                    PayToName = PayToName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferCheque model)
        {
            model = model ?? new TransferCheque();

            model.TransferID = this.Transfer.Id ?? this.Transfer.Id.Value;
            model.ChequePayToMasterCenterID = this.ChequePayTo?.Id;
            model.PayToCompanyID = this.PayToCompany?.Id;
            model.ChequeNo = this.ChequeNo;
            model.BankID = this.Bank?.Id;
            model.PayDate = this.PayDate;
            model.IsWrongCompany = this.IsWrongCompany;
            model.Amount = this.Amount;
            model.PayToLegalEntityID = this.PayToLegalEntity?.Id;
        }
    }
}
