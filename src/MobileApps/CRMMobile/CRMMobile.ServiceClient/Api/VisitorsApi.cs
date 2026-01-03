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
    public interface IVisitorsApi
    {
        /// <summary>
        /// สร้าง Visitor 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        VisitorDTO CreateVisitor (VisitorCreateDTO input);
        /// <summary>
        /// แก้ไขสถานะลูกค้า 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        VisitorDTO EditVisitorType (Guid? id, VisitorDTO input);
        /// <summary>
        /// พิมพ์ประวัติการเข้าออกโครงการ 
        /// </summary>
        /// <param name="receiveNumber"></param>
        /// <param name="contactNo"></param>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="ownerID"></param>
        /// <param name="vehicleDescription"></param>
        /// <param name="projectID"></param>
        /// <param name="visitByKey"></param>
        /// <param name="vehicleKey"></param>
        /// <param name="isContact"></param>
        /// <param name="visitDateInFrom"></param>
        /// <param name="visitDateInTo"></param>
        /// <returns>FileDTO</returns>
        FileDTO ExportVisitor (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo);
        /// <summary>
        /// Get visitor ทีละรายการ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>VisitorDTO</returns>
        VisitorDTO GetVisitor (Guid? id);
        /// <summary>
        /// Get ประวัติการ Call/Web/Facebook 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;LeadListDTO&gt;</returns>
        List<LeadListDTO> GetVisitorAdvertisement (Guid? id, string sortBy, bool? ascending);
        /// <summary>
        /// Get ประวัติการเยี่ยมชมโครงการ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;VisitorHistoryDTO&gt;</returns>
        List<VisitorHistoryDTO> GetVisitorHistory (Guid? id, string sortBy, bool? ascending);
        /// <summary>
        /// Get Visitor ทั้งหมด 
        /// </summary>
        /// <param name="receiveNumber"></param>
        /// <param name="contactNo"></param>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="ownerID"></param>
        /// <param name="vehicleDescription"></param>
        /// <param name="projectID"></param>
        /// <param name="visitByKey"></param>
        /// <param name="vehicleKey"></param>
        /// <param name="isContact"></param>
        /// <param name="visitDateInFrom"></param>
        /// <param name="visitDateInTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;VisitorListDTO&gt;</returns>
        List<VisitorListDTO> GetVisitorList (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// Get จำนวนผู้เข้าออกโครงการ 
        /// </summary>
        /// <param name="receiveNumber"></param>
        /// <param name="contactNo"></param>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="ownerID"></param>
        /// <param name="vehicleDescription"></param>
        /// <param name="projectID"></param>
        /// <param name="visitByKey"></param>
        /// <param name="vehicleKey"></param>
        /// <param name="isContact"></param>
        /// <param name="visitDateInFrom"></param>
        /// <param name="visitDateInTo"></param>
        /// <returns>VisitorProjectDTO</returns>
        VisitorProjectDTO GetVisitorProject (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo);
        /// <summary>
        /// ประวัติการซื้อโครงการ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;VisitorPurchaseHistoryDTO&gt;</returns>
        List<VisitorPurchaseHistoryDTO> GetVisitorPurchaseHistoryListAsync (Guid? id, string sortBy, bool? ascending);
        /// <summary>
        /// ประวัติการตอบแบบสอบถาม 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;VisitorQuestionnaireHistoryDTO&gt;</returns>
        List<VisitorQuestionnaireHistoryDTO> GetVisitorQuestionnaireHistoryListAsync (Guid? id, string sortBy, bool? ascending);
        /// <summary>
        /// บันทึกต้อนรับลูกค้า 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        VisitorDTO SubmitVisitorWelcome (Guid? id, VisitorWelcomeInput input);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class VisitorsApi : IVisitorsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public VisitorsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public VisitorsApi(String basePath)
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
        /// สร้าง Visitor 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>VisitorDTO</returns>            
        public VisitorDTO CreateVisitor (VisitorCreateDTO input)
        {
            
    
            var path = "/api/Visitors";
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateVisitor: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateVisitor: " + response.ErrorMessage, response.ErrorMessage);
    
            return (VisitorDTO) ApiClient.Deserialize(response.Content, typeof(VisitorDTO), response.Headers);
        }
    
        /// <summary>
        /// แก้ไขสถานะลูกค้า 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>VisitorDTO</returns>            
        public VisitorDTO EditVisitorType (Guid? id, VisitorDTO input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditVisitorType");
            
    
            var path = "/api/Visitors/{id}/UpdateVisitorTypes";
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
                throw new ApiException ((int)response.StatusCode, "Error calling EditVisitorType: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditVisitorType: " + response.ErrorMessage, response.ErrorMessage);
    
            return (VisitorDTO) ApiClient.Deserialize(response.Content, typeof(VisitorDTO), response.Headers);
        }
    
        /// <summary>
        /// พิมพ์ประวัติการเข้าออกโครงการ 
        /// </summary>
        /// <param name="receiveNumber"></param> 
        /// <param name="contactNo"></param> 
        /// <param name="fullName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="vehicleDescription"></param> 
        /// <param name="projectID"></param> 
        /// <param name="visitByKey"></param> 
        /// <param name="vehicleKey"></param> 
        /// <param name="isContact"></param> 
        /// <param name="visitDateInFrom"></param> 
        /// <param name="visitDateInTo"></param> 
        /// <returns>FileDTO</returns>            
        public FileDTO ExportVisitor (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo)
        {
            
    
            var path = "/api/Visitors/Exports";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (receiveNumber != null) queryParams.Add("receiveNumber", ApiClient.ParameterToString(receiveNumber)); // query parameter
 if (contactNo != null) queryParams.Add("contactNo", ApiClient.ParameterToString(contactNo)); // query parameter
 if (fullName != null) queryParams.Add("fullName", ApiClient.ParameterToString(fullName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (vehicleDescription != null) queryParams.Add("vehicleDescription", ApiClient.ParameterToString(vehicleDescription)); // query parameter
 if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (visitByKey != null) queryParams.Add("visitByKey", ApiClient.ParameterToString(visitByKey)); // query parameter
 if (vehicleKey != null) queryParams.Add("vehicleKey", ApiClient.ParameterToString(vehicleKey)); // query parameter
 if (isContact != null) queryParams.Add("isContact", ApiClient.ParameterToString(isContact)); // query parameter
 if (visitDateInFrom != null) queryParams.Add("visitDateInFrom", ApiClient.ParameterToString(visitDateInFrom)); // query parameter
 if (visitDateInTo != null) queryParams.Add("visitDateInTo", ApiClient.ParameterToString(visitDateInTo)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ExportVisitor: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ExportVisitor: " + response.ErrorMessage, response.ErrorMessage);
    
            return (FileDTO) ApiClient.Deserialize(response.Content, typeof(FileDTO), response.Headers);
        }
    
        /// <summary>
        /// Get visitor ทีละรายการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>VisitorDTO</returns>            
        public VisitorDTO GetVisitor (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetVisitor");
            
    
            var path = "/api/Visitors/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitor: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitor: " + response.ErrorMessage, response.ErrorMessage);
    
            return (VisitorDTO) ApiClient.Deserialize(response.Content, typeof(VisitorDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ประวัติการ Call/Web/Facebook 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;LeadListDTO&gt;</returns>            
        public List<LeadListDTO> GetVisitorAdvertisement (Guid? id, string sortBy, bool? ascending)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetVisitorAdvertisement");
            
    
            var path = "/api/Visitors/{id}/Advertisements";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorAdvertisement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorAdvertisement: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<LeadListDTO>) ApiClient.Deserialize(response.Content, typeof(List<LeadListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get ประวัติการเยี่ยมชมโครงการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;VisitorHistoryDTO&gt;</returns>            
        public List<VisitorHistoryDTO> GetVisitorHistory (Guid? id, string sortBy, bool? ascending)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetVisitorHistory");
            
    
            var path = "/api/Visitors/{id}/VisitHistories";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorHistory: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorHistory: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<VisitorHistoryDTO>) ApiClient.Deserialize(response.Content, typeof(List<VisitorHistoryDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get Visitor ทั้งหมด 
        /// </summary>
        /// <param name="receiveNumber"></param> 
        /// <param name="contactNo"></param> 
        /// <param name="fullName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="vehicleDescription"></param> 
        /// <param name="projectID"></param> 
        /// <param name="visitByKey"></param> 
        /// <param name="vehicleKey"></param> 
        /// <param name="isContact"></param> 
        /// <param name="visitDateInFrom"></param> 
        /// <param name="visitDateInTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;VisitorListDTO&gt;</returns>            
        public List<VisitorListDTO> GetVisitorList (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {
            
    
            var path = "/api/Visitors";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (receiveNumber != null) queryParams.Add("receiveNumber", ApiClient.ParameterToString(receiveNumber)); // query parameter
 if (contactNo != null) queryParams.Add("contactNo", ApiClient.ParameterToString(contactNo)); // query parameter
 if (fullName != null) queryParams.Add("fullName", ApiClient.ParameterToString(fullName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (vehicleDescription != null) queryParams.Add("vehicleDescription", ApiClient.ParameterToString(vehicleDescription)); // query parameter
 if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (visitByKey != null) queryParams.Add("visitByKey", ApiClient.ParameterToString(visitByKey)); // query parameter
 if (vehicleKey != null) queryParams.Add("vehicleKey", ApiClient.ParameterToString(vehicleKey)); // query parameter
 if (isContact != null) queryParams.Add("isContact", ApiClient.ParameterToString(isContact)); // query parameter
 if (visitDateInFrom != null) queryParams.Add("visitDateInFrom", ApiClient.ParameterToString(visitDateInFrom)); // query parameter
 if (visitDateInTo != null) queryParams.Add("visitDateInTo", ApiClient.ParameterToString(visitDateInTo)); // query parameter
 if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
 if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
 if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<VisitorListDTO>) ApiClient.Deserialize(response.Content, typeof(List<VisitorListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get จำนวนผู้เข้าออกโครงการ 
        /// </summary>
        /// <param name="receiveNumber"></param> 
        /// <param name="contactNo"></param> 
        /// <param name="fullName"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="ownerID"></param> 
        /// <param name="vehicleDescription"></param> 
        /// <param name="projectID"></param> 
        /// <param name="visitByKey"></param> 
        /// <param name="vehicleKey"></param> 
        /// <param name="isContact"></param> 
        /// <param name="visitDateInFrom"></param> 
        /// <param name="visitDateInTo"></param> 
        /// <returns>VisitorProjectDTO</returns>            
        public VisitorProjectDTO GetVisitorProject (string receiveNumber, string contactNo, string fullName, string phoneNumber, Guid? ownerID, string vehicleDescription, Guid? projectID, string visitByKey, string vehicleKey, bool? isContact, DateTime? visitDateInFrom, DateTime? visitDateInTo)
        {
            
    
            var path = "/api/Visitors/Projects";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (receiveNumber != null) queryParams.Add("receiveNumber", ApiClient.ParameterToString(receiveNumber)); // query parameter
 if (contactNo != null) queryParams.Add("contactNo", ApiClient.ParameterToString(contactNo)); // query parameter
 if (fullName != null) queryParams.Add("fullName", ApiClient.ParameterToString(fullName)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (ownerID != null) queryParams.Add("ownerID", ApiClient.ParameterToString(ownerID)); // query parameter
 if (vehicleDescription != null) queryParams.Add("vehicleDescription", ApiClient.ParameterToString(vehicleDescription)); // query parameter
 if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
 if (visitByKey != null) queryParams.Add("visitByKey", ApiClient.ParameterToString(visitByKey)); // query parameter
 if (vehicleKey != null) queryParams.Add("vehicleKey", ApiClient.ParameterToString(vehicleKey)); // query parameter
 if (isContact != null) queryParams.Add("isContact", ApiClient.ParameterToString(isContact)); // query parameter
 if (visitDateInFrom != null) queryParams.Add("visitDateInFrom", ApiClient.ParameterToString(visitDateInFrom)); // query parameter
 if (visitDateInTo != null) queryParams.Add("visitDateInTo", ApiClient.ParameterToString(visitDateInTo)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorProject: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorProject: " + response.ErrorMessage, response.ErrorMessage);
    
            return (VisitorProjectDTO) ApiClient.Deserialize(response.Content, typeof(VisitorProjectDTO), response.Headers);
        }
    
        /// <summary>
        /// ประวัติการซื้อโครงการ 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;VisitorPurchaseHistoryDTO&gt;</returns>            
        public List<VisitorPurchaseHistoryDTO> GetVisitorPurchaseHistoryListAsync (Guid? id, string sortBy, bool? ascending)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetVisitorPurchaseHistoryListAsync");
            
    
            var path = "/api/Visitors/{id}/PurchaseHistories";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorPurchaseHistoryListAsync: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorPurchaseHistoryListAsync: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<VisitorPurchaseHistoryDTO>) ApiClient.Deserialize(response.Content, typeof(List<VisitorPurchaseHistoryDTO>), response.Headers);
        }
    
        /// <summary>
        /// ประวัติการตอบแบบสอบถาม 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;VisitorQuestionnaireHistoryDTO&gt;</returns>            
        public List<VisitorQuestionnaireHistoryDTO> GetVisitorQuestionnaireHistoryListAsync (Guid? id, string sortBy, bool? ascending)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetVisitorQuestionnaireHistoryListAsync");
            
    
            var path = "/api/Visitors/{id}/QuestionnaireHistories";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorQuestionnaireHistoryListAsync: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetVisitorQuestionnaireHistoryListAsync: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<VisitorQuestionnaireHistoryDTO>) ApiClient.Deserialize(response.Content, typeof(List<VisitorQuestionnaireHistoryDTO>), response.Headers);
        }
    
        /// <summary>
        /// บันทึกต้อนรับลูกค้า 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>VisitorDTO</returns>            
        public VisitorDTO SubmitVisitorWelcome (Guid? id, VisitorWelcomeInput input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SubmitVisitorWelcome");
            
    
            var path = "/api/Visitors/{id}/Welcomes";
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
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubmitVisitorWelcome: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubmitVisitorWelcome: " + response.ErrorMessage, response.ErrorMessage);
    
            return (VisitorDTO) ApiClient.Deserialize(response.Content, typeof(VisitorDTO), response.Headers);
        }
    
    }
}
