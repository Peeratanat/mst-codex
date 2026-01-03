using Base.DTOs.Common;
using Common.Extensions;
using Common.Extensions.HeaderUril;
using Common.Helper;
using Common.Helper.Logging;
using Common.Services.Interfaces; 
using External.Kafka.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Services
{
    public class LogService : ILogService
    {
        private readonly IHeadersUtils _headersUtil;
        private readonly ILogger _logger;
        private readonly IKakfaProducer _kakfaProducer;
        private static LogHelper loggertxt;
        External.Kafka.Models.ApplicationLog _applicationLog;
        private bool cmdlog = true;

        public LogService(ILogger<LogService> logger, IHeadersUtils headersUtil, IKakfaProducer kakfaProducer)
        {
            _headersUtil = headersUtil;
            _logger = logger;
            _kakfaProducer = kakfaProducer;
        }

        public async Task WriteLog(LogModel logModel)
        {
            _applicationLog = new External.Kafka.Models.ApplicationLog();
            #region Set Request  
            Dictionary<string, string> AuthHeader = null;
            if (_headersUtil.EmpCode == null)
            {
                AuthHeader = new Dictionary<string, string>() {
                { "EmpCode", _headersUtil.EmpCode??""}
                };
            }



            #region check sensitive body  
            //sensitive pdpa
            string[] sensitive = {
                    "firstname"
                    ,"lastname"
                    ,"fullname"
                    ,"nickname"
                    ,"tel"
                    ,"mobile"
                    ,"email"
                    ,"password"
                    ,"passport"
                    ,"bank"
                    ,"cardid"
                    ,"creditcard"
                    ,"blood"
                    ,"health"
                    ,"sex"
                    ,"biometric"
                    ,"iden"
                    ,"personal"
                    ,"gender"
                    ,"dateofbirth"
            };

            object bodyresponse = null;
            JObject bodyjsonObj;
            var body = await _headersUtil.GetBodyRequest();
            if (!string.IsNullOrEmpty(body))
            {
                try
                {
                    //Parse to JObject พัง
                    //ยังไม่ทราบวิธีแก้ปัญหา เลยใช้ try catch ดักไว้ก่อน 
                    bodyjsonObj = JObject.Parse(body);
                }
                catch (Exception)
                {
                    bodyjsonObj = new JObject();
                }

                foreach (var item in sensitive)
                {
                    MaskKeyValue(bodyjsonObj, item, "**********");
                }
                bodyresponse = JsonConvert.DeserializeObject<object>(bodyjsonObj.ToString());
            }
            #endregion

            var dics = new Dictionary<string, object>() {
                    { "Url", _headersUtil.GeturlPath()},
                    { "Method", _headersUtil.GetMethod() },
                    { "Header" , AuthHeader },
                    { "Body", bodyresponse },
                    { "QueryString", _headersUtil.GetQueryString() }
                };
            //filter dics not null
            var filtered = dics.Where(p => p.Value != null)
                .ToDictionary(p => p.Key, p => p.Value);

            logModel.RequestObject = filtered;
            #endregion

            PropertyInfo pi = logModel.ResponseData.GetType().GetProperty("Data");
            string strData = JsonConvert.SerializeObject(pi.GetValue(logModel.ResponseData));

            //convert data<T> to string
            ResponseModel respdata = new ResponseModel<string>()
            {
                Message = logModel.ResponseData.Message,
                Data = strData,
                RequestID = logModel.RequestID,
            };
            logModel.ResponseData = respdata;
            logModel.ActivityLog = logModel.GetActivityLog();


            TraceLog traceLog = new TraceLog
            {
                RequestID = logModel.RequestID,
                StartTime = logModel.StartTime,
                RequestObject = logModel.RequestObject,
                ResponseData = logModel.ResponseData,
                Endpoint = logModel.EndPoint,
                LogAlive = logModel.LogAlive,
                ExceptionMessage = logModel.ExceptionMessage,
                ActivityLog = logModel.ActivityLog
            };

            #region ApplicationLog 
            if (logModel.StatusCode == HttpStatusCode.InternalServerError)
            {
                _applicationLog.LogLevel = "ERROR";
            }
            else
            {
                _applicationLog.LogLevel = "INFO";
            }

            //change name for project LogSystem
            _applicationLog.LogSystem = EnvironmentHelper.GetEnvironment("SystemName");
            _applicationLog.SetLogMessage(traceLog);
            //_applicationLog.LogMessage = traceLog;
            _applicationLog.HttpMethod = _headersUtil.GetMethod();
            _applicationLog.Module = logModel.Command;
            _applicationLog.SubModule = logModel.SubModule;
            _applicationLog.HttpStatusCode = ((int)logModel.StatusCode).ToString();
            _applicationLog.TimeStamp = logModel.StartTime;
            _applicationLog.User = _headersUtil.GetUserEmcode();

            //await _kakfaProducer.WriteApplicationLog(_applicationLog);  
            #endregion


            if (logModel.StatusCode == HttpStatusCode.InternalServerError)
            {
                logModel.ExceptionMessage = new LogExceptionModel(new Exception());
            }



            #region LogType Writelog   
            //command log 
            if (cmdlog)
            {
                _logger.LogInformation(_applicationLog.Serialize());
                var ElapsedLog = new 
                { 
                    RequestObject = logModel.RequestObject,   
                    ActivityLog = logModel.ActivityLog
                };
                //_logger.LogInformation($"Elapsed : {JsonConvert.SerializeObject(ElapsedLog)}");
            }
            else
            {
                loggertxt = new LogHelper();
                loggertxt.WriteTraceLog(_applicationLog.Serialize());
            }
            #endregion  
        }

        static void MaskKeyValue(JObject jsonObject, string key, string mask)
        {
            foreach (JProperty property in jsonObject.Properties())
            {

                if (property.Name.ToLower() == key.ToLower())
                {
                    property.Value = new JValue(mask);
                    return;
                }

                if (property.Value.Type == JTokenType.Object)
                {
                    MaskKeyValue((JObject)property.Value, key, mask);
                }
            }
        }
    }
}
