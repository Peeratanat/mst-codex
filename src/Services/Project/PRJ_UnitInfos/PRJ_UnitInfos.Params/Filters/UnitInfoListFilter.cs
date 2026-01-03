using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_UnitInfos.Params.Filters
{
    public class UnitInfoListFilter
    {
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string FullName { get; set; }
        public string BookingNo { get; set; }
        public string AgreementNo { get; set; }
        public Guid? ProjectID { get; set; }
        public string UnitStatusKeys { get; set; }
        public Guid? ContactID { get; set; }
        public string TransferPromotionNo { get; set; }
        public Guid? BankID { get; set; }
        public string TransferNo { get; set; }
        public Guid? TowerID { get; set; }
    }

    public class UnitInfoListFilterByContact
    {
        public Guid? ContactID { get; set; }
    }
}
