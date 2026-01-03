using System;
namespace Report.Integration.Reports.AG
{
    public class RPT_Transfer_WaitLetter
    {
        /// <summary>
        /// รายงานแผนการออกจดหมายนัดโอนกรรมสิทธิ์
        /// </summary>

        //param
        public long? DateStart { get; set; }
        public string ProductID { get; set; }
        public string UserName { get; set; }
    }
}
