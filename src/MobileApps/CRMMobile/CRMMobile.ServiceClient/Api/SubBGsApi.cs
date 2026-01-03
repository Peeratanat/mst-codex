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
    public interface ISubBGsApi
    {
        /// <summary>
        /// สร้างข้อมูล Subbg 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>SubBGDTO</returns>
        SubBGDTO CreateSubBG(SubBGDTO input);
        /// <summary>
        /// ลบข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteSubBG(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>SubBGDTO</returns>
        SubBGDTO EditSubBG(Guid? id, SubBGDTO input);
        /// <summary>
        /// ลิสข้อมูล Subbg 
        /// </summary>
        /// <param name="subBGNo"></param>
        /// <param name="name"></param>
        /// <param name="bgID"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;SubBGDTO&gt;</returns>
        List<SubBGDTO> GetBGSubList(string subBGNo, string name, Guid? bgID, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// ข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SubBGDTO</returns>
        SubBGDTO GetSubBG(Guid? id);
        /// <summary>
        /// ลิสข้อมูล SUBG Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bGID"></param>
        /// <returns>List&lt;SubBGDropdownDTO&gt;</returns>
        List<SubBGDropdownDTO> GetSubBGDropdownList(string name, Guid? bGID);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class SubBGsApi : ISubBGsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubBGsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public SubBGsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubBGsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SubBGsApi(String basePath)
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
        /// สร้างข้อมูล Subbg 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>SubBGDTO</returns>            
        public SubBGDTO CreateSubBG(SubBGDTO input)
        {


            var path = "/api/SubBGs";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateSubBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateSubBG: " + response.ErrorMessage, response.ErrorMessage);

            return (SubBGDTO)ApiClient.Deserialize(response.Content, typeof(SubBGDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteSubBG(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteSubBG");


            var path = "/api/SubBGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteSubBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteSubBG: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>SubBGDTO</returns>            
        public SubBGDTO EditSubBG(Guid? id, SubBGDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditSubBG");


            var path = "/api/SubBGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditSubBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditSubBG: " + response.ErrorMessage, response.ErrorMessage);

            return (SubBGDTO)ApiClient.Deserialize(response.Content, typeof(SubBGDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูล Subbg 
        /// </summary>
        /// <param name="subBGNo"></param> 
        /// <param name="name"></param> 
        /// <param name="bgID"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;SubBGDTO&gt;</returns>            
        public List<SubBGDTO> GetBGSubList(string subBGNo, string name, Guid? bgID, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/SubBGs";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (subBGNo != null) queryParams.Add("subBGNo", ApiClient.ParameterToString(subBGNo)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (bgID != null) queryParams.Add("bgID", ApiClient.ParameterToString(bgID)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBGSubList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBGSubList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<SubBGDTO>)ApiClient.Deserialize(response.Content, typeof(List<SubBGDTO>), response.Headers);
        }

        /// <summary>
        /// ข้อมูล Subbg 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>SubBGDTO</returns>            
        public SubBGDTO GetSubBG(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetSubBG");


            var path = "/api/SubBGs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetSubBG: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubBG: " + response.ErrorMessage, response.ErrorMessage);

            return (SubBGDTO)ApiClient.Deserialize(response.Content, typeof(SubBGDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูล SUBG Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <param name="bGID"></param> 
        /// <returns>List&lt;SubBGDropdownDTO&gt;</returns>            
        public List<SubBGDropdownDTO> GetSubBGDropdownList(string name, Guid? bGID)
        {


            var path = "/api/SubBGs/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (bGID != null) queryParams.Add("bGID", ApiClient.ParameterToString(bGID)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubBGDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubBGDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<SubBGDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<SubBGDropdownDTO>), response.Headers);
        }

    }
}
