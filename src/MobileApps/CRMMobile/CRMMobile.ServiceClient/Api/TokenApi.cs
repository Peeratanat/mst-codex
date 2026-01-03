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
    public interface ITokenApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="input"></param>
        /// <returns>JsonWebToken</returns>
        JsonWebToken Login(LoginParam input);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Logout(LogoutParam input);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TokenApi : ITokenApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public TokenApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TokenApi(String basePath)
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
        ///  
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>JsonWebToken</returns>            
        public JsonWebToken Login(LoginParam input)
        {
            var path = "/api/Token/Login";
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
                throw new ApiException((int)response.StatusCode, "Error calling Login: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling Login: " + response.ErrorMessage, response.ErrorMessage);

            var result = (JsonWebToken)ApiClient.Deserialize(response.Content, typeof(JsonWebToken), response.Headers);
            if (result != null)
            {
                UserIdentify.AccessToken = result.Token;
                UserIdentify.RefreshToken = result.RefreshToken;

                UserIdentify.UserId = result.UserId;
                UserIdentify.Displayname = result.DisplayName;

                UserIdentify.Expires = result.Expires.Value;
                UserIdentify.ExpiresIn = result.ExpiresIn.Value;
               
                if (Configuration.ApiKey.ContainsKey("Authorization"))
                {
                    Configuration.ApiKey["Authorization"] = UserIdentify.AccessToken;
                }
                else
                {
                    Configuration.ApiKey.Add("Authorization", UserIdentify.AccessToken);
                }

                if (!Configuration.ApiKeyPrefix.ContainsKey("Authorization"))
                    Configuration.ApiKeyPrefix.Add("Authorization", "Bearer ");

            }

            return result;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="input"></param> 
        /// <returns></returns>            
        public void Logout(LogoutParam input)
        {


            var path = "/api/Token/Logout";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            postBody = ApiClient.Serialize(input); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling Logout: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling Logout: " + response.ErrorMessage, response.ErrorMessage);

            //UserIdentify.ClearAll();
            return;
        }

    }
}
