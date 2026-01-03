namespace MST_General.Params.Filters
{
    public class DistrictFilter : BaseFilter
    {
        public Guid? ProvinceID { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string PostalCode { get; set; }
    }
}
