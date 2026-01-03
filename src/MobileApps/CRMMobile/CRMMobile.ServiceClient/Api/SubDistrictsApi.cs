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
    public interface ISubDistrictsApi
    {
        /// <summary>
        /// สร้างข้อมูล อำเภอ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>SubDistrictDTO</returns>
        SubDistrictDTO CreateSubDistrict(SubDistrictDTO input);
        /// <summary>
        /// ลบข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteSubDistrict(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>SubDistrictDTO</returns>
        SubDistrictDTO EditSubDistrict(Guid? id, SubDistrictDTO input);
        /// <summary>
        /// ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SubDistrictDTO</returns>
        SubDistrictDTO GetSubDistrict(Guid? id);
        /// <summary>
        /// ลิส ข้อมูลอำเภอ Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="districtID"></param>
        /// <returns>List&lt;SubDistrictListDTO&gt;</returns>
        List<SubDistrictListDTO> GetSubDistrictDropdownList(string name, Guid? districtID);
        /// <summary>
        /// ลิสข้อมูลอำเภอ 
        /// </summary>
        /// <param name="districtID"></param>
        /// <param name="landOfficeID"></param>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="postalCode"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;SubDistrictDTO&gt;</returns>
        List<SubDistrictDTO> GetSubDistrictList(Guid? districtID, Guid? landOfficeID, string nameTH, string nameEN, string postalCode, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class SubDistrictsApi : ISubDistrictsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubDistrictsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public SubDistrictsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubDistrictsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SubDistrictsApi(String basePath)
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
        /// สร้างข้อมูล อำเภอ 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>SubDistrictDTO</returns>            
        public SubDistrictDTO CreateSubDistrict(SubDistrictDTO input)
        {


            var path = "/api/SubDistricts";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateSubDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateSubDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (SubDistrictDTO)ApiClient.Deserialize(response.Content, typeof(SubDistrictDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteSubDistrict(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteSubDistrict");


            var path = "/api/SubDistricts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteSubDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteSubDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>SubDistrictDTO</returns>            
        public SubDistrictDTO EditSubDistrict(Guid? id, SubDistrictDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditSubDistrict");


            var path = "/api/SubDistricts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditSubDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditSubDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (SubDistrictDTO)ApiClient.Deserialize(response.Content, typeof(SubDistrictDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>SubDistrictDTO</returns>            
        public SubDistrictDTO GetSubDistrict(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetSubDistrict");


            var path = "/api/SubDistricts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (SubDistrictDTO)ApiClient.Deserialize(response.Content, typeof(SubDistrictDTO), response.Headers);
        }

        /// <summary>
        /// ลิส ข้อมูลอำเภอ Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <param name="districtID"></param> 
        /// <returns>List&lt;SubDistrictListDTO&gt;</returns>            
        public List<SubDistrictListDTO> GetSubDistrictDropdownList(string name, Guid? districtID)
        {


            var path = "/api/SubDistricts/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (districtID != null) queryParams.Add("districtID", ApiClient.ParameterToString(districtID)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrictDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrictDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<SubDistrictListDTO>)ApiClient.Deserialize(response.Content, typeof(List<SubDistrictListDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลอำเภอ 
        /// </summary>
        /// <param name="districtID"></param> 
        /// <param name="landOfficeID"></param> 
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="postalCode"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;SubDistrictDTO&gt;</returns>            
        public List<SubDistrictDTO> GetSubDistrictList(Guid? districtID, Guid? landOfficeID, string nameTH, string nameEN, string postalCode, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/SubDistricts";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (districtID != null) queryParams.Add("districtID", ApiClient.ParameterToString(districtID)); // query parameter
            if (landOfficeID != null) queryParams.Add("landOfficeID", ApiClient.ParameterToString(landOfficeID)); // query parameter
            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (postalCode != null) queryParams.Add("postalCode", ApiClient.ParameterToString(postalCode)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrictList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetSubDistrictList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<SubDistrictDTO>)ApiClient.Deserialize(response.Content, typeof(List<SubDistrictDTO>), response.Headers);
        }

    }
}
