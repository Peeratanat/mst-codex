using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace MST_Promotion.UnitTests
{
    public class BaseServiceTest
    {

        IConfiguration Configuration;
        public BaseServiceTest()
        {

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "promotion");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
            Environment.SetEnvironmentVariable("report_SecretKey", "nIcHeoYiMNZiJMYz");
            Environment.SetEnvironmentVariable("report_Url", "http://192.168.4.160/rptcrmrevo/printform.aspx");


            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

    }
}
