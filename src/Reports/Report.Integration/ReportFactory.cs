using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Report.Integration
{
    public class ReportFactory
    {
        private string _reportUrl;
        private string _secretKey;
        ShowAs _showAs;
        IConfiguration Configuration;
        ReportFolder folder;
        string reportName = "";
        List<ReportParameter> parameters = null;

        /// <summary>
        /// List Parameter ที่ต้องการส่งไปให้กับ Report
        /// </summary>
        public List<ReportParameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// ชื่อ Folder ที่เก็บ File Crystal Report ที่ต้องการให้แสดงผล
        /// </summary>
        public ReportFolder Folder
        {
            get { return this.folder; }
        }

        /// <summary>
        /// ชื่อ Crystal report file ที่ต้องการให้แสดงผล
        /// </summary>
        public string ReportName
        {
            get { return this.reportName; }
        }

        //public ReportFactory(IConfiguration config)
        //{
        //    this.Configuration = config;
        //}

        /// <summary>
        /// Service สำหรับสร้าง Url แสดงผลรายงานในรูปแบบ PDf หรือ Excel
        /// </summary>
        /// <param name="config">ค่า config ที่เก็บ Url ของ Report Server</param>
        /// <param name="folder">ชื่อ Folder ที่เก็บ File Crystal Report ที่ต้องการให้แสดงผล</param>
        /// <param name="reportName">ชื่อ Crystal report file ที่ต้องการให้แสดงผล</param>
        /// <param name="showAs">Output ที่ต้องการให้ Web Report แสดงผล PDF,Excel</param>
        public ReportFactory(IConfiguration config, ReportFolder folder, string reportName, ShowAs showAs = ShowAs.PDF)
        {
            this.Configuration = config;
            this._secretKey = Environment.GetEnvironmentVariable("report_SecretKey");
            this._reportUrl = Environment.GetEnvironmentVariable("report_Url");
            this._showAs = showAs;
            this.folder = folder;
            this.reportName = reportName;
            parameters = new List<ReportParameter>();
        }

        public ReportFactory(ReportFolder folder, string reportName, ShowAs showAs = ShowAs.PDF)
        {
            //this._secretKey = config["Report:SecretKey"];
            //this._reportUrl = config["Report:Url"];
            this._secretKey = Environment.GetEnvironmentVariable("report_SecretKey");
            this._reportUrl = Environment.GetEnvironmentVariable("report_Url");
            this._showAs = showAs;
            this.folder = folder;
            this.reportName = reportName;
            parameters = new List<ReportParameter>();
        }

        /// <summary>
        /// เพิ่ม Parameter เพื่อใช้ส่งให้ Report
        /// </summary>
        /// <param name="parameterName">ชื่อ Parameter</param>
        /// <param name="value">ค่าของ Parameter</param>
        public ReportParameter AddParameter(string parameterName, object value)
        {
            if (parameters == null) parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            param.Name = parameterName;
            if (param.Value is List<string>)
            {
                string valueStr = "";
                foreach (string str in (List<string>)param.Value)
                {
                    if(valueStr!="") valueStr += "," ;
                    valueStr +="'"+ str + "'";
                }
                param.Value = valueStr;
            }
            else if (value is List<Guid?>)
            {
                string valueStr = "";
                foreach (Guid? guid in (List<Guid?>)value)
                {
                    if (guid != null)
                    {
                        if (valueStr != "") valueStr += "," ;
                        valueStr += guid.ToString();
                    }
                }
                param.Value = valueStr;
            }
            else param.Value = value;
            parameters.Add(param);
            return param;
        }

        /// <summary>
        /// ลบ Parameter ทั้งหมดที่เพิ่มเอาไว้
        /// </summary>
        public void ClearParameter()
        {
            if (parameters == null) parameters = new List<ReportParameter>();
            parameters.Clear();
        }

        /// <summary>
        /// สร้าง Url สำหรับแสดงผลรายงาน และ Encrypt Parameter ที่จะส่งให้รายงาน
        /// </summary>
        /// <returns></returns>
        public ReportResult CreateUrl()
        {
            ReportResult result = new ReportResult();
            //var source = $"{folder.ToString()}:{reportName}:{DateTime.Now.AddHours(1).Ticks}";
            var source = $"{DateTime.Now.AddHours(1).Ticks}";
            Console.WriteLine(source);
            //encrypt source to token using secretKey
            var token = Encrypt.EncryptString(source, _secretKey);
            Console.WriteLine(token);
            var deToken = Encrypt.DecryptString(token, _secretKey);
            Console.WriteLine(deToken);
            token = HttpUtility.UrlEncode(token);
            Console.WriteLine(token);
            var url = $"{this._reportUrl}?reportname={reportName}&folder={folder.ToString()}&token={token}&show={_showAs.ToString()}";

            string parameters = string.Empty;
            Dictionary<string, object> paramDict = new Dictionary<string, object>();

            foreach (ReportParameter p in this.parameters)
            {
                paramDict.Add(p.Name, p.Value);
            }

            parameters = JsonConvert.SerializeObject(paramDict);

            //encrypt parameter first before sending
            var encryptParameter = Encrypt.EncryptString(parameters, _secretKey);

            encryptParameter = HttpUtility.UrlEncode(encryptParameter);

            result.URL = url;
            result.Params = encryptParameter;
            return result;
        }

        //public ReportFactory(string reportUrl, string secretKey)
        //{
        //    this._reportUrl = reportUrl;
        //    this._secretKey = secretKey;
        //}

        public string CreateUrl<T>(T report, ShowAs downloadAs = ShowAs.PDF)
        {
            var type = typeof(T).Namespace.Split('.')[2];
            var module = typeof(T).Namespace.Split('.')[3];
            var reportID = typeof(T).Name;
            var source = $"{reportID}:{DateTime.Now.AddMinutes(5).Ticks}";
            Console.WriteLine(source);
            //encrypt source to token using secretKey
            var token = Encrypt.EncryptString(source, _secretKey);
            Console.WriteLine(token);
            var deToken = Encrypt.DecryptString(token, _secretKey);
            Console.WriteLine(deToken);
            token = HttpUtility.UrlEncode(token);
            Console.WriteLine(token);
            var url = $"{this._reportUrl}/{type}/{module}/{reportID}.aspx?token={token}&downloadAs={downloadAs.ToString()}";
            PropertyInfo[] props = typeof(T).GetProperties();

            string parameters = string.Empty;
            Dictionary<string, string> paramDict = new Dictionary<string, string>();

            foreach (var prop in props)
            {
                paramDict.Add(prop.Name, prop.GetValue(report).ToString());
                //parameters += $"{prop.Name}={prop.GetValue(report)}";
                //parameters += "&";
            }

            parameters = JsonConvert.SerializeObject(paramDict);

            //encrypt parameter first before sending
            var encryptParameter = Encrypt.EncryptString(parameters, _secretKey);

            encryptParameter = HttpUtility.UrlEncode(encryptParameter);

            url += $"&params={encryptParameter}";

            return url;
        }
    }

    public enum ShowAs
    {
        Excel, PDF
    }

    public enum ReportFolder
    {
        AG, AR, EX, FI, LC, MD, TR, AC, GP
    }
}
