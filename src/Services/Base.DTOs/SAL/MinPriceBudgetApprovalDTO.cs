using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class MinPriceBudgetApprovalDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MinPriceBudgetWorkflow
        /// </summary>
        public Guid? MinPriceBudgetWorkflowID { get; set; }
        /// <summary>
        /// ลำดับที่
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// ผู้อนุมัติ/ผู้รองขอ
        /// </summary>
        public USR.UserListDTO User { get; set; }
        /// <summary>
        /// ตำแหน่งที่ต้องอนุมัติ
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// วันที่อนุมัติ/วันที่ร้องขอ
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// การอนุมัติ (null = รอนุมัติ/ false = ไม่อนุมัติ/ true = อนุมัติ)
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// เป็นผู้ร้องขอหรือไม่ (false = ผู้อนุมัติ/ true = ผู้ร้องขอ)
        /// </summary>
        public bool? IsRequest { get; set; }

        public static MinPriceBudgetApprovalDTO CreateFromModel(MinPriceBudgetApproval model)
        {
            if (model != null)
            {
                var result = new MinPriceBudgetApprovalDTO
                {
                    Id = model.ID,
                    MinPriceBudgetWorkflowID = model.MinPriceBudgetWorkflowID,
                    Order = model.Order,
                    RoleName = model.Role!=null? model.Role.Code:null,
                    User = USR.UserListDTO.CreateFromModel(model.User),
                    ApprovedDate = model.ApprovedTime,
                    IsApproved = model.IsApproved,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    IsRequest = false
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
