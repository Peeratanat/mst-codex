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
    public interface IUsersApi
    {
        /// <summary>
        /// Get ข้อมูล Dropdown List User 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;UserListDTO&gt;</returns>
        List<UserListDTO> GetUserDropdownList(string name);
        /// <summary>
        /// Get ข้อมูล List User 
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="displayName"></param>
        /// <param name="roleCodes"></param>
        /// <param name="authorizeProjectIDs"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;UserListDTO&gt;</returns>
        List<UserListDTO> GetUserList(string employeeNo, string firstName, string lastName, string displayName, string roleCodes, string authorizeProjectIDs, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class UsersApi : IUsersApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public UsersApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class.
        /// </summary>
        /// <returns></returns>
        public UsersApi(String basePath)
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
        /// Get ข้อมูล Dropdown List User 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;UserListDTO&gt;</returns>            
        public List<UserListDTO> GetUserDropdownList(string name)
        {


            var path = "/api/Users/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetUserDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetUserDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<UserListDTO>)ApiClient.Deserialize(response.Content, typeof(List<UserListDTO>), response.Headers);
        }

        /// <summary>
        /// Get ข้อมูล List User 
        /// </summary>
        /// <param name="employeeNo"></param> 
        /// <param name="firstName"></param> 
        /// <param name="lastName"></param> 
        /// <param name="displayName"></param> 
        /// <param name="roleCodes"></param> 
        /// <param name="authorizeProjectIDs"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;UserListDTO&gt;</returns>            
        public List<UserListDTO> GetUserList(string employeeNo, string firstName, string lastName, string displayName, string roleCodes, string authorizeProjectIDs, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Users";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (employeeNo != null) queryParams.Add("employeeNo", ApiClient.ParameterToString(employeeNo)); // query parameter
            if (firstName != null) queryParams.Add("firstName", ApiClient.ParameterToString(firstName)); // query parameter
            if (lastName != null) queryParams.Add("lastName", ApiClient.ParameterToString(lastName)); // query parameter
            if (displayName != null) queryParams.Add("displayName", ApiClient.ParameterToString(displayName)); // query parameter
            if (roleCodes != null) queryParams.Add("roleCodes", ApiClient.ParameterToString(roleCodes)); // query parameter
            if (authorizeProjectIDs != null) queryParams.Add("authorizeProjectIDs", ApiClient.ParameterToString(authorizeProjectIDs)); // query parameter
            if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
            if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
            if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
            if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetUserList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetUserList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<UserListDTO>)ApiClient.Deserialize(response.Content, typeof(List<UserListDTO>), response.Headers);
        }

    }
}
