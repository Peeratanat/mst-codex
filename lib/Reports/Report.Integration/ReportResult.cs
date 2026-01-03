namespace Report.Integration
{
    public class ReportResult
    {
        public string URL { get; set; }
        public string Params { get; set; }
        public string URLMinio { get; set; }
    }

    public class ReportParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
