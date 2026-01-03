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
    public interface IMasterCenterGroupsApi
    {
        /// <summary>
        /// เพิ่ม กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MasterCenterGroupDTO</returns>
        MasterCenterGroupDTO CreateMasterCenterGroup(MasterCenterGroupDTO input);
        /// <summary>
        /// ลบ กลุ่มข้อมูลกลุ่มพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void DeleteMasterCenterGroup(string key);
        /// <summary>
        /// แก้ไขกลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns>MasterCenterGroupDTO</returns>
        MasterCenterGroupDTO EditMasterCenterGroup(string key, MasterCenterGroupDTO input);
        /// <summary>
        /// ข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>MasterCenterGroupDTO</returns>
        MasterCenterGroupDTO GetMasterCenterGroup(string key);
        /// <summary>
        /// ลิสข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;MasterCenterGroupDTO&gt;</returns>
        List<MasterCenterGroupDTO> GetMasterCenterGroupList(string key, string name, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class MasterCenterGroupsApi : IMasterCenterGroupsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterCenterGroupsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public MasterCenterGroupsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterCenterGroupsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MasterCenterGroupsApi(String basePath)
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
        /// เพิ่ม กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>MasterCenterGroupDTO</returns>            
        public MasterCenterGroupDTO CreateMasterCenterGroup(MasterCenterGroupDTO input)
        {


            var path = "/api/MasterCenterGroups";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            postBody = ApiClient.Serialize(input); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling CreateMasterCenterGroup: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateMasterCenterGroup: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterGroupDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterGroupDTO), response.Headers);
        }

        /// <summary>
        /// ลบ กลุ่มข้อมูลกลุ่มพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param> 
        /// <returns></returns>            
        public void DeleteMasterCenterGroup(string key)
        {

            // verify the required parameter 'key' is set
            if (key == null) throw new ApiException(400, "Missing required parameter 'key' when calling DeleteMasterCenterGroup");


            var path = "/api/MasterCenterGroups/{key}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "key" + "}", ApiClient.ParameterToString(key));

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;


            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteMasterCenterGroup: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteMasterCenterGroup: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขกลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param> 
        /// <param name="input"></param> 
        /// <returns>MasterCenterGroupDTO</returns>            
        public MasterCenterGroupDTO EditMasterCenterGroup(string key, MasterCenterGroupDTO input)
        {

            // verify the required parameter 'key' is set
            if (key == null) throw new ApiException(400, "Missing required parameter 'key' when calling EditMasterCenterGroup");


            var path = "/api/MasterCenterGroups/{key}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "key" + "}", ApiClient.ParameterToString(key));

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
                throw new ApiException((int)response.StatusCode, "Error calling EditMasterCenterGroup: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditMasterCenterGroup: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterGroupDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterGroupDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param> 
        /// <returns>MasterCenterGroupDTO</returns>            
        public MasterCenterGroupDTO GetMasterCenterGroup(string key)
        {

            // verify the required parameter 'key' is set
            if (key == null) throw new ApiException(400, "Missing required parameter 'key' when calling GetMasterCenterGroup");


            var path = "/api/MasterCenterGroups/{key}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "key" + "}", ApiClient.ParameterToString(key));

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
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterGroup: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterGroup: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterGroupDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterGroupDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="key"></param> 
        /// <param name="name"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;MasterCenterGroupDTO&gt;</returns>            
        public List<MasterCenterGroupDTO> GetMasterCenterGroupList(string key, string name, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/MasterCenterGroups";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (key != null) queryParams.Add("key", ApiClient.ParameterToString(key)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (updatedBy != null) queryParams.Add("updatedBy", ApiClient.ParameterToString(updatedBy)); // query parameter
            if (updatedFrom != null) queryParams.Add("updatedFrom", ApiClient.ParameterToString(updatedFrom)); // query parameter
            if (updatedTo != null) queryParams.Add("updatedTo", ApiClient.ParameterToString(updatedTo)); // query parameter
            if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
            if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
            if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
            if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterGroupList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterGroupList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<MasterCenterGroupDTO>)ApiClient.Deserialize(response.Content, typeof(List<MasterCenterGroupDTO>), response.Headers);
        }

    }
}
