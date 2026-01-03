namespace Base.DTOs.FIN
{
    public class UnknownPaymentAutoCCSortByParam
    {
        public UnknownPaymentAutoCCSortBy? SortBy { get; set; }
        public bool Ascending { get; set; } = true;
    }

    public enum UnknownPaymentAutoCCSortBy
    {
        UnitNo
    }
}