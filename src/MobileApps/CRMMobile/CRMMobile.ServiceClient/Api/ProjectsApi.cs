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
    public interface IProjectsApi
    {
        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;ProjectDTO&gt;</returns>
        List<ProjectDTO> GetProjectDropdown(string name);

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ProjectsApi : IProjectsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ProjectsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProjectsApi(String basePath)
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
        /// ลิสข้อมูลโครงการ Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;ProjectDTO&gt;</returns>            
        public List<ProjectDTO> GetProjectDropdown(string name)
        {


            var path = "/api/Projects/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetProjectDropdown: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetProjectDropdown: " + response.ErrorMessage, response.ErrorMessage);

            return (List<ProjectDTO>)ApiClient.Deserialize(response.Content, typeof(List<ProjectDTO>), response.Headers);
        }



    }
}
