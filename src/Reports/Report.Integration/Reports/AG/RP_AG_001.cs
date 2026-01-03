using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_001
    {
        /// <summary>
        /// รายละเอียดการค้างชำระเงินดาวน์ตามวันครบกำหนดชำระ
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateStart2 { get; set; }
        public string StatusPeriod { get; set; }
        public string UserName { get; set; }
        public string CustomerStatus { get; set; }
    }
}
