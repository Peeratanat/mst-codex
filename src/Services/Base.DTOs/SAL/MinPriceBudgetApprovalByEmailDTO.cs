using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class MinPriceBudgetApprovalByEmailDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MinPriceBudgetWorkflow
        /// </summary>
        public Guid MinPriceBudgetWorkflowID { get; set; }
        /// <summary>
        /// ลำดับที่
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// ผู้อนุมัติ/ผู้รองขอ
        /// </summary>
        public Guid User { get; set; }
        /// <summary>
        /// ยกเลิกอนุมัติ comment
        /// </summary>
        public string RejectComment { get; set; }
    }
}
