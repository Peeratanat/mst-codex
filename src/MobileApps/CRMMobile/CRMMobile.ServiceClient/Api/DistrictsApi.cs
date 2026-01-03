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
    public interface IDistrictsApi
    {
        /// <summary>
        /// สร้างอำเภอ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>DistrictDTO</returns>
        DistrictDTO CreateDistrict(DistrictDTO input);
        /// <summary>
        /// ลบ ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteDistrict(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูล อำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>DistrictDTO</returns>
        DistrictDTO EditDistrict(Guid? id, DistrictDTO input);
        /// <summary>
        /// ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DistrictDTO</returns>
        DistrictDTO GetDistrict(Guid? id);
        /// <summary>
        /// ลิสของอำเภอ dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinceID"></param>
        /// <returns>List&lt;DistrictListDTO&gt;</returns>
        List<DistrictListDTO> GetDistrictDropdownList(string name, Guid? provinceID);
        /// <summary>
        /// ลิสของอำเภอ 
        /// </summary>
        /// <param name="provinceID"></param>
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
        /// <returns>List&lt;DistrictDTO&gt;</returns>
        List<DistrictDTO> GetDistrictList(Guid? provinceID, string nameTH, string nameEN, string postalCode, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DistrictsApi : IDistrictsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public DistrictsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DistrictsApi(String basePath)
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
        /// สร้างอำเภอ 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>DistrictDTO</returns>            
        public DistrictDTO CreateDistrict(DistrictDTO input)
        {


            var path = "/api/Districts";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (DistrictDTO)ApiClient.Deserialize(response.Content, typeof(DistrictDTO), response.Headers);
        }

        /// <summary>
        /// ลบ ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteDistrict(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteDistrict");


            var path = "/api/Districts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูล อำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>DistrictDTO</returns>            
        public DistrictDTO EditDistrict(Guid? id, DistrictDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditDistrict");


            var path = "/api/Districts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (DistrictDTO)ApiClient.Deserialize(response.Content, typeof(DistrictDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลอำเภอ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>DistrictDTO</returns>            
        public DistrictDTO GetDistrict(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetDistrict");


            var path = "/api/Districts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrict: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrict: " + response.ErrorMessage, response.ErrorMessage);

            return (DistrictDTO)ApiClient.Deserialize(response.Content, typeof(DistrictDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของอำเภอ dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <param name="provinceID"></param> 
        /// <returns>List&lt;DistrictListDTO&gt;</returns>            
        public List<DistrictListDTO> GetDistrictDropdownList(string name, Guid? provinceID)
        {


            var path = "/api/Districts/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (provinceID != null) queryParams.Add("provinceID", ApiClient.ParameterToString(provinceID)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrictDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrictDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<DistrictListDTO>)ApiClient.Deserialize(response.Content, typeof(List<DistrictListDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของอำเภอ 
        /// </summary>
        /// <param name="provinceID"></param> 
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
        /// <returns>List&lt;DistrictDTO&gt;</returns>            
        public List<DistrictDTO> GetDistrictList(Guid? provinceID, string nameTH, string nameEN, string postalCode, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Districts";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (provinceID != null) queryParams.Add("provinceID", ApiClient.ParameterToString(provinceID)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrictList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetDistrictList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<DistrictDTO>)ApiClient.Deserialize(response.Content, typeof(List<DistrictDTO>), response.Headers);
        }

    }
}
