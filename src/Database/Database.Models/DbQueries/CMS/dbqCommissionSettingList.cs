using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class dbqCommissionSettingList : BaseDbQueries
    {
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public DateTime? MinGeneralSetting { get; set; }
        public DateTime? MaxGeneralSetting { get; set; }
        public DateTime? MinRateSettingFixSaleModel { get; set; }
        public DateTime? MaxRateSettingFixSaleModel { get; set; }
        public DateTime? MinRateSettingFixTransferModel { get; set; }
        public DateTime? MaxRateSettingFixTransferModel { get; set; }
        public DateTime? MinRateSettingFixSale { get; set; }
        public DateTime? MaxRateSettingFixSale { get; set; }
        public DateTime? MinRateSettingFixTransfer { get; set; }
        public DateTime? MaxRateSettingFixTransfer { get; set; }
        public DateTime? MinRateSettingSale { get; set; }
        public DateTime? MaxRateSettingSale { get; set; }
        public DateTime? MinRateSettingTransfer { get; set; }
        public DateTime? MaxRateSettingTransfer { get; set; }
        public DateTime? MinRateSettingAgent { get; set; }
        public DateTime? MaxRateSettingAgent { get; set; }
    }
}
