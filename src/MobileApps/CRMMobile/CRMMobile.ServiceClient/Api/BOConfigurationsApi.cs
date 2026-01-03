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
    public interface IBOConfigurationsApi
    {
        /// <summary>
        /// แก้ไขข้อมูล BOConfig 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>BOConfigurationDTO</returns>
        BOConfigurationDTO EditBOConfiguration(Guid? id, BOConfigurationDTO input);
        /// <summary>
        /// ดึงข้อมูล BOConfig 
        /// </summary>
        /// <returns>BOConfigurationDTO</returns>
        BOConfigurationDTO GetBOConfiguration();
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BOConfigurationsApi : IBOConfigurationsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BOConfigurationsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BOConfigurationsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BOConfigurationsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BOConfigurationsApi(String basePath)
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
        /// แก้ไขข้อมูล BOConfig 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>BOConfigurationDTO</returns>            
        public BOConfigurationDTO EditBOConfiguration(Guid? id, BOConfigurationDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditBOConfiguration");


            var path = "/api/BOConfigurations/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditBOConfiguration: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditBOConfiguration: " + response.ErrorMessage, response.ErrorMessage);

            return (BOConfigurationDTO)ApiClient.Deserialize(response.Content, typeof(BOConfigurationDTO), response.Headers);
        }

        /// <summary>
        /// ดึงข้อมูล BOConfig 
        /// </summary>
        /// <returns>BOConfigurationDTO</returns>            
        public BOConfigurationDTO GetBOConfiguration()
        {


            var path = "/api/BOConfigurations";
            path = path.Replace("{format}", "json");

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
                throw new ApiException((int)response.StatusCode, "Error calling GetBOConfiguration: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBOConfiguration: " + response.ErrorMessage, response.ErrorMessage);

            return (BOConfigurationDTO)ApiClient.Deserialize(response.Content, typeof(BOConfigurationDTO), response.Headers);
        }

    }
}
