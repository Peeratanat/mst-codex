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
    public interface ILeadsApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LeadListDTO</returns>
        LeadListDTO AssignLead (Guid? id, UserListDTO input);
        /// <summary>
        /// Assign ผู้ดูแลหลายคน 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>LeadAssignDTO</returns>
        LeadAssignDTO AssignLeadList (LeadAssignDTO input);
        /// <summary>
        /// สร้างข้อมูล Lead 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>LeadDTO</returns>
        LeadDTO CreateLead (LeadDTO input);
        /// <summary>
        /// สร้าง Activity 
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="input"></param>
        /// <returns>LeadActivityDTO</returns>
        LeadActivityDTO CreateLeadActivity (Guid? leadID, LeadActivityDTO input);
        /// <summary>
        /// ลบ Lead ทีละรายการ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteLead (Guid? id);
        /// <summary>
        /// ลบ Activity 
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteLeadActivity (Guid? leadID, Guid? id);
        /// <summary>
        /// แก้ไขข้อมูล Lead 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LeadDTO</returns>
        LeadDTO EditLead (Guid? id, LeadDTO input);
        /// <summary>
        /// แก้ไข Activity 
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LeadActivityDTO</returns>
        LeadActivityDTO EditLeadActivity (Guid? leadID, Guid? id, LeadActivityDTO input);
        /// <summary>
        /// Get ข้อมูล Lead ทีละรายการ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LeadDTO</returns>
        LeadDTO GetLead (Guid? id);
        /// <summary>
        /// Get ข้อมูล Activity 
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <returns>LeadActivityDTO</returns>
        LeadActivityDTO GetLeadActivity (Guid? leadID, Guid? id);
        /// <summary>
        /// Get ข้อมูล Draft ของ Activity ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>LeadActivityDTO</returns>
        LeadActivityDTO GetLeadActivityDraft (Guid? leadID);
        /// <summary>
        /// Get ข้อมูล Activity ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>List&lt;LeadActivityListDTO&gt;</returns>
        List<LeadActivityListDTO> GetLeadActivityList (Guid? leadID);
        /// <summary>
        /// Get List ข้อมูล Lead 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="leadTypeKey"></param>
        /// <param name="ownerID"></param>
        /// <param name="leadStatusKey"></param>
        /// <param name="projectID"></param>
        /// <param name="createdDateFrom"></param>
        /// <param name="createdDateTo"></param>
        /// <param name="excludeIDs"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;LeadListDTO&gt;</returns>
        List<LeadListDTO> GetLeadList (string firstName, string lastName, string phoneNumber, string leadTypeKey, Guid? ownerID, string leadStatusKey, Guid? projectID, DateTime? createdDateFrom, DateTime? createdDateTo, string excludeIDs, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// Get ข้อมูล Contact ที่ใกล้เคียงกับ Lead ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>List&lt;LeadQualifyDTO&gt;</returns>
        List<LeadQualifyDTO> GetLeadQualify (Guid? leadID);
        /// <summary>
        /// Assign ผู้ดูแลหลายคนแบบ Random 
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="inputs"></param>
        /// <returns>LeadAssignDTO</returns>
        LeadAssignDTO RandomAssignLeadList (Guid? projectID, List<LeadListDTO> inputs);
        /// <summary>
        /// ยืนยัน Qualify 
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="contactID"></param>
        /// <returns>LeadDTO</returns>
        LeadDTO SubmitQualify (Guid? leadID, Guid? contactID);
        /// <summary>
        /// ไม่ยืนยัน Qualify (DisQualify) 
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>LeadDTO</returns>
        LeadDTO UnSubmitQualify (Guid? leadID);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class LeadsApi : ILeadsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeadsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public LeadsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="LeadsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public LeadsApi(String basePath)
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
        ///  
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>LeadListDTO</returns>            
        public LeadListDTO AssignLead (Guid? id, UserListDTO input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling AssignLead");
            
    
            var path = "/api/Leads/{id}/Assign";
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
                throw new ApiException ((int)response.StatusCode, "Error calling AssignLead: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AssignLead: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadListDTO) ApiClient.Deserialize(response.Content, typeof(LeadListDTO), response.Headers);
        }
    
        /// <summary>
        /// Assign ผู้ดูแลหลายคน 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>LeadAssignDTO</returns>            
        public LeadAssignDTO AssignLeadList (LeadAssignDTO input)
        {
            
    
            var path = "/api/Leads/Assigns";
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
                throw new ApiException ((int)response.StatusCode, "Error calling AssignLeadList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AssignLeadList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadAssignDTO) ApiClient.Deserialize(response.Content, typeof(LeadAssignDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้างข้อมูล Lead 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>LeadDTO</returns>            
        public LeadDTO CreateLead (LeadDTO input)
        {
            
    
            var path = "/api/Leads";
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateLead: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateLead: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadDTO) ApiClient.Deserialize(response.Content, typeof(LeadDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้าง Activity 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <param name="input"></param> 
        /// <returns>LeadActivityDTO</returns>            
        public LeadActivityDTO CreateLeadActivity (Guid? leadID, LeadActivityDTO input)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling CreateLeadActivity");
            
    
            var path = "/api/Leads/{leadID}/Activities";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateLeadActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateLeadActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadActivityDTO) ApiClient.Deserialize(response.Content, typeof(LeadActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// ลบ Lead ทีละรายการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteLead (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteLead");
            
    
            var path = "/api/Leads/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteLead: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteLead: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// ลบ Activity 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteLeadActivity (Guid? leadID, Guid? id)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling DeleteLeadActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteLeadActivity");
            
    
            var path = "/api/Leads/{leadID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteLeadActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteLeadActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// แก้ไขข้อมูล Lead 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>LeadDTO</returns>            
        public LeadDTO EditLead (Guid? id, LeadDTO input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditLead");
            
    
            var path = "/api/Leads/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling EditLead: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditLead: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadDTO) ApiClient.Deserialize(response.Content, typeof(LeadDTO), response.Headers);
        }
    
        /// <summary>
        /// แก้ไข Activity 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>LeadActivityDTO</returns>            
        public LeadActivityDTO EditLeadActivity (Guid? leadID, Guid? id, LeadActivityDTO input)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling EditLeadActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditLeadActivity");
            
    
            var path = "/api/Leads/{leadID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling EditLeadActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditLeadActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadActivityDTO) ApiClient.Deserialize(response.Content, typeof(LeadActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Lead ทีละรายการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>LeadDTO</returns>            
        public LeadDTO GetLead (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetLead");
            
    
            var path = "/api/Leads/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLead: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLead: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadDTO) ApiClient.Deserialize(response.Content, typeof(LeadDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Activity 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <param name="id"></param> 
        /// <returns>LeadActivityDTO</returns>            
        public LeadActivityDTO GetLeadActivity (Guid? leadID, Guid? id)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling GetLeadActivity");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetLeadActivity");
            
    
            var path = "/api/Leads/{leadID}/Activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadActivityDTO) ApiClient.Deserialize(response.Content, typeof(LeadActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Draft ของ Activity ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <returns>LeadActivityDTO</returns>            
        public LeadActivityDTO GetLeadActivityDraft (Guid? leadID)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling GetLeadActivityDraft");
            
    
            var path = "/api/Leads/{leadID}/Activities/Draft";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivityDraft: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivityDraft: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadActivityDTO) ApiClient.Deserialize(response.Content, typeof(LeadActivityDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Activity ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <returns>List&lt;LeadActivityListDTO&gt;</returns>            
        public List<LeadActivityListDTO> GetLeadActivityList (Guid? leadID)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling GetLeadActivityList");
            
    
            var path = "/api/Leads/{leadID}/Activities";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivityList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadActivityList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<LeadActivityListDTO>) ApiClient.Deserialize(response.Content, typeof(List<LeadActivityListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get List ข้อมูล Lead 
        /// </summary>
        /// <param name="firstName"></param> 
        /// <param name="lastName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="leadTypeKey"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="leadStatusKey"></param> 
        /// <param name="projectID"></param> 
        /// <param name="createdDateFrom"></param> 
        /// <param name="createdDateTo"></param> 
        /// <param name="excludeIDs"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;LeadListDTO&gt;</returns>            
        public List<LeadListDTO> GetLeadList (string firstName, string lastName, string phoneNumber, string leadTypeKey, Guid? ownerID, string leadStatusKey, Guid? projectID, DateTime? createdDateFrom, DateTime? createdDateTo, string excludeIDs, int? page, int? pageSize, string sortBy, bool? ascending)
        {
            
    
            var path = "/api/Leads";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (firstName != null) queryParams.Add("firstName", ApiClient.ParameterToString(firstName)); // query parameter
 if (lastName != null) queryParams.Add("lastName", ApiClient.ParameterToString(lastName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (leadTypeKey != null) queryParams.Add("leadTypeKey", ApiClient.ParameterToString(leadTypeKey)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (leadStatusKey != null) queryParams.Add("leadStatusKey", ApiClient.ParameterToString(leadStatusKey)); // query parameter
 if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (createdDateFrom != null) queryParams.Add("createdDateFrom", ApiClient.ParameterToString(createdDateFrom)); // query parameter
 if (createdDateTo != null) queryParams.Add("createdDateTo", ApiClient.ParameterToString(createdDateTo)); // query parameter
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<LeadListDTO>) ApiClient.Deserialize(response.Content, typeof(List<LeadListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Contact ที่ใกล้เคียงกับ Lead ทั้งหมด 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <returns>List&lt;LeadQualifyDTO&gt;</returns>            
        public List<LeadQualifyDTO> GetLeadQualify (Guid? leadID)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling GetLeadQualify");
            
    
            var path = "/api/Leads/{leadID}/Qualifies";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadQualify: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetLeadQualify: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<LeadQualifyDTO>) ApiClient.Deserialize(response.Content, typeof(List<LeadQualifyDTO>), response.Headers);
        }
    
        /// <summary>
        /// Assign ผู้ดูแลหลายคนแบบ Random 
        /// </summary>
        /// <param name="projectID"></param> 
        /// <param name="inputs"></param> 
        /// <returns>LeadAssignDTO</returns>            
        public LeadAssignDTO RandomAssignLeadList (Guid? projectID, List<LeadListDTO> inputs)
        {
            
    
            var path = "/api/Leads/RandomAssigns";
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
                throw new ApiException ((int)response.StatusCode, "Error calling RandomAssignLeadList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RandomAssignLeadList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadAssignDTO) ApiClient.Deserialize(response.Content, typeof(LeadAssignDTO), response.Headers);
        }
    
        /// <summary>
        /// ยืนยัน Qualify 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <param name="contactID"></param> 
        /// <returns>LeadDTO</returns>            
        public LeadDTO SubmitQualify (Guid? leadID, Guid? contactID)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling SubmitQualify");
            
    
            var path = "/api/Leads/{leadID}/Qualifies";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (contactID != null) queryParams.Add("contactID", ApiClient.ParameterToString(contactID)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubmitQualify: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubmitQualify: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadDTO) ApiClient.Deserialize(response.Content, typeof(LeadDTO), response.Headers);
        }
    
        /// <summary>
        /// ไม่ยืนยัน Qualify (DisQualify) 
        /// </summary>
        /// <param name="leadID"></param> 
        /// <returns>LeadDTO</returns>            
        public LeadDTO UnSubmitQualify (Guid? leadID)
        {
            
            // verify the required parameter 'leadID' is set
            if (leadID == null) throw new ApiException(400, "Missing required parameter 'leadID' when calling UnSubmitQualify");
            
    
            var path = "/api/Leads/{leadID}/DisQualifies";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "leadID" + "}", ApiClient.ParameterToString(leadID));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UnSubmitQualify: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UnSubmitQualify: " + response.ErrorMessage, response.ErrorMessage);
    
            return (LeadDTO) ApiClient.Deserialize(response.Content, typeof(LeadDTO), response.Headers);
        }
    
    }
}
