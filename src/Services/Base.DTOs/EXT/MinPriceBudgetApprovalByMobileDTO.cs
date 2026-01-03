using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.EXT
{
    public class MinPriceBudgetApprovalByMobileDTO 
    {
       
        /// <summary>
        /// ผู้อนุมัติ/ผู้รองขอ
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// ยกเลิกอนุมัติ comment
        /// </summary>
        public string RejectComment { get; set; }
        /// <summary>
        /// listของ minprice
        /// </summary>
        public List<MinpriceList> Minprice { get; set; }
    }

    public class MinpriceList
    {
        /// <summary>
        /// ID ของ MinPriceBudgetWorkflow
        /// </summary>
        public Guid MinPriceBudgetWorkflowID { get; set; }
        /// <summary>
        /// ลำดับที่
        /// </summary>
        public int Order { get; set; }
    }
}
