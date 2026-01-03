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
    public interface IAgentEmployeesApi
    {
        /// <summary>
        /// สร้างข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>AgentEmployeeDTO</returns>
        AgentEmployeeDTO CreateAgentEmployee(AgentEmployeeDTO input);
        /// <summary>
        /// ลบข้อมุล AgentEmployee 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteAgentEmployee(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>AgentEmployeeDTO</returns>
        AgentEmployeeDTO EditAgentEmployee(Guid? id, AgentEmployeeDTO input);
        /// <summary>
        /// ข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AgentEmployeeDTO</returns>
        AgentEmployeeDTO GetAgentEmployee(Guid? id);
        /// <summary>
        /// ข้อมูล AgentEmployee Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;AgentEmployeeDropdownDTO&gt;</returns>
        List<AgentEmployeeDropdownDTO> GetAgentEmployeesDropdownList(string name);
        /// <summary>
        /// ลิสข้องข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="telNo"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;AgentEmployeeDTO&gt;</returns>
        List<AgentEmployeeDTO> GetAgentEmployeesList(string firstName, string lastName, string telNo, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class AgentEmployeesApi : IAgentEmployeesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentEmployeesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public AgentEmployeesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentEmployeesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AgentEmployeesApi(String basePath)
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
        /// สร้างข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>AgentEmployeeDTO</returns>            
        public AgentEmployeeDTO CreateAgentEmployee(AgentEmployeeDTO input)
        {


            var path = "/api/AgentEmployees";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateAgentEmployee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateAgentEmployee: " + response.ErrorMessage, response.ErrorMessage);

            return (AgentEmployeeDTO)ApiClient.Deserialize(response.Content, typeof(AgentEmployeeDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมุล AgentEmployee 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteAgentEmployee(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteAgentEmployee");


            var path = "/api/AgentEmployees/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteAgentEmployee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteAgentEmployee: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>AgentEmployeeDTO</returns>            
        public AgentEmployeeDTO EditAgentEmployee(Guid? id, AgentEmployeeDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditAgentEmployee");


            var path = "/api/AgentEmployees/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditAgentEmployee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditAgentEmployee: " + response.ErrorMessage, response.ErrorMessage);

            return (AgentEmployeeDTO)ApiClient.Deserialize(response.Content, typeof(AgentEmployeeDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>AgentEmployeeDTO</returns>            
        public AgentEmployeeDTO GetAgentEmployee(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetAgentEmployee");


            var path = "/api/AgentEmployees/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployee: " + response.ErrorMessage, response.ErrorMessage);

            return (AgentEmployeeDTO)ApiClient.Deserialize(response.Content, typeof(AgentEmployeeDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูล AgentEmployee Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;AgentEmployeeDropdownDTO&gt;</returns>            
        public List<AgentEmployeeDropdownDTO> GetAgentEmployeesDropdownList(string name)
        {


            var path = "/api/AgentEmployees/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployeesDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployeesDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<AgentEmployeeDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<AgentEmployeeDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสข้องข้อมูล AgentEmployee 
        /// </summary>
        /// <param name="firstName"></param> 
        /// <param name="lastName"></param> 
        /// <param name="telNo"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;AgentEmployeeDTO&gt;</returns>            
        public List<AgentEmployeeDTO> GetAgentEmployeesList(string firstName, string lastName, string telNo, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/AgentEmployees";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (firstName != null) queryParams.Add("firstName", ApiClient.ParameterToString(firstName)); // query parameter
            if (lastName != null) queryParams.Add("lastName", ApiClient.ParameterToString(lastName)); // query parameter
            if (telNo != null) queryParams.Add("telNo", ApiClient.ParameterToString(telNo)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployeesList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetAgentEmployeesList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<AgentEmployeeDTO>)ApiClient.Deserialize(response.Content, typeof(List<AgentEmployeeDTO>), response.Headers);
        }

    }
}
