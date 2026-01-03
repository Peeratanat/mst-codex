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
    public interface IBGsApi
    {
        /// <summary>
        /// สร้าง BG 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>BGDTO</returns>
        BGDTO CreateBG(BGDTO input);
        /// <summary>
        /// ลบ BG 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteBG(Guid? id);
        /// <summary>
        /// แก้ไข BG 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>BGDTO</returns>
        BGDTO EditBG(Guid? id, BGDTO input);
        /// <summary>
        /// ข้อมูล BG 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BGDTO</returns>
        BGDTO GetBG(Guid? id);
        /// <summary>
        /// ลิสของBG Dropdown 
        /// </summary>
        /// <param name="productTypeKey"></param>
        /// <param name="name"></param>
        /// <returns>List&lt;BGDropdownDTO&gt;</returns>
        List<BGDropdownDTO> GetBGDropdownList(string productTypeKey, string name);
        /// <summary>
        /// ลิสของ BG 
        /// </summary>
        /// <param name="bgNo"></param>
        /// <param name="name"></param>
        /// <param name="productTypeKey"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;BGDTO&gt;</returns>
        List<BGDTO> GetBGList(string bgNo, string name, string productTypeKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BGsApi : IBGsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BGsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BGsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BGsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BGsApi(String basePath)
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
        /// สร้าง BG 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>BGDTO</returns>            
        public BGDTO CreateBG(BGDTO input)
        {


            var path = "/api/BGs";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateBG: " + response.ErrorMessage, response.ErrorMessage);

            return (BGDTO)ApiClient.Deserialize(response.Content, typeof(BGDTO), response.Headers);
        }

        /// <summary>
        /// ลบ BG 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteBG(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteBG");


            var path = "/api/BGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBG: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไข BG 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>BGDTO</returns>            
        public BGDTO EditBG(Guid? id, BGDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditBG");


            var path = "/api/BGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditBG: " + response.ErrorMessage, response.ErrorMessage);

            return (BGDTO)ApiClient.Deserialize(response.Content, typeof(BGDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูล BG 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>BGDTO</returns>            
        public BGDTO GetBG(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetBG");


            var path = "/api/BGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBG: " + response.ErrorMessage, response.ErrorMessage);

            return (BGDTO)ApiClient.Deserialize(response.Content, typeof(BGDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของBG Dropdown 
        /// </summary>
        /// <param name="productTypeKey"></param> 
        /// <param name="name"></param> 
        /// <returns>List&lt;BGDropdownDTO&gt;</returns>            
        public List<BGDropdownDTO> GetBGDropdownList(string productTypeKey, string name)
        {


            var path = "/api/BGs/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (productTypeKey != null) queryParams.Add("productTypeKey", ApiClient.ParameterToString(productTypeKey)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetBGDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBGDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BGDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<BGDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของ BG 
        /// </summary>
        /// <param name="bgNo"></param> 
        /// <param name="name"></param> 
        /// <param name="productTypeKey"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;BGDTO&gt;</returns>            
        public List<BGDTO> GetBGList(string bgNo, string name, string productTypeKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/BGs";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bgNo != null) queryParams.Add("bgNo", ApiClient.ParameterToString(bgNo)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (productTypeKey != null) queryParams.Add("productTypeKey", ApiClient.ParameterToString(productTypeKey)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBGList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBGList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BGDTO>)ApiClient.Deserialize(response.Content, typeof(List<BGDTO>), response.Headers);
        }

    }
}
