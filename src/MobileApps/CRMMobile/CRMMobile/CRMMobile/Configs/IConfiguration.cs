namespace CRMMobile.Configs
{
    public interface IConfiguration
    {
        string CustomerUrl { get; set; }
        string MasterCenterUrl { get; set; }
        string ProjectUrl { get; set; }
        string IndentifyUrl { get; set; }
    }

    public class Configuration : IConfiguration
    {
        public string CustomerUrl { get; set; }
        public string MasterCenterUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string IndentifyUrl { get; set; }
    }
}