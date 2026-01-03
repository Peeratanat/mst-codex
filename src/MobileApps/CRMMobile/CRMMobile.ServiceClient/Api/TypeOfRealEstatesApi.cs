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
    public interface ITypeOfRealEstatesApi
    {
        /// <summary>
        /// สร้างข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>TypeOfRealEstateDTO</returns>
        TypeOfRealEstateDTO CreateTypeOfRealEstate(TypeOfRealEstateDTO input);
        /// <summary>
        /// ลบข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteTypeOfRealEstate(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>TypeOfRealEstateDTO</returns>
        TypeOfRealEstateDTO EditTypeOfRealEstate(Guid? id, TypeOfRealEstateDTO input);
        /// <summary>
        /// ข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TypeOfRealEstateDTO</returns>
        TypeOfRealEstateDTO GetTypeOfRealEstate(Guid? id);
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;TypeOfRealEstateDropdownDTO&gt;</returns>
        List<TypeOfRealEstateDropdownDTO> GetTypeOfRealEstateDropdownList(string name);
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="realEstateCategoryKey"></param>
        /// <param name="standardCostFrom"></param>
        /// <param name="standardCostTo"></param>
        /// <param name="standardPriceFrom"></param>
        /// <param name="standardPriceTo"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;TypeOfRealEstateDTO&gt;</returns>
        List<TypeOfRealEstateDTO> GetTypeOfRealEstateList(string code, string name, string realEstateCategoryKey, double? standardCostFrom, double? standardCostTo, double? standardPriceFrom, double? standardPriceTo, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TypeOfRealEstatesApi : ITypeOfRealEstatesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeOfRealEstatesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public TypeOfRealEstatesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeOfRealEstatesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TypeOfRealEstatesApi(String basePath)
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
        /// สร้างข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>TypeOfRealEstateDTO</returns>            
        public TypeOfRealEstateDTO CreateTypeOfRealEstate(TypeOfRealEstateDTO input)
        {


            var path = "/api/TypeOfRealEstates";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateTypeOfRealEstate: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateTypeOfRealEstate: " + response.ErrorMessage, response.ErrorMessage);

            return (TypeOfRealEstateDTO)ApiClient.Deserialize(response.Content, typeof(TypeOfRealEstateDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteTypeOfRealEstate(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteTypeOfRealEstate");


            var path = "/api/TypeOfRealEstates/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteTypeOfRealEstate: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteTypeOfRealEstate: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>TypeOfRealEstateDTO</returns>            
        public TypeOfRealEstateDTO EditTypeOfRealEstate(Guid? id, TypeOfRealEstateDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditTypeOfRealEstate");


            var path = "/api/TypeOfRealEstates/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditTypeOfRealEstate: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditTypeOfRealEstate: " + response.ErrorMessage, response.ErrorMessage);

            return (TypeOfRealEstateDTO)ApiClient.Deserialize(response.Content, typeof(TypeOfRealEstateDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>TypeOfRealEstateDTO</returns>            
        public TypeOfRealEstateDTO GetTypeOfRealEstate(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetTypeOfRealEstate");


            var path = "/api/TypeOfRealEstates/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstate: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstate: " + response.ErrorMessage, response.ErrorMessage);

            return (TypeOfRealEstateDTO)ApiClient.Deserialize(response.Content, typeof(TypeOfRealEstateDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;TypeOfRealEstateDropdownDTO&gt;</returns>            
        public List<TypeOfRealEstateDropdownDTO> GetTypeOfRealEstateDropdownList(string name)
        {


            var path = "/api/TypeOfRealEstates/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstateDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstateDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<TypeOfRealEstateDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<TypeOfRealEstateDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="name"></param> 
        /// <param name="realEstateCategoryKey"></param> 
        /// <param name="standardCostFrom"></param> 
        /// <param name="standardCostTo"></param> 
        /// <param name="standardPriceFrom"></param> 
        /// <param name="standardPriceTo"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;TypeOfRealEstateDTO&gt;</returns>            
        public List<TypeOfRealEstateDTO> GetTypeOfRealEstateList(string code, string name, string realEstateCategoryKey, double? standardCostFrom, double? standardCostTo, double? standardPriceFrom, double? standardPriceTo, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/TypeOfRealEstates";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (realEstateCategoryKey != null) queryParams.Add("realEstateCategoryKey", ApiClient.ParameterToString(realEstateCategoryKey)); // query parameter
            if (standardCostFrom != null) queryParams.Add("standardCostFrom", ApiClient.ParameterToString(standardCostFrom)); // query parameter
            if (standardCostTo != null) queryParams.Add("standardCostTo", ApiClient.ParameterToString(standardCostTo)); // query parameter
            if (standardPriceFrom != null) queryParams.Add("standardPriceFrom", ApiClient.ParameterToString(standardPriceFrom)); // query parameter
            if (standardPriceTo != null) queryParams.Add("standardPriceTo", ApiClient.ParameterToString(standardPriceTo)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstateList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetTypeOfRealEstateList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<TypeOfRealEstateDTO>)ApiClient.Deserialize(response.Content, typeof(List<TypeOfRealEstateDTO>), response.Headers);
        }

    }
}
