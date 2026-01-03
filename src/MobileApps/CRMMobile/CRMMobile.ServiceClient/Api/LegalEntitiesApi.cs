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
    public interface ILegalEntitiesApi
    {
        /// <summary>
        /// สร้างข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>LegalEntityDTO</returns>
        LegalEntityDTO CreateLegalEntitiy(LegalEntityDTO input);
        /// <summary>
        /// ลบข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteLegalEntitiy(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LegalEntityDTO</returns>
        LegalEntityDTO EditLegalEntitiy(Guid? id, LegalEntityDTO input);
        /// <summary>
        /// ข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LegalEntityDTO</returns>
        LegalEntityDTO GetLegalEntitiy(Guid? id);
        /// <summary>
        /// ลิสข้อมูลนิติบุคคล Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;LegalEntityDropdownDTO&gt;</returns>
        List<LegalEntityDropdownDTO> GetLegalEntitiyDropdownList(string name);
        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="bankID"></param>
        /// <param name="bankAccountTypeKey"></param>
        /// <param name="bankAccountNo"></param>
        /// <param name="isActive"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;LegalEntityDTO&gt;</returns>
        List<LegalEntityDTO> GetLegalEntitiyList(string nameTH, string nameEN, Guid? bankID, string bankAccountTypeKey, string bankAccountNo, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class LegalEntitiesApi : ILegalEntitiesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegalEntitiesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public LegalEntitiesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LegalEntitiesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public LegalEntitiesApi(String basePath)
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
        /// สร้างข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>LegalEntityDTO</returns>            
        public LegalEntityDTO CreateLegalEntitiy(LegalEntityDTO input)
        {


            var path = "/api/LegalEntities";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateLegalEntitiy: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateLegalEntitiy: " + response.ErrorMessage, response.ErrorMessage);

            return (LegalEntityDTO)ApiClient.Deserialize(response.Content, typeof(LegalEntityDTO), response.Headers);
        }

        /// <summary>
        /// ลบข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteLegalEntitiy(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteLegalEntitiy");


            var path = "/api/LegalEntities/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteLegalEntitiy: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteLegalEntitiy: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>LegalEntityDTO</returns>            
        public LegalEntityDTO EditLegalEntitiy(Guid? id, LegalEntityDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditLegalEntitiy");


            var path = "/api/LegalEntities/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditLegalEntitiy: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditLegalEntitiy: " + response.ErrorMessage, response.ErrorMessage);

            return (LegalEntityDTO)ApiClient.Deserialize(response.Content, typeof(LegalEntityDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>LegalEntityDTO</returns>            
        public LegalEntityDTO GetLegalEntitiy(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetLegalEntitiy");


            var path = "/api/LegalEntities/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiy: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiy: " + response.ErrorMessage, response.ErrorMessage);

            return (LegalEntityDTO)ApiClient.Deserialize(response.Content, typeof(LegalEntityDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลนิติบุคคล Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;LegalEntityDropdownDTO&gt;</returns>            
        public List<LegalEntityDropdownDTO> GetLegalEntitiyDropdownList(string name)
        {


            var path = "/api/LegalEntities/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiyDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiyDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<LegalEntityDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<LegalEntityDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล 
        /// </summary>
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="bankID"></param> 
        /// <param name="bankAccountTypeKey"></param> 
        /// <param name="bankAccountNo"></param> 
        /// <param name="isActive"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;LegalEntityDTO&gt;</returns>            
        public List<LegalEntityDTO> GetLegalEntitiyList(string nameTH, string nameEN, Guid? bankID, string bankAccountTypeKey, string bankAccountNo, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/LegalEntities";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (bankAccountTypeKey != null) queryParams.Add("bankAccountTypeKey", ApiClient.ParameterToString(bankAccountTypeKey)); // query parameter
            if (bankAccountNo != null) queryParams.Add("bankAccountNo", ApiClient.ParameterToString(bankAccountNo)); // query parameter
            if (isActive != null) queryParams.Add("isActive", ApiClient.ParameterToString(isActive)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiyList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetLegalEntitiyList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<LegalEntityDTO>)ApiClient.Deserialize(response.Content, typeof(List<LegalEntityDTO>), response.Headers);
        }

    }
}
