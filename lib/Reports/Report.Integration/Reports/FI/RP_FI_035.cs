using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_035
    {
        /// <summary>
        /// รายงานเครื่องรูดบัตร
        /// </summary>

        //param
        public string Code { get; set; }
        public string CardMachineTypeKey { get; set; }
        public Guid? BankAccountID { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectStatusKey { get; set; }
        public string ReceiveBy { get; set; }
        public long? ReceiveDateFrom { get; set; }
        public long? ReceiveDateTo { get; set; }
        public string CardMachineStatusKey { get; set; }
    }
}
