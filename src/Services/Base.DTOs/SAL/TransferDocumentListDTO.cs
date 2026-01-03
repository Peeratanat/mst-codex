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
    public class TransferDocumentListDTO : BaseDTO
    {

        ///<summary>
        ///รายการ
        ///</summary>
        public string Description { get; set; }
        ///<summary>
        ///ผู้ทำรายการ
        ///</summary>
        public USR.UserDTO ActionUser { get; set; }
        ///<summary>
        ///วัน-เวลา
        ///</summary>
        public DateTime? ActionDate { get; set; }
        ///<summary>
        ///หมายเหตุ
        ///</summary>
        public string Remark { get; set; }

        public static TransferDocumentListDTO CreateFromModel(TransferDocument model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TransferDocumentListDTO()
                {
                    Id = model.ID,
                    Remark = model.Remark
                };

                result.Description = "";
                result.ActionUser = new UserDTO();

                if (model.IsAssignAuthority.HasValue && model.IsAssignAuthority.Value)
                {
                    result.Description = "มอบอำนาจ";
                    result.ActionUser = UserDTO.CreateFromModel(model.AssignAuthorityUser);
                    result.ActionDate = model.AssignAuthorityDate;
                }
                else if (model.IsAssignAuthority.HasValue && !model.IsAssignAuthority.Value)
                {
                    result.Description = "ไม่มอบอำนาจ";
                    result.ActionUser = UserDTO.CreateFromModel(model.AssignAuthorityUser);
                    result.ActionDate = model.AssignAuthorityDate;
                }

                //if (result.Description != "")
                //{
                //    result.Description += ",";
                //}

                else if (model.IsReceiveDocument.HasValue && model.IsReceiveDocument.Value)
                {
                    result.Description += "ลูกค้าได้รับเอกสารแล้ว";
                    result.ActionUser = UserDTO.CreateFromModel(model.ReceiveDocumentUser);
                    result.ActionDate = model.ReceiveDocumentDate;
                }
                else if (model.IsReceiveDocument.HasValue && !model.IsReceiveDocument.Value)
                {
                    result.Description += "ลูกค้ายังไม่ได้รับเอกสาร";
                    result.ActionUser = UserDTO.CreateFromModel(model.ReceiveDocumentUser);
                    result.ActionDate = model.ReceiveDocumentDate;
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
