using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IActivitiesApi
    {
        /// <summary>
        /// Get List ข้อมูล Activity (My world) 
        /// </summary>
        /// <param name="activityTaskTopicKey"></param>
        /// <param name="activityTaskTopicKeys"></param>
        /// <param name="leadTypeKey"></param>
        /// <param name="activityTaskTypeKey"></param>
        /// <param name="activityTaskTypeKeys"></param>
        /// <param name="projectID"></param>
        /// <param name="projectIDs"></param>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="dueDateFrom"></param>
        /// <param name="dueDateTo"></param>
        /// <param name="overdueStatusKey"></param>
        /// <param name="ownerID"></param>
        /// <param name="activityTaskStatusKey"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;ActivityListDTO&gt;</returns>
        List<ActivityListDTO> GetActivityList (string activityTaskTopicKey, string activityTaskTopicKeys, string leadTypeKey, string activityTaskTypeKey, string activityTaskTypeKeys, Guid? projectID, string projectIDs, string fullName, string phoneNumber, DateTime? dueDateFrom, DateTime? dueDateTo, string overdueStatusKey, Guid? ownerID, string activityTaskStatusKey, int? page, int? pageSize, string sortBy, bool? ascending);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ActivitiesApi : IActivitiesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ActivitiesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ActivitiesApi(String basePath)
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
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get List ข้อมูล Activity (My world) 
        /// </summary>
        /// <param name="activityTaskTopicKey"></param> 
        /// <param name="activityTaskTopicKeys"></param> 
        /// <param name="leadTypeKey"></param> 
        /// <param name="activityTaskTypeKey"></param> 
        /// <param name="activityTaskTypeKeys"></param> 
        /// <param name="projectID"></param> 
        /// <param name="projectIDs"></param> 
        /// <param name="fullName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="dueDateFrom"></param> 
        /// <param name="dueDateTo"></param> 
        /// <param name="overdueStatusKey"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="activityTaskStatusKey"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;ActivityListDTO&gt;</returns>            
        public List<ActivityListDTO> GetActivityList (string activityTaskTopicKey, string activityTaskTopicKeys, string leadTypeKey, string activityTaskTypeKey, string activityTaskTypeKeys, Guid? projectID, string projectIDs, string fullName, string phoneNumber, DateTime? dueDateFrom, DateTime? dueDateTo, string overdueStatusKey, Guid? ownerID, string activityTaskStatusKey, int? page, int? pageSize, string sortBy, bool? ascending)
        {
            
    
            var path = "/api/Activities";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (activityTaskTopicKey != null) queryParams.Add("activityTaskTopicKey", ApiClient.ParameterToString(activityTaskTopicKey)); // query parameter
 if (activityTaskTopicKeys != null) queryParams.Add("activityTaskTopicKeys", ApiClient.ParameterToString(activityTaskTopicKeys)); // query parameter
 if (leadTypeKey != null) queryParams.Add("leadTypeKey", ApiClient.ParameterToString(leadTypeKey)); // query parameter
 if (activityTaskTypeKey != null) queryParams.Add("activityTaskTypeKey", ApiClient.ParameterToString(activityTaskTypeKey)); // query parameter
 if (activityTaskTypeKeys != null) queryParams.Add("activityTaskTypeKeys", ApiClient.ParameterToString(activityTaskTypeKeys)); // query parameter
 if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (projectIDs != null) queryParams.Add("projectIDs", ApiClient.ParameterToString(projectIDs)); // query parameter
 if (fullName != null) queryParams.Add("fullName", ApiClient.ParameterToString(fullName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (dueDateFrom != null) queryParams.Add("dueDateFrom", ApiClient.ParameterToString(dueDateFrom)); // query parameter
 if (dueDateTo != null) queryParams.Add("dueDateTo", ApiClient.ParameterToString(dueDateTo)); // query parameter
 if (overdueStatusKey != null) queryParams.Add("overdueStatusKey", ApiClient.ParameterToString(overdueStatusKey)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (activityTaskStatusKey != null) queryParams.Add("activityTaskStatusKey", ApiClient.ParameterToString(activityTaskStatusKey)); // query parameter
 if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
 if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
 if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetActivityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetActivityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ActivityListDTO>) ApiClient.Deserialize(response.Content, typeof(List<ActivityListDTO>), response.Headers);
        }
    
    }
}
