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
    /// รายชื่อลูกค้าใน Memo คืนเงินลูกค้า
    /// </summary>
    public class RefundMemoCustomerDTO : BaseDTO
    {
        /// <summary>
        /// เลือกหรือไม่
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Memo คืนเงินลูกค้า
        /// </summary>
        public RefundMemoDTO RefundMemo { get; set; }

        /// <summary>
        /// ผู้โอนกรรมสิทธิ์
        /// </summary>
        public TransferOwnerDropdownDTO TransferOwner { get; set; }

        public static RefundMemoCustomerDTO CreateFromModel(TransferOwner model)
        {
            if (model != null)
            {
                var result = new RefundMemoCustomerDTO()
                {
                    Id = new Guid(),
                    RefundMemo = new RefundMemoDTO(),
                    TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(model),
                    IsSelected = false
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static RefundMemoCustomerDTO CreateFromDrafModel(TransferOwner model)
        {
            if (model != null)
            {
                var result = new RefundMemoCustomerDTO()
                {
                    Id = new Guid(),
                    RefundMemo = new RefundMemoDTO(),
                    TransferOwner = TransferOwnerDropdownDTO.CreateFromModel(model),
                    IsSelected = false
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref RefundMemoCustomer model)
        {
            model = model ?? new RefundMemoCustomer();

            model.RefundMemoID = this.RefundMemo.Id.Value;
            model.TransferOwnerID = this.TransferOwner.Id.Value;
        }

    }
}
