using System;
namespace MST_Finacc.Params.Filters
{
    public class EDCFilter : BaseFilter
    {
        public string Code { get; set; }
        public string CardMachineTypeKey { get; set; }
        public Guid? BankAccountID { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectStatusKey { get; set; }
        public string ReceiveBy { get; set; }
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }
        public string CardMachineStatusKey { get; set; }
        public Guid? BankID { get; set; }
        public bool? IsLast5Unit { get; set; }
        public string TelNo { get; set; }
    }
}
