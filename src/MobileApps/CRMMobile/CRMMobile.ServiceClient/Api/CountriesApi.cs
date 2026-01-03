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
    public interface ICountriesApi
    {
        /// <summary>
        /// สร้าง ข้อมูลประเทศ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>CountryDTO</returns>
        CountryDTO CreateCountry(CountryDTO input);
        /// <summary>
        /// ลบ ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteCountry(Guid? id);
        /// <summary>
        /// แก้ไข ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>CountryDTO</returns>
        CountryDTO EditCountry(Guid? id, CountryDTO input);
        /// <summary>
        /// ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CountryDTO</returns>
        CountryDTO GetCountry(Guid? id);
        /// <summary>
        /// ลิสของประเทศ Dropdown 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <returns>List&lt;CountryDTO&gt;</returns>
        List<CountryDTO> GetCountryDropdownList(string code, string nameTH, string nameEN, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo);
        /// <summary>
        /// ลิสของ ข้อมูลประเทศ 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;CountryDTO&gt;</returns>
        List<CountryDTO> GetCountryList(string code, string nameTH, string nameEN, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class CountriesApi : ICountriesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public CountriesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CountriesApi(String basePath)
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
        /// สร้าง ข้อมูลประเทศ 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>CountryDTO</returns>            
        public CountryDTO CreateCountry(CountryDTO input)
        {


            var path = "/api/Countries";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateCountry: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateCountry: " + response.ErrorMessage, response.ErrorMessage);

            return (CountryDTO)ApiClient.Deserialize(response.Content, typeof(CountryDTO), response.Headers);
        }

        /// <summary>
        /// ลบ ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteCountry(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteCountry");


            var path = "/api/Countries/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCountry: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCountry: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไข ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>CountryDTO</returns>            
        public CountryDTO EditCountry(Guid? id, CountryDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditCountry");


            var path = "/api/Countries/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditCountry: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditCountry: " + response.ErrorMessage, response.ErrorMessage);

            return (CountryDTO)ApiClient.Deserialize(response.Content, typeof(CountryDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลประเทศ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>CountryDTO</returns>            
        public CountryDTO GetCountry(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetCountry");


            var path = "/api/Countries/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCountry: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCountry: " + response.ErrorMessage, response.ErrorMessage);

            return (CountryDTO)ApiClient.Deserialize(response.Content, typeof(CountryDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของประเทศ Dropdown 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <returns>List&lt;CountryDTO&gt;</returns>            
        public List<CountryDTO> GetCountryDropdownList(string code, string nameTH, string nameEN, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo)
        {


            var path = "/api/Countries/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (updatedBy != null) queryParams.Add("updatedBy", ApiClient.ParameterToString(updatedBy)); // query parameter
            if (updatedFrom != null) queryParams.Add("updatedFrom", ApiClient.ParameterToString(updatedFrom)); // query parameter
            if (updatedTo != null) queryParams.Add("updatedTo", ApiClient.ParameterToString(updatedTo)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetCountryDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCountryDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<CountryDTO>)ApiClient.Deserialize(response.Content, typeof(List<CountryDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของ ข้อมูลประเทศ 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;CountryDTO&gt;</returns>            
        public List<CountryDTO> GetCountryList(string code, string nameTH, string nameEN, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Countries";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCountryList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCountryList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<CountryDTO>)ApiClient.Deserialize(response.Content, typeof(List<CountryDTO>), response.Headers);
        }

    }
}
