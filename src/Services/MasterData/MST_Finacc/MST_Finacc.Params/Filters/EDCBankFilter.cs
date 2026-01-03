using System;
namespace MST_Finacc.Params.Filters
{
    public class EDCBankFilter : BaseFilter
    {
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
    }
}
