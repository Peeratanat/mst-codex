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
using Base.DTOs.USR;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ข้อมูล Control Doc (การส่งมอบเอกสารโอนกรรมสิทธิ์)
    /// </summary>
    public class TransferDocumentDTO : BaseDTO
    {
        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public TransferListDTO Transfer { get; set; }

        ///<summary>
        ///มอบอำนาจหรือไม่?
        ///</summary>
        public bool? IsAssignAuthority { get; set; }
        ///<summary>
        ///ผู้แก้ไขมอบอำนาจ
        ///</summary>
        public USR.UserDTO AssignAuthorityUser { get; set; }
        ///<summary>
        ///วันที่แก้ไขมอบอำนาจ
        ///</summary>
        public DateTime? AssignAuthorityDate { get; set; }

        ///<summary>
        ///ลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้วหรือไม่?
        ///</summary>
        public bool? IsReceiveDocument { get; set; }
        ///<summary>
        ///ผู้แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว
        ///</summary>
        public USR.UserDTO ReceiveDocumentUser { get; set; }
        ///<summary>
        ///วันที่แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว
        ///</summary>
        public DateTime? ReceiveDocumentDate { get; set; }

        ///<summary>
        ///เหตุผล
        ///</summary>
        public string Remark { get; set; }

        public static TransferDocumentDTO CreateFromModel(TransferDocument model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TransferDocumentDTO()
                {
                    Id = model.ID,
                    Transfer = TransferListDTO.CreateFromModel(model.Transfer, DB),
                    IsAssignAuthority = model.IsAssignAuthority,
                    AssignAuthorityUser = UserDTO.CreateFromModel(model.AssignAuthorityUser),
                    AssignAuthorityDate = model.AssignAuthorityDate,
                    IsReceiveDocument = model.IsReceiveDocument,
                    ReceiveDocumentUser = UserDTO.CreateFromModel(model.ReceiveDocumentUser),
                    ReceiveDocumentDate = model.ReceiveDocumentDate,
                    Remark = model.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.Transfer == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferDocumentDTO.Transfer)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref TransferDocument model)
        {
            model = model ?? new TransferDocument();

            model.TransferID = this.Transfer.Id.Value;
            model.IsAssignAuthority = this.IsAssignAuthority;
            model.AssignAuthorityUserID = this.AssignAuthorityUser.Id;
            model.AssignAuthorityDate = this.AssignAuthorityDate;
            model.IsReceiveDocument = this.IsReceiveDocument;
            model.ReceiveDocumentUserID = this.ReceiveDocumentUser.Id;
            model.ReceiveDocumentDate = this.ReceiveDocumentDate;
            model.Remark = this.Remark;
        }
    }
}
