
using Base.DTOs.SystemMessage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Base.DTOs.Common
{
    public class ResponseModel<T> : ResponseModel
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)] 
        public virtual T? Data { get; set; }
    }

    public abstract class ResponseModel
    {
        [JsonProperty("requestID", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string RequestID { get; set; }
        [JsonProperty("message")]
        public virtual string Message { get; set; } = CommonMessage.Success;
    }

    public class api_response
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public object Data { get; set; } = null!;
    }

    public class token_response
    {
        public Guid? CID { get; set; }
        public Guid? LcID { get; set; }
        public string ProjectCode { get; set; }
        public string gtm_key { get; set; }
        public Guid? CustomerID { get; set; }
        public bool IsReview  { get; set; }
        public List<file_path_response> file_path_response { get; set; }
        //public file_path_response file_path_responses_StartWebFilesID { get; set; }
        //public file_path_response file_path_responses_EndWebFilesID { get; set; }
        //public file_path_response file_path_responses_StartMobileFilesID { get; set; }
        //public file_path_response file_path_responses_EndMobileFilesID { get; set; }

    }

    public class file_path_response
    {
        public string Type { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public bool IsTemp { get; set; }
    }

}
