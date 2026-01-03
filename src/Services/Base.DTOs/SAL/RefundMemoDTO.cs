using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.PRJ;
using Database.Models.MST;
using Database.Models.USR;
using System.Threading.Tasks;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// Memo คืนเงินลูกค้า
    /// </summary>
    public class RefundMemoDTO : BaseDTO
    {
        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public TransferListDTO Transfer { get; set; }

        /// <summary>
        /// ธนาคารนิติบุคคล
        /// </summary>
        public MST.BankDropdownDTO LegalEntityBank { get; set; }

        /// <summary>
        /// สาขาธนาคารนิติบุคคล
        /// </summary>
        public string LegalEntityBankBranch { get; set; }

        /// <summary>
        /// เลขที่บัญชีธนาคารนิติบุคคล
        /// </summary>
        public string LegalEntityBankAccount { get; set; }

        /// <summary>
        /// ธนาคารลูกค้า
        /// </summary>
        public MST.BankDropdownDTO CustomerBank { get; set; }

        /// <summary>
        /// สาขาธนาคารลูกค้า
        /// </summary>
        public string CustomerBankBranch { get; set; }

        /// <summary>
        /// เลขที่บัญชีธนาคารลูกค้า
        /// </summary>
        public string CustomerBankAccount { get; set; }

        /// <summary>
        /// ประเภท Memo
        /// </summary>
        public MST.MasterCenterDropdownDTO RefundMemoType { get; set; }

        /// <summary>
        /// วันที่นัดชำระ (คืนลูกค้า)
        /// </summary>
        public DateTime? RefundDueDate { get; set; }

        /// <summary>
        /// โอนเงินคืนลูกค้า
        /// </summary>
        public bool IsRefundCustomer { get; set; }

        /// <summary>
        /// โอนเงินคืนนิติ
        /// </summary>
        public bool IsRefundLegalEntity { get; set; }

        /// <summary>
        /// โอนเงินคืนลูกค้า (สั่งจ่ายนิติ) 
        /// </summary>
        public bool IsRefundCustomerByLegalEntity { get; set; }

        /// <summary>
        /// รายชื่อลูกค้า
        /// </summary>
        public List<RefundMemoCustomerDTO> RefundMemoCustomer { get; set; }

        public static RefundMemoDTO CreateFromModel(RefundMemo model, DatabaseContext DB)
        {
            if (model != null)
            {
                var refundCustomer = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && (o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerAndLegalEntity || o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByAP)).FirstOrDefault();
                var refundLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundLegalEntity).FirstOrDefault();
                var refundCustomerByLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByLegalEntity).FirstOrDefault();

                var listCustomer = DB.TransferOwners.Where(o => o.TransferID == model.TransferID).ToList();

                var listCustomerActive = DB.RefundMemoCustomers
                                        .Include(o => o.RefundMemo)
                                        .Include(o => o.TransferOwner)
                                        .Where(o => o.RefundMemoID == model.ID).ToList();

                var result = new RefundMemoDTO()
                {
                    Id = model.ID,
                    Transfer = TransferListDTO.CreateFromModel(model.Transfer, DB),
                    LegalEntityBank = BankDropdownDTO.CreateFromModel(model.LegalEntityBank),
                    LegalEntityBankBranch = model.LegalEntityBankBranch,
                    LegalEntityBankAccount = model.LegalEntityBankAccount,
                    CustomerBank = BankDropdownDTO.CreateFromModel(model.CustomerBank),
                    CustomerBankBranch = model.CustomerBankBranch,
                    CustomerBankAccount = model.CustomerBankAccount,
                    RefundMemoType = MasterCenterDropdownDTO.CreateFromModel(model.RefundMemoType),
                    RefundDueDate = model.RefundDueDate,
                    IsRefundCustomer = (refundCustomer != null),
                    IsRefundLegalEntity = (refundLegalEntity != null),
                    IsRefundCustomerByLegalEntity = (refundCustomerByLegalEntity != null)
                };

                result.RefundMemoCustomer = new List<RefundMemoCustomerDTO>();
                foreach (var item in listCustomer)
                {
                    var IsSelected = listCustomerActive.Where(o => o.TransferOwnerID == item.ID).Any() ? true : false;

                    var cust = RefundMemoCustomerDTO.CreateFromModel(item);
                    cust.RefundMemo = CreateFromForRefundMemoCustomerModel(model, DB);
                    cust.IsSelected = IsSelected;

                    result.RefundMemoCustomer.Add(cust);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static RefundMemoDTO CreateFromForRefundMemoCustomerModel(RefundMemo model, DatabaseContext DB)
        {
            if (model != null)
            {
                var refundCustomer = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && (o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerAndLegalEntity || o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByAP)).FirstOrDefault();
                var refundLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByLegalEntity).FirstOrDefault();
                var refundCustomerByLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.Transfer.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByLegalEntity).FirstOrDefault();

                var result = new RefundMemoDTO()
                {
                    Id = model.ID,
                    Transfer = TransferListDTO.CreateFromModel(model.Transfer, DB),
                    LegalEntityBank = BankDropdownDTO.CreateFromModel(model.LegalEntityBank) ?? new BankDropdownDTO(),
                    LegalEntityBankBranch = model.LegalEntityBankBranch ?? "",
                    LegalEntityBankAccount = model.LegalEntityBankAccount ?? "",
                    CustomerBank = BankDropdownDTO.CreateFromModel(model.CustomerBank) ?? new BankDropdownDTO(),
                    CustomerBankBranch = model.CustomerBankBranch ?? "",
                    CustomerBankAccount = model.CustomerBankAccount ?? "",
                    RefundMemoType = MasterCenterDropdownDTO.CreateFromModel(model.RefundMemoType) ?? new MasterCenterDropdownDTO(),
                    RefundDueDate = model.RefundDueDate,
                    IsRefundCustomer = (refundCustomer != null),
                    IsRefundLegalEntity = (refundLegalEntity != null),
                    IsRefundCustomerByLegalEntity = (refundCustomerByLegalEntity != null)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static RefundMemoDTO CreateFromDrafModel(Transfer model, DatabaseContext DB)
        {
            if (model != null)
            {
                var refundCustomer = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.ID && (o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerAndLegalEntity || o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByAP)).FirstOrDefault();
                var refundLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByLegalEntity).FirstOrDefault();
                var refundCustomerByLegalEntity = DB.RefundMemos.Include(o => o.RefundMemoType).Where(o => o.TransferID == model.ID && o.RefundMemoType.Key == RefundMemoTypeKey.RefundCustomerByLegalEntity).FirstOrDefault();

                var listCustomer = DB.TransferOwners.Where(o => o.TransferID == model.ID).ToList();

                var config = DB.AgreementConfigs
                    .Include(o => o.LegalEntity)
                    .Include(o => o.LegalEntity.Bank)
                    .Where(o => o.ProjectID == model.ProjectID).FirstOrDefault() ?? new AgreementConfig();

                var bank = config.LegalEntity?.Bank;
                var legalEntityBankAccount = config.LegalEntity?.BankAccountNo;

                var result = new RefundMemoDTO()
                {
                    Id = model.ID,
                    Transfer = TransferListDTO.CreateFromModel(model, DB),
                    LegalEntityBank = BankDropdownDTO.CreateFromModel(bank) ?? new BankDropdownDTO(),
                    LegalEntityBankBranch = "",
                    LegalEntityBankAccount = legalEntityBankAccount,
                    CustomerBank = new BankDropdownDTO(),
                    CustomerBankBranch = "",
                    CustomerBankAccount = "",
                    RefundMemoType = new MasterCenterDropdownDTO(),
                    RefundDueDate = null,
                    IsRefundCustomer = (refundCustomer != null),
                    IsRefundLegalEntity = (refundLegalEntity != null),
                    IsRefundCustomerByLegalEntity = (refundCustomerByLegalEntity != null)
                };

                result.RefundMemoCustomer = new List<RefundMemoCustomerDTO>();
                foreach (var item in listCustomer)
                {
                    var cust = RefundMemoCustomerDTO.CreateFromDrafModel(item);
                    result.RefundMemoCustomer.Add(cust);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        //public async Task ValidateAsync(DatabaseContext db)
        //{
        //    ValidateException ex = new ValidateException();
        //    if (this.Transfer == null)
        //    {
        //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
        //        string desc = this.GetType().GetProperty(nameof(RefundMemoDTO.Transfer)).GetCustomAttribute<DescriptionAttribute>().Description;
        //        var msg = errMsg.Message.Replace("[field]", desc);
        //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
        //    }

        //    if (ex.HasError)
        //    {
        //        throw ex;
        //    }
        //}

        public void ToModel(ref RefundMemo model)
        {
            model = model ?? new RefundMemo();

            model.TransferID = this.Transfer.Id.Value;
            model.LegalEntityBankID = this.LegalEntityBank?.Id;
            model.LegalEntityBankBranch = this.LegalEntityBankBranch;
            model.LegalEntityBankAccount = this.LegalEntityBankAccount;
            model.CustomerBankID = this.CustomerBank?.Id;
            model.CustomerBankBranch = this.CustomerBankBranch;
            model.CustomerBankAccount = this.CustomerBankAccount;
            model.RefundMemoTypeMasterCenterID = this.RefundMemoType?.Id;
            model.RefundDueDate = this.RefundDueDate;
        }

    }
}
