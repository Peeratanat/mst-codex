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
    public interface IBankAccountsApi
    {
        /// <summary>
        /// สร้างบัญชีธนาคาร 
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>BankAccountDTO</returns>
        BankAccountDTO CreateBankAccount(BankAccountDTO input);
        /// <summary>
        /// สร้าง ข้อมูลคู่บัญชี 
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>BankAccountDTO</returns>
        BankAccountDTO CreateChartOfAccount(BankAccountDTO input);
        /// <summary>
        /// ลบบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns></returns>
        void DeleteBankAccount(Guid? id);
        /// <summary>
        /// ลบบัญชีธนาคาร ทีละหลายรายการ 
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        void DeleteBankAccountList(List<BankAccountDTO> inputs);
        /// <summary>
        /// ดึงรายละเอียดบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>List&lt;BankAccountDTO&gt;</returns>
        List<BankAccountDTO> GetBankAccountDetail(Guid? id);
        /// <summary>
        /// ดึงข้อมูลบัญชีธนาคาร 
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="bankBranchName"></param>
        /// <param name="bankAccountNo"></param>
        /// <param name="bankAccountTypeKey"></param>
        /// <param name="companyID"></param>
        /// <param name="gLAccountNo"></param>
        /// <param name="isActive"></param>
        /// <param name="hasVat"></param>
        /// <param name="gLAccountTypeKey"></param>
        /// <param name="gLRefCode"></param>
        /// <param name="name"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="updatedBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;BankAccountDTO&gt;</returns>
        List<BankAccountDTO> GetBankAccountList(Guid? bankID, string bankBranchName, string bankAccountNo, string bankAccountTypeKey, Guid? companyID, string gLAccountNo, bool? isActive, bool? hasVat, string gLAccountTypeKey, string gLRefCode, string name, DateTime? updatedFrom, DateTime? updatedTo, string updatedBy, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// แก้ไขบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        /// <returns>BankAccountDTO</returns>
        BankAccountDTO UpdateBankAccount(Guid? id, BankAccountDTO input);
        /// <summary>
        /// แก้ไข ข้อมูลคู่บัญชี 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        /// <returns>BankAccountDTO</returns>
        BankAccountDTO UpdateChartOfAccount(Guid? id, BankAccountDTO input);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BankAccountsApi : IBankAccountsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BankAccountsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BankAccountsApi(String basePath)
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
        /// สร้างบัญชีธนาคาร 
        /// </summary>
        /// <param name="input">Input.</param> 
        /// <returns>BankAccountDTO</returns>            
        public BankAccountDTO CreateBankAccount(BankAccountDTO input)
        {


            var path = "/api/BankAccounts";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateBankAccount: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateBankAccount: " + response.ErrorMessage, response.ErrorMessage);

            return (BankAccountDTO)ApiClient.Deserialize(response.Content, typeof(BankAccountDTO), response.Headers);
        }

        /// <summary>
        /// สร้าง ข้อมูลคู่บัญชี 
        /// </summary>
        /// <param name="input">Input.</param> 
        /// <returns>BankAccountDTO</returns>            
        public BankAccountDTO CreateChartOfAccount(BankAccountDTO input)
        {


            var path = "/api/BankAccounts/ChartOfAccount";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateChartOfAccount: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateChartOfAccount: " + response.ErrorMessage, response.ErrorMessage);

            return (BankAccountDTO)ApiClient.Deserialize(response.Content, typeof(BankAccountDTO), response.Headers);
        }

        /// <summary>
        /// ลบบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <returns></returns>            
        public void DeleteBankAccount(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteBankAccount");


            var path = "/api/BankAccounts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankAccount: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankAccount: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// ลบบัญชีธนาคาร ทีละหลายรายการ 
        /// </summary>
        /// <param name="inputs"></param> 
        /// <returns></returns>            
        public void DeleteBankAccountList(List<BankAccountDTO> inputs)
        {


            var path = "/api/BankAccounts";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            postBody = ApiClient.Serialize(inputs); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankAccountList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankAccountList: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// ดึงรายละเอียดบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <returns>List&lt;BankAccountDTO&gt;</returns>            
        public List<BankAccountDTO> GetBankAccountDetail(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetBankAccountDetail");


            var path = "/api/BankAccounts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBankAccountDetail: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankAccountDetail: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankAccountDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankAccountDTO>), response.Headers);
        }

        /// <summary>
        /// ดึงข้อมูลบัญชีธนาคาร 
        /// </summary>
        /// <param name="bankID"></param> 
        /// <param name="bankBranchName"></param> 
        /// <param name="bankAccountNo"></param> 
        /// <param name="bankAccountTypeKey"></param> 
        /// <param name="companyID"></param> 
        /// <param name="gLAccountNo"></param> 
        /// <param name="isActive"></param> 
        /// <param name="hasVat"></param> 
        /// <param name="gLAccountTypeKey"></param> 
        /// <param name="gLRefCode"></param> 
        /// <param name="name"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;BankAccountDTO&gt;</returns>            
        public List<BankAccountDTO> GetBankAccountList(Guid? bankID, string bankBranchName, string bankAccountNo, string bankAccountTypeKey, Guid? companyID, string gLAccountNo, bool? isActive, bool? hasVat, string gLAccountTypeKey, string gLRefCode, string name, DateTime? updatedFrom, DateTime? updatedTo, string updatedBy, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/BankAccounts";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (bankBranchName != null) queryParams.Add("bankBranchName", ApiClient.ParameterToString(bankBranchName)); // query parameter
            if (bankAccountNo != null) queryParams.Add("bankAccountNo", ApiClient.ParameterToString(bankAccountNo)); // query parameter
            if (bankAccountTypeKey != null) queryParams.Add("bankAccountTypeKey", ApiClient.ParameterToString(bankAccountTypeKey)); // query parameter
            if (companyID != null) queryParams.Add("companyID", ApiClient.ParameterToString(companyID)); // query parameter
            if (gLAccountNo != null) queryParams.Add("gLAccountNo", ApiClient.ParameterToString(gLAccountNo)); // query parameter
            if (isActive != null) queryParams.Add("isActive", ApiClient.ParameterToString(isActive)); // query parameter
            if (hasVat != null) queryParams.Add("hasVat", ApiClient.ParameterToString(hasVat)); // query parameter
            if (gLAccountTypeKey != null) queryParams.Add("gLAccountTypeKey", ApiClient.ParameterToString(gLAccountTypeKey)); // query parameter
            if (gLRefCode != null) queryParams.Add("gLRefCode", ApiClient.ParameterToString(gLRefCode)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (updatedFrom != null) queryParams.Add("updatedFrom", ApiClient.ParameterToString(updatedFrom)); // query parameter
            if (updatedTo != null) queryParams.Add("updatedTo", ApiClient.ParameterToString(updatedTo)); // query parameter
            if (updatedBy != null) queryParams.Add("updatedBy", ApiClient.ParameterToString(updatedBy)); // query parameter
            if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
            if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
            if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
            if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankAccountList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankAccountList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankAccountDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankAccountDTO>), response.Headers);
        }

        /// <summary>
        /// แก้ไขบัญชีธนาคาร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <param name="input">Input.</param> 
        /// <returns>BankAccountDTO</returns>            
        public BankAccountDTO UpdateBankAccount(Guid? id, BankAccountDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling UpdateBankAccount");


            var path = "/api/BankAccounts/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling UpdateBankAccount: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling UpdateBankAccount: " + response.ErrorMessage, response.ErrorMessage);

            return (BankAccountDTO)ApiClient.Deserialize(response.Content, typeof(BankAccountDTO), response.Headers);
        }

        /// <summary>
        /// แก้ไข ข้อมูลคู่บัญชี 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <param name="input">Input.</param> 
        /// <returns>BankAccountDTO</returns>            
        public BankAccountDTO UpdateChartOfAccount(Guid? id, BankAccountDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling UpdateChartOfAccount");


            var path = "/api/BankAccounts/ChartOfAccount/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling UpdateChartOfAccount: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling UpdateChartOfAccount: " + response.ErrorMessage, response.ErrorMessage);

            return (BankAccountDTO)ApiClient.Deserialize(response.Content, typeof(BankAccountDTO), response.Headers);
        }

    }
}
