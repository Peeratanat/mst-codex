namespace MST_General.Params.Filters
{
    public class SubDistrictFilter : BaseFilter
    {
        public Guid? DistrictID { get; set; }
        public string LandOffice { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string PostalCode { get; set; }
    }
}
