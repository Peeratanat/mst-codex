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
    public interface IOpportunitiesApi
    {
        /// <summary>
        /// Assign ผู้ดูแลหลายคน 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>OpportunityAssignDTO</returns>
        OpportunityAssignDTO AssignOpportunityList (OpportunityAssignDTO input);
        /// <summary>
        /// สร้าง Opportunity 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fromVisitorID"></param>
        /// <returns>OpportunityDTO</returns>
        OpportunityDTO CreateOpportunity (OpportunityDTO input, Guid? fromVisitorID);
        /// <summary>
        /// สร้าง Activity 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityActivityDTO</returns>
        OpportunityActivityDTO CreateOpportunityActivity (Guid? opportunityID, OpportunityActivityDTO input);
        /// <summary>
        /// สร้าง Revisit 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="input"></param>
        /// <returns>RevisitActivityDTO</returns>
        RevisitActivityDTO CreateRevisit (Guid? opportunityID, RevisitActivityDTO input);
        /// <summary>
        /// ลบ Opportunity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteOpportunity (Guid? id);
        /// <summary>
        /// ลบ Activity 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteOpportunityActivity (Guid? opportunityID, Guid? id);
        /// <summary>
        /// ลบ Revisit 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteRevisitActivity (Guid? opportunityID, Guid? id);
        /// <summary>
        /// แก้ไข Opportunity 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityDTO</returns>
        OpportunityDTO EditOpportunity (Guid? id, OpportunityDTO input);
        /// <summary>
        /// แก้ไข Activity 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityActivityDTO</returns>
        OpportunityActivityDTO EditOpportunityActivity (Guid? opportunityID, Guid? id, OpportunityActivityDTO input);
        /// <summary>
        /// แก้ไข Revisit 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>RevisitActivityDTO</returns>
        RevisitActivityDTO EditOpportunityRevisit (Guid? opportunityID, Guid? id, RevisitActivityDTO input);
        /// <summary>
        /// Get Opportunity ทีละรายการ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OpportunityDTO</returns>
        OpportunityDTO GetOpportunity (Guid? id);
        /// <summary>
        /// Get ข้อมูล Activity 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns>OpportunityActivityDTO</returns>
        OpportunityActivityDTO GetOpportunityActivity (Guid? opportunityID, Guid? id);
        /// <summary>
        /// Get Draft Activity 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>OpportunityActivityDTO</returns>
        OpportunityActivityDTO GetOpportunityActivityDraft (Guid? opportunityID);
        /// <summary>
        /// Get ข้อมูล Activity ทั้งหมด 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>List&lt;OpportunityActivityListDTO&gt;</returns>
        List<OpportunityActivityListDTO> GetOpportunityActivityList (Guid? opportunityID);
        /// <summary>
        /// Get Opportunity ทั้งหมด 
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="arriveDateFrom"></param>
        /// <param name="arriveDateTo"></param>
        /// <param name="contactID"></param>
        /// <param name="contactNo"></param>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="salesOpportunityKey"></param>
        /// <param name="ownerID"></param>
        /// <param name="statusQuestionaireKey"></param>
        /// <param name="updatedDateFrom"></param>
        /// <param name="updatedDateTo"></param>
        /// <param name="excludeIDs"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;OpportunityListDTO&gt;</returns>
        List<OpportunityListDTO> GetOpportunityList (Guid? projectID, DateTime? arriveDateFrom, DateTime? arriveDateTo, Guid? contactID, string contactNo, string fullName, string phoneNumber, string salesOpportunityKey, Guid? ownerID, string statusQuestionaireKey, DateTime? updatedDateFrom, DateTime? updatedDateTo, string excludeIDs, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// Get ข้อมูล Revisit 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns>RevisitActivityDTO</returns>
        RevisitActivityDTO GetRevisit (Guid? opportunityID, Guid? id);
        /// <summary>
        /// Get Draft Revisit 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>RevisitActivityDTO</returns>
        RevisitActivityDTO GetRevisitDraft (Guid? opportunityID);
        /// <summary>
        /// Get ข้อมูล Revisit ทั้งหมด 
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>List&lt;RevisitActivityListDTO&gt;</returns>
        List<RevisitActivityListDTO> GetRevisitList (Guid? opportunityID);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="inputs"></param>
        /// <returns>OpportunityAssignDTO</returns>
        OpportunityAssignDTO RandomAssignOpportunityList (Guid? projectID, List<OpportunityListDTO> inputs);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class OpportunitiesApi : IOpportunitiesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpportunitiesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public OpportunitiesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="OpportunitiesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public OpportunitiesApi(String basePath)
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
        /// Assign ผู้ดูแลหลายคน 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>OpportunityAssignDTO</returns>            
        public OpportunityAssignDTO AssignOpportunityList (OpportunityAssignDTO input)
        {
            
    
            var path = "/api/Opportunities/Assigns";
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
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling AssignOpportunityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AssignOpportunityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityAssignDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityAssignDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้าง Opportunity 
        /// </summary>
        /// <param name="input"></param> 
        /// <param name="fromVisitorID"></param> 
        /// <returns>OpportunityDTO</returns>            
        public OpportunityDTO CreateOpportunity (OpportunityDTO input, Guid? fromVisitorID)
        {
            
    
            var path = "/api/Opportunities";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (fromVisitorID != null) queryParams.Add("fromVisitorID", ApiClient.ParameterToString(fromVisitorID)); // query parameter
                                    postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateOpportunity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateOpportunity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้าง Activity 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="input"></param> 
        /// <returns>OpportunityActivityDTO</returns>            
        public OpportunityActivityDTO CreateOpportunityActivity (Guid? opportunityID, OpportunityActivityDTO input)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling CreateOpportunityActivity");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateOpportunityActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateOpportunityActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityActivityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้าง Revisit 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="input"></param> 
        /// <returns>RevisitActivityDTO</returns>            
        public RevisitActivityDTO CreateRevisit (Guid? opportunityID, RevisitActivityDTO input)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling CreateRevisit");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateRevisit: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateRevisit: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RevisitActivityDTO) ApiClient.Deserialize(response.Content, typeof(RevisitActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// ลบ Opportunity 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteOpportunity (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteOpportunity");
            
    
            var path = "/api/Opportunities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteOpportunity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteOpportunity: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// ลบ Activity 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteOpportunityActivity (Guid? opportunityID, Guid? id)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling DeleteOpportunityActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteOpportunityActivity");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteOpportunityActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteOpportunityActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// ลบ Revisit 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteRevisitActivity (Guid? opportunityID, Guid? id)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling DeleteRevisitActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteRevisitActivity");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteRevisitActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteRevisitActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// แก้ไข Opportunity 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>OpportunityDTO</returns>            
        public OpportunityDTO EditOpportunity (Guid? id, OpportunityDTO input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditOpportunity");
            
    
            var path = "/api/Opportunities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityDTO), response.Headers);
        }
    
        /// <summary>
        /// แก้ไข Activity 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>OpportunityActivityDTO</returns>            
        public OpportunityActivityDTO EditOpportunityActivity (Guid? opportunityID, Guid? id, OpportunityActivityDTO input)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling EditOpportunityActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditOpportunityActivity");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunityActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunityActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityActivityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// แก้ไข Revisit 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>RevisitActivityDTO</returns>            
        public RevisitActivityDTO EditOpportunityRevisit (Guid? opportunityID, Guid? id, RevisitActivityDTO input)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling EditOpportunityRevisit");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditOpportunityRevisit");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunityRevisit: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditOpportunityRevisit: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RevisitActivityDTO) ApiClient.Deserialize(response.Content, typeof(RevisitActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get Opportunity ทีละรายการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>OpportunityDTO</returns>            
        public OpportunityDTO GetOpportunity (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetOpportunity");
            
    
            var path = "/api/Opportunities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Activity 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <returns>OpportunityActivityDTO</returns>            
        public OpportunityActivityDTO GetOpportunityActivity (Guid? opportunityID, Guid? id)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetOpportunityActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetOpportunityActivity");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityActivityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get Draft Activity 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <returns>OpportunityActivityDTO</returns>            
        public OpportunityActivityDTO GetOpportunityActivityDraft (Guid? opportunityID)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetOpportunityActivityDraft");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities/Draft";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivityDraft: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivityDraft: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityActivityDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Activity ทั้งหมด 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <returns>List&lt;OpportunityActivityListDTO&gt;</returns>            
        public List<OpportunityActivityListDTO> GetOpportunityActivityList (Guid? opportunityID)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetOpportunityActivityList");
            
    
            var path = "/api/Opportunities/{opportunityID}/Activities";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityActivityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<OpportunityActivityListDTO>) ApiClient.Deserialize(response.Content, typeof(List<OpportunityActivityListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get Opportunity ทั้งหมด 
        /// </summary>
        /// <param name="projectID"></param> 
        /// <param name="arriveDateFrom"></param> 
        /// <param name="arriveDateTo"></param> 
        /// <param name="contactID"></param> 
        /// <param name="contactNo"></param> 
        /// <param name="fullName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="salesOpportunityKey"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="statusQuestionaireKey"></param> 
        /// <param name="updatedDateFrom"></param> 
        /// <param name="updatedDateTo"></param> 
        /// <param name="excludeIDs"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;OpportunityListDTO&gt;</returns>            
        public List<OpportunityListDTO> GetOpportunityList (Guid? projectID, DateTime? arriveDateFrom, DateTime? arriveDateTo, Guid? contactID, string contactNo, string fullName, string phoneNumber, string salesOpportunityKey, Guid? ownerID, string statusQuestionaireKey, DateTime? updatedDateFrom, DateTime? updatedDateTo, string excludeIDs, int? page, int? pageSize, string sortBy, bool? ascending)
        {
            
    
            var path = "/api/Opportunities";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (arriveDateFrom != null) queryParams.Add("arriveDateFrom", ApiClient.ParameterToString(arriveDateFrom)); // query parameter
 if (arriveDateTo != null) queryParams.Add("arriveDateTo", ApiClient.ParameterToString(arriveDateTo)); // query parameter
 if (contactID != null) queryParams.Add("contactID", ApiClient.ParameterToString(contactID)); // query parameter
 if (contactNo != null) queryParams.Add("contactNo", ApiClient.ParameterToString(contactNo)); // query parameter
 if (fullName != null) queryParams.Add("fullName", ApiClient.ParameterToString(fullName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (salesOpportunityKey != null) queryParams.Add("salesOpportunityKey", ApiClient.ParameterToString(salesOpportunityKey)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (statusQuestionaireKey != null) queryParams.Add("statusQuestionaireKey", ApiClient.ParameterToString(statusQuestionaireKey)); // query parameter
 if (updatedDateFrom != null) queryParams.Add("updatedDateFrom", ApiClient.ParameterToString(updatedDateFrom)); // query parameter
 if (updatedDateTo != null) queryParams.Add("updatedDateTo", ApiClient.ParameterToString(updatedDateTo)); // query parameter
 if (excludeIDs != null) queryParams.Add("excludeIDs", ApiClient.ParameterToString(excludeIDs)); // query parameter
 if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
 if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
 if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetOpportunityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<OpportunityListDTO>) ApiClient.Deserialize(response.Content, typeof(List<OpportunityListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Revisit 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <param name="id"></param> 
        /// <returns>RevisitActivityDTO</returns>            
        public RevisitActivityDTO GetRevisit (Guid? opportunityID, Guid? id)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetRevisit");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetRevisit");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisit: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisit: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RevisitActivityDTO) ApiClient.Deserialize(response.Content, typeof(RevisitActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get Draft Revisit 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <returns>RevisitActivityDTO</returns>            
        public RevisitActivityDTO GetRevisitDraft (Guid? opportunityID)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetRevisitDraft");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits/Draft";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisitDraft: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisitDraft: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RevisitActivityDTO) ApiClient.Deserialize(response.Content, typeof(RevisitActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Revisit ทั้งหมด 
        /// </summary>
        /// <param name="opportunityID"></param> 
        /// <returns>List&lt;RevisitActivityListDTO&gt;</returns>            
        public List<RevisitActivityListDTO> GetRevisitList (Guid? opportunityID)
        {
            
            // verify the required parameter 'opportunityID' is set
            if (opportunityID == null) throw new ApiException(400, "Missing required parameter 'opportunityID' when calling GetRevisitList");
            
    
            var path = "/api/Opportunities/{opportunityID}/Revisits";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "opportunityID" + "}", ApiClient.ParameterToString(opportunityID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisitList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRevisitList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RevisitActivityListDTO>) ApiClient.Deserialize(response.Content, typeof(List<RevisitActivityListDTO>), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="projectID"></param> 
        /// <param name="inputs"></param> 
        /// <returns>OpportunityAssignDTO</returns>            
        public OpportunityAssignDTO RandomAssignOpportunityList (Guid? projectID, List<OpportunityListDTO> inputs)
        {
            
    
            var path = "/api/Opportunities/RandomAssigns";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
                                    postBody = ApiClient.Serialize(inputs); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RandomAssignOpportunityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RandomAssignOpportunityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (OpportunityAssignDTO) ApiClient.Deserialize(response.Content, typeof(OpportunityAssignDTO), response.Headers);
        }
    
    }
}
