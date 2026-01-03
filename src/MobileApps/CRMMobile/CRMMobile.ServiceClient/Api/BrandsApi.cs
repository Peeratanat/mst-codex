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
    public interface IBrandsApi
    {
        /// <summary>
        /// สร้างแบรนด์ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>BrandDTO</returns>
        BrandDTO CreateBrand(BrandDTO input);
        /// <summary>
        /// ลบแบรนด์ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteBrand(Guid? id);
        /// <summary>
        /// แก้ไขแบรนด์ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>BrandDTO</returns>
        BrandDTO EditBrand(Guid? id, BrandDTO input);
        /// <summary>
        /// ข้อมูลแบรนด์ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BrandDTO</returns>
        BrandDTO GetBrand(Guid? id);
        /// <summary>
        /// ลิสของแบรนด์ Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;BrandDropdownDTO&gt;</returns>
        List<BrandDropdownDTO> GetBrandDropdownList(string name);
        /// <summary>
        /// ลิสของแบรนด์ 
        /// </summary>
        /// <param name="brandNo"></param>
        /// <param name="name"></param>
        /// <param name="unitNumberFormatKey"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;BrandDTO&gt;</returns>
        List<BrandDTO> GetBrandList(string brandNo, string name, string unitNumberFormatKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BrandsApi : IBrandsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrandsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BrandsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BrandsApi(String basePath)
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
        /// สร้างแบรนด์ 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>BrandDTO</returns>            
        public BrandDTO CreateBrand(BrandDTO input)
        {


            var path = "/api/Brands";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateBrand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateBrand: " + response.ErrorMessage, response.ErrorMessage);

            return (BrandDTO)ApiClient.Deserialize(response.Content, typeof(BrandDTO), response.Headers);
        }

        /// <summary>
        /// ลบแบรนด์ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteBrand(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteBrand");


            var path = "/api/Brands/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBrand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBrand: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขแบรนด์ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>BrandDTO</returns>            
        public BrandDTO EditBrand(Guid? id, BrandDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditBrand");


            var path = "/api/Brands/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditBrand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditBrand: " + response.ErrorMessage, response.ErrorMessage);

            return (BrandDTO)ApiClient.Deserialize(response.Content, typeof(BrandDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลแบรนด์ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>BrandDTO</returns>            
        public BrandDTO GetBrand(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetBrand");


            var path = "/api/Brands/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBrand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBrand: " + response.ErrorMessage, response.ErrorMessage);

            return (BrandDTO)ApiClient.Deserialize(response.Content, typeof(BrandDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของแบรนด์ Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;BrandDropdownDTO&gt;</returns>            
        public List<BrandDropdownDTO> GetBrandDropdownList(string name)
        {


            var path = "/api/Brands/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBrandDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBrandDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BrandDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<BrandDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของแบรนด์ 
        /// </summary>
        /// <param name="brandNo"></param> 
        /// <param name="name"></param> 
        /// <param name="unitNumberFormatKey"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;BrandDTO&gt;</returns>            
        public List<BrandDTO> GetBrandList(string brandNo, string name, string unitNumberFormatKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Brands";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (brandNo != null) queryParams.Add("brandNo", ApiClient.ParameterToString(brandNo)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (unitNumberFormatKey != null) queryParams.Add("unitNumberFormatKey", ApiClient.ParameterToString(unitNumberFormatKey)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBrandList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBrandList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BrandDTO>)ApiClient.Deserialize(response.Content, typeof(List<BrandDTO>), response.Headers);
        }

    }
}
