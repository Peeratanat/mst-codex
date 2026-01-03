using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class PostponeTransferDTO : BaseDTO
    {
        ///<summary>
        ///สัญญา
        ///</summary>
        public AgreementDropdownDTO Agreement { get; set; }

        ///<summary>
        ///โครงการ
        ///</summary>
        public ProjectDropdownDTO Project { get; set; }

        ///<summary>
        ///แปลง
        ///</summary>
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// วันที่นัดโอนตามใบบันทึกเลื่อนโอน
        /// </summary>
        public DateTime PostponeTransferDate { get; set; }

        /// <summary>
        /// วันที่นัดโอนตามใบบันทึกเลื่อนโอนต่ำสุดที่สามารถกำหนดได้
        /// </summary>
        public DateTime? MinPostponeTransferDate { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// LCM อนุมัติ
        /// </summary>
        public UserDTO LCMAprrove { get; set; }

        /// <summary>
        /// วันที่ LCM อนุมัติ
        /// </summary>
        public DateTime? LCMApproveDate { get; set; }

        /// <summary>
        /// LCM Reject
        /// </summary>
        public UserDTO LCMReject { get; set; }

        /// <summary>
        /// วันที่ LCM Reject
        /// </summary>
        public DateTime? LCMRejectDate { get; set; }

        /// <summary>
        ///สามารถบันทึกได้
        /// </summary>
        public bool? IsAdd { get; set; }
        /// <summary>
        ///สามารถแก้ไขได้
        /// </summary>
        public bool? IsUpdate { get; set; }
        /// <summary>
        ///สามารถพิมพ์ได้
        /// </summary>
        public bool? IsPrint { get; set; }
        /// <summary>
        ///สามารถอนุมัติได้
        /// </summary>
        public bool? IsApprove { get; set; }
        /// <summary>
        ///สามารถไม่อนุมัติได้
        /// </summary>
        public bool? IsCancel { get; set; }

        /// <summary>
        ///สถานะ
        /// </summary>
        public string StatusName { get; set; }

        public static PostponeTransferDTO CreateFromModel(PostponeTransfer model)
        {
            if (model != null)
            {
                var result = new PostponeTransferDTO()
                {
                    Id = model.ID,

                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    Project = ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                    PostponeTransferDate = model.PostponeTransferDate,
                    Remark = model.Remark,
                    LCMAprrove = UserDTO.CreateFromModel(model.LCMAprrove),
                    LCMApproveDate = model.LCMApproveDate,
                    LCMReject = UserDTO.CreateFromModel(model.LCMReject),
                    LCMRejectDate = model.LCMRejectDate,
                    MinPostponeTransferDate = model.Agreement.TransferOwnershipDate
                };

                //set button
                result.IsAdd = false;
                result.IsUpdate = false;
                result.IsPrint = false;
                result.IsApprove = false;
                result.IsCancel = false;
                
                if (model.Created == null)
                {
                    result.IsAdd = true;
                    result.StatusName = "รอตั้งเรื่องทำบันทึกเลื่อนโอน";
                }
                else
                {
                    if (result.LCMApproveDate.HasValue)
                    {
                        result.IsPrint = true;
                        result.StatusName = "ตั้งเรื่องเรียบร้อย กรุณาพิมพ์ใบบันทึก";
                    }
                    else
                    {
                        result.IsUpdate = true;
                        result.IsApprove = true;
                        result.IsCancel = true;
                        result.StatusName = "รอ LCM อนุมัติ";
                    }
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
