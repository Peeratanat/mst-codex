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
    public interface IMasterCentersApi
    {
        /// <summary>
        /// เพิ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MasterCenterDTO</returns>
        MasterCenterDTO CreateMasterCenter(MasterCenterDTO input);
        /// <summary>
        /// ลบข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteMasterCenter(Guid? id);
        /// <summary>
        /// แก้ไข ข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>MasterCenterDTO</returns>
        MasterCenterDTO EditMasterCenter(Guid? id, MasterCenterDTO input);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="key"></param>
        /// <returns>MasterCenterDropdownDTO</returns>
        MasterCenterDropdownDTO GetFindMasterCenterDropdownItem(string masterCenterGroupKey, string key);
        /// <summary>
        /// ข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MasterCenterDTO</returns>
        MasterCenterDTO GetMasterCenter(Guid? id);
        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown 
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="name"></param>
        /// <returns>List&lt;MasterCenterDropdownDTO&gt;</returns>
        List<MasterCenterDropdownDTO> GetMasterCenterDropdownList(string masterCenterGroupKey, string name);
        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="order"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="isActive"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;MasterCenterDTO&gt;</returns>
        List<MasterCenterDTO> GetMasterCenterList(string masterCenterGroupKey, int? order, string name, string key, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class MasterCentersApi : IMasterCentersApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterCentersApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public MasterCentersApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterCentersApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MasterCentersApi(String basePath)
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
        /// เพิ่มข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>MasterCenterDTO</returns>            
        public MasterCenterDTO CreateMasterCenter(MasterCenterDTO input)
        {


            var path = "/api/MasterCenters";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateMasterCenter: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateMasterCenter: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteMasterCenter(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteMasterCenter");


            var path = "/api/MasterCenters/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));

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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteMasterCenter: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteMasterCenter: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไข ข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>MasterCenterDTO</returns>            
        public MasterCenterDTO EditMasterCenter(Guid? id, MasterCenterDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditMasterCenter");


            var path = "/api/MasterCenters/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditMasterCenter: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditMasterCenter: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterDTO), response.Headers);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="masterCenterGroupKey"></param> 
        /// <param name="key"></param> 
        /// <returns>MasterCenterDropdownDTO</returns>            
        public MasterCenterDropdownDTO GetFindMasterCenterDropdownItem(string masterCenterGroupKey, string key)
        {


            var path = "/api/MasterCenters/Find";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (masterCenterGroupKey != null) queryParams.Add("masterCenterGroupKey", ApiClient.ParameterToString(masterCenterGroupKey)); // query parameter
            if (key != null) queryParams.Add("key", ApiClient.ParameterToString(key)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetFindMasterCenterDropdownItem: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetFindMasterCenterDropdownItem: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterDropdownDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterDropdownDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>MasterCenterDTO</returns>            
        public MasterCenterDTO GetMasterCenter(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetMasterCenter");


            var path = "/api/MasterCenters/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));

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
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenter: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenter: " + response.ErrorMessage, response.ErrorMessage);

            return (MasterCenterDTO)ApiClient.Deserialize(response.Content, typeof(MasterCenterDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown 
        /// </summary>
        /// <param name="masterCenterGroupKey"></param> 
        /// <param name="name"></param> 
        /// <returns>List&lt;MasterCenterDropdownDTO&gt;</returns>            
        public List<MasterCenterDropdownDTO> GetMasterCenterDropdownList(string masterCenterGroupKey, string name)
        {


            var path = "/api/MasterCenters/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (masterCenterGroupKey != null) queryParams.Add("masterCenterGroupKey", ApiClient.ParameterToString(masterCenterGroupKey)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<MasterCenterDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<MasterCenterDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป 
        /// </summary>
        /// <param name="masterCenterGroupKey"></param> 
        /// <param name="order"></param> 
        /// <param name="name"></param> 
        /// <param name="key"></param> 
        /// <param name="isActive"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;MasterCenterDTO&gt;</returns>            
        public List<MasterCenterDTO> GetMasterCenterList(string masterCenterGroupKey, int? order, string name, string key, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/MasterCenters";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (masterCenterGroupKey != null) queryParams.Add("masterCenterGroupKey", ApiClient.ParameterToString(masterCenterGroupKey)); // query parameter
            if (order != null) queryParams.Add("order", ApiClient.ParameterToString(order)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (key != null) queryParams.Add("key", ApiClient.ParameterToString(key)); // query parameter
            if (isActive != null) queryParams.Add("isActive", ApiClient.ParameterToString(isActive)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetMasterCenterList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<MasterCenterDTO>)ApiClient.Deserialize(response.Content, typeof(List<MasterCenterDTO>), response.Headers);
        }

    }
}
