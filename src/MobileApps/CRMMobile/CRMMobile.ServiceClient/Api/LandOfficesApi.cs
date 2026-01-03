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
    public interface ILandOfficesApi
    {
        /// <summary>
        /// สร้างข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>LandOfficeDTO</returns>
        LandOfficeDTO CreateLandOffice(LandOfficeDTO input);
        /// <summary>
        /// ลบข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteLandOffice(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LandOfficeDTO</returns>
        LandOfficeDTO EditLandOffice(Guid? id, LandOfficeDTO input);
        /// <summary>
        /// ข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LandOfficeDTO</returns>
        LandOfficeDTO GetLandOffice(Guid? id);
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;LandOfficeListDTO&gt;</returns>
        List<LandOfficeListDTO> GetLandOfficeDropdownList(string name);
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน 
        /// </summary>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="subDistrictID"></param>
        /// <param name="districtID"></param>
        /// <param name="provinceID"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;LandOfficeDTO&gt;</returns>
        List<LandOfficeDTO> GetLandOfficeList(string nameTH, string nameEN, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class LandOfficesApi : ILandOfficesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LandOfficesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public LandOfficesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandOfficesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public LandOfficesApi(String basePath)
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
        /// สร้างข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>LandOfficeDTO</returns>            
        public LandOfficeDTO CreateLandOffice(LandOfficeDTO input)
        {


            var path = "/api/LandOffices";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateLandOffice: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateLandOffice: " + response.ErrorMessage, response.ErrorMessage);

            return (LandOfficeDTO)ApiClient.Deserialize(response.Content, typeof(LandOfficeDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteLandOffice(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteLandOffice");


            var path = "/api/LandOffices/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteLandOffice: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteLandOffice: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>LandOfficeDTO</returns>            
        public LandOfficeDTO EditLandOffice(Guid? id, LandOfficeDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditLandOffice");


            var path = "/api/LandOffices/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditLandOffice: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditLandOffice: " + response.ErrorMessage, response.ErrorMessage);

            return (LandOfficeDTO)ApiClient.Deserialize(response.Content, typeof(LandOfficeDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลสำนักงานที่ดิน 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>LandOfficeDTO</returns>            
        public LandOfficeDTO GetLandOffice(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetLandOffice");


            var path = "/api/LandOffices/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOffice: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOffice: " + response.ErrorMessage, response.ErrorMessage);

            return (LandOfficeDTO)ApiClient.Deserialize(response.Content, typeof(LandOfficeDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;LandOfficeListDTO&gt;</returns>            
        public List<LandOfficeListDTO> GetLandOfficeDropdownList(string name)
        {


            var path = "/api/LandOffices/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOfficeDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOfficeDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<LandOfficeListDTO>)ApiClient.Deserialize(response.Content, typeof(List<LandOfficeListDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน 
        /// </summary>
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="subDistrictID"></param> 
        /// <param name="districtID"></param> 
        /// <param name="provinceID"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;LandOfficeDTO&gt;</returns>            
        public List<LandOfficeDTO> GetLandOfficeList(string nameTH, string nameEN, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/LandOffices";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (subDistrictID != null) queryParams.Add("subDistrictID", ApiClient.ParameterToString(subDistrictID)); // query parameter
            if (districtID != null) queryParams.Add("districtID", ApiClient.ParameterToString(districtID)); // query parameter
            if (provinceID != null) queryParams.Add("provinceID", ApiClient.ParameterToString(provinceID)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOfficeList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLandOfficeList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<LandOfficeDTO>)ApiClient.Deserialize(response.Content, typeof(List<LandOfficeDTO>), response.Headers);
        }

    }
}
