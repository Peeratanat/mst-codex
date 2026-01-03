using IO.Swagger.Client;
using IO.Swagger.Model;
using RestSharp;
using System;
using System.Collections.Generic;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICancelReturnSettingsApi
    {
        /// <summary>
        /// แก้ไขข้อมูล ตั้งค่าการยกเลิกคืนเงิน 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>CancelReturnSettingDTO</returns>
        CancelReturnSettingDTO EditCancelReturnSetting(Guid? id, CancelReturnSettingDTO input);
        /// <summary>
        /// ดึงข้อมูล ตั้งค่าการยกเลิกคืนเงิน 
        /// </summary>
        /// <returns>CancelReturnSettingDTO</returns>
        CancelReturnSettingDTO GetCancelReturnSetting();
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class CancelReturnSettingsApi : ICancelReturnSettingsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelReturnSettingsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public CancelReturnSettingsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelReturnSettingsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CancelReturnSettingsApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }

        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient { get; set; }

        /// <summary>
        /// แก้ไขข้อมูล ตั้งค่าการยกเลิกคืนเงิน 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>CancelReturnSettingDTO</returns>            
        public CancelReturnSettingDTO EditCancelReturnSetting(Guid? id, CancelReturnSettingDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditCancelReturnSetting");


            var path = "/api/CancelReturnSettings/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            postBody = ApiClient.Serialize(input); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling EditCancelReturnSetting: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditCancelReturnSetting: " + response.ErrorMessage, response.ErrorMessage);

            return (CancelReturnSettingDTO)ApiClient.Deserialize(response.Content, typeof(CancelReturnSettingDTO), response.Headers);
        }

        /// <summary>
        /// ดึงข้อมูล ตั้งค่าการยกเลิกคืนเงิน 
        /// </summary>
        /// <returns>CancelReturnSettingDTO</returns>            
        public CancelReturnSettingDTO GetCancelReturnSetting()
        {


            var path = "/api/CancelReturnSettings";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;


            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReturnSetting: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReturnSetting: " + response.ErrorMessage, response.ErrorMessage);

            return (CancelReturnSettingDTO)ApiClient.Deserialize(response.Content, typeof(CancelReturnSettingDTO), response.Headers);
        }

    }
}
