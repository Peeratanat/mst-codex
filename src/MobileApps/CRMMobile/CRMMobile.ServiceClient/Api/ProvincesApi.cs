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
    public interface IProvincesApi
    {
        /// <summary>
        /// สร้างข้อมูลจังหวัด 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ProvinceDTO</returns>
        ProvinceDTO CreateProvince(ProvinceDTO input);
        /// <summary>
        /// ลบข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteProvince(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>ProvinceDTO</returns>
        ProvinceDTO EditProvince(Guid? id, ProvinceDTO input);
        /// <summary>
        /// ข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProvinceDTO</returns>
        ProvinceDTO GetProvince(Guid? id);
        /// <summary>
        /// ลิส ข้อมูลจังหวัด Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;ProvinceListDTO&gt;</returns>
        List<ProvinceListDTO> GetProvinceDropdownList(string name);
        /// <summary>
        /// ลิส ข้อมูลจังหวัด 
        /// </summary>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="isShow"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;ProvinceDTO&gt;</returns>
        List<ProvinceDTO> GetProvinceList(string nameTH, string nameEN, bool? isShow, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ProvincesApi : IProvincesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvincesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ProvincesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvincesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProvincesApi(String basePath)
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
        /// สร้างข้อมูลจังหวัด 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>ProvinceDTO</returns>            
        public ProvinceDTO CreateProvince(ProvinceDTO input)
        {


            var path = "/api/Provinces";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateProvince: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateProvince: " + response.ErrorMessage, response.ErrorMessage);

            return (ProvinceDTO)ApiClient.Deserialize(response.Content, typeof(ProvinceDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteProvince(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteProvince");


            var path = "/api/Provinces/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteProvince: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteProvince: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>ProvinceDTO</returns>            
        public ProvinceDTO EditProvince(Guid? id, ProvinceDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditProvince");


            var path = "/api/Provinces/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditProvince: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditProvince: " + response.ErrorMessage, response.ErrorMessage);

            return (ProvinceDTO)ApiClient.Deserialize(response.Content, typeof(ProvinceDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลจังหวัด 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>ProvinceDTO</returns>            
        public ProvinceDTO GetProvince(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetProvince");


            var path = "/api/Provinces/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetProvince: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetProvince: " + response.ErrorMessage, response.ErrorMessage);

            return (ProvinceDTO)ApiClient.Deserialize(response.Content, typeof(ProvinceDTO), response.Headers);
        }

        /// <summary>
        /// ลิส ข้อมูลจังหวัด Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;ProvinceListDTO&gt;</returns>            
        public List<ProvinceListDTO> GetProvinceDropdownList(string name)
        {


            var path = "/api/Provinces/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetProvinceDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetProvinceDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<ProvinceListDTO>)ApiClient.Deserialize(response.Content, typeof(List<ProvinceListDTO>), response.Headers);
        }

        /// <summary>
        /// ลิส ข้อมูลจังหวัด 
        /// </summary>
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="isShow"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;ProvinceDTO&gt;</returns>            
        public List<ProvinceDTO> GetProvinceList(string nameTH, string nameEN, bool? isShow, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Provinces";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (isShow != null) queryParams.Add("isShow", ApiClient.ParameterToString(isShow)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetProvinceList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetProvinceList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<ProvinceDTO>)ApiClient.Deserialize(response.Content, typeof(List<ProvinceDTO>), response.Headers);
        }

    }
}
