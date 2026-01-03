 
namespace MST_Unitmeter.Params.Filters
{
    public class UnitMeterFilter : BaseFilter
    {
        public string ProjectIDs { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public Guid? ModelID { get; set; }
        public string UnitStatusKey { get; set; }
        public DateTime? TransferOwnerShipDateFrom { get; set; }
        public DateTime? TransferOwnerShipDateTo { get; set; }
        public string ElectricMeter { get; set; }
        public string WaterMeter { get; set; }
        public DateTime? CompletedDocumentDateFrom { get; set; }
        public DateTime? CompletedDocumentDateTo { get; set; }
        public string WaterMeterStatusKey { get; set; }
        public string ElectricMeterStatusKey { get; set; }
    }
}
