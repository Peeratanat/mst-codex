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
    public interface ICancelReasonsApi
    {
        /// <summary>
        /// สร้างเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>CancelReasonDTO</returns>
        CancelReasonDTO CreateCancelReason(CancelReasonDTO input);
        /// <summary>
        /// ลบเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteCancelReason(Guid? id);
        /// <summary>
        /// แก้ไขเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>CancelReasonDTO</returns>
        CancelReasonDTO EditCancelReason(Guid? id, CancelReasonDTO input);
        /// <summary>
        /// ข้อมูลเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CancelReasonDTO</returns>
        CancelReasonDTO GetCancelReason(Guid? id);
        /// <summary>
        /// ลิสของเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="description"></param>
        /// <param name="groupOfCancelReasonKey"></param>
        /// <param name="cancelApproveFlowKey"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;CancelReasonDTO&gt;</returns>
        List<CancelReasonDTO> GetCancelReasonList(string key, string description, string groupOfCancelReasonKey, string cancelApproveFlowKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class CancelReasonsApi : ICancelReasonsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelReasonsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public CancelReasonsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelReasonsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CancelReasonsApi(String basePath)
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
        /// สร้างเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>CancelReasonDTO</returns>            
        public CancelReasonDTO CreateCancelReason(CancelReasonDTO input)
        {


            var path = "/api/CancelReasons";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateCancelReason: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateCancelReason: " + response.ErrorMessage, response.ErrorMessage);

            return (CancelReasonDTO)ApiClient.Deserialize(response.Content, typeof(CancelReasonDTO), response.Headers);
        }

        /// <summary>
        /// ลบเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteCancelReason(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteCancelReason");


            var path = "/api/CancelReasons/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCancelReason: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCancelReason: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>CancelReasonDTO</returns>            
        public CancelReasonDTO EditCancelReason(Guid? id, CancelReasonDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditCancelReason");


            var path = "/api/CancelReasons/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditCancelReason: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditCancelReason: " + response.ErrorMessage, response.ErrorMessage);

            return (CancelReasonDTO)ApiClient.Deserialize(response.Content, typeof(CancelReasonDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>CancelReasonDTO</returns>            
        public CancelReasonDTO GetCancelReason(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetCancelReason");


            var path = "/api/CancelReasons/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReason: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReason: " + response.ErrorMessage, response.ErrorMessage);

            return (CancelReasonDTO)ApiClient.Deserialize(response.Content, typeof(CancelReasonDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของเหตุผลการยกเลิก 
        /// </summary>
        /// <param name="key"></param> 
        /// <param name="description"></param> 
        /// <param name="groupOfCancelReasonKey"></param> 
        /// <param name="cancelApproveFlowKey"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;CancelReasonDTO&gt;</returns>            
        public List<CancelReasonDTO> GetCancelReasonList(string key, string description, string groupOfCancelReasonKey, string cancelApproveFlowKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/CancelReasons";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (key != null) queryParams.Add("key", ApiClient.ParameterToString(key)); // query parameter
            if (description != null) queryParams.Add("description", ApiClient.ParameterToString(description)); // query parameter
            if (groupOfCancelReasonKey != null) queryParams.Add("groupOfCancelReasonKey", ApiClient.ParameterToString(groupOfCancelReasonKey)); // query parameter
            if (cancelApproveFlowKey != null) queryParams.Add("cancelApproveFlowKey", ApiClient.ParameterToString(cancelApproveFlowKey)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReasonList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCancelReasonList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<CancelReasonDTO>)ApiClient.Deserialize(response.Content, typeof(List<CancelReasonDTO>), response.Headers);
        }

    }
}
