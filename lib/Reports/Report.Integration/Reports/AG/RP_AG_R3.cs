using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_R3
    {
        /// <summary>
        /// รายงานของแถมในสัญญา+โอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
