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
    public interface IBanksApi
    {
        /// <summary>
        /// สร้างธนาคาร 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>BankDTO</returns>
        BankDTO CreateBank(BankDTO input);
        /// <summary>
        /// ลบธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteBank(Guid? id);
        /// <summary>
        /// แก้ไขข้อมูลธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>BankDTO</returns>
        BankDTO EditBank(Guid? id, BankDTO input);
        /// <summary>
        /// ข้อมูลธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BankDTO</returns>
        BankDTO GetBank(Guid? id);
        /// <summary>
        /// ลิสข้อมูลธนาคาร Dropdown 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List&lt;BankDropdownDTO&gt;</returns>
        List<BankDropdownDTO> GetBankDropdownList(string name);
        /// <summary>
        /// ลิสของธนาคาร 
        /// </summary>
        /// <param name="bankNo"></param>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="alias"></param>
        /// <param name="isCreditCard"></param>
        /// <param name="isNonBank"></param>
        /// <param name="isCoorperative"></param>
        /// <param name="isFreeMortgage"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;BankDTO&gt;</returns>
        List<BankDTO> GetBankList(string bankNo, string nameTH, string nameEN, string alias, bool? isCreditCard, bool? isNonBank, bool? isCoorperative, bool? isFreeMortgage, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BanksApi : IBanksApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BanksApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BanksApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BanksApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BanksApi(String basePath)
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
        /// สร้างธนาคาร 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>BankDTO</returns>            
        public BankDTO CreateBank(BankDTO input)
        {


            var path = "/api/Banks";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateBank: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateBank: " + response.ErrorMessage, response.ErrorMessage);

            return (BankDTO)ApiClient.Deserialize(response.Content, typeof(BankDTO), response.Headers);
        }

        /// <summary>
        /// ลบธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteBank(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteBank");


            var path = "/api/Banks/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBank: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBank: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขข้อมูลธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>BankDTO</returns>            
        public BankDTO EditBank(Guid? id, BankDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditBank");


            var path = "/api/Banks/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditBank: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditBank: " + response.ErrorMessage, response.ErrorMessage);

            return (BankDTO)ApiClient.Deserialize(response.Content, typeof(BankDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>BankDTO</returns>            
        public BankDTO GetBank(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetBank");


            var path = "/api/Banks/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBank: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBank: " + response.ErrorMessage, response.ErrorMessage);

            return (BankDTO)ApiClient.Deserialize(response.Content, typeof(BankDTO), response.Headers);
        }

        /// <summary>
        /// ลิสข้อมูลธนาคาร Dropdown 
        /// </summary>
        /// <param name="name"></param> 
        /// <returns>List&lt;BankDropdownDTO&gt;</returns>            
        public List<BankDropdownDTO> GetBankDropdownList(string name)
        {


            var path = "/api/Banks/DropdownList";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBankDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของธนาคาร 
        /// </summary>
        /// <param name="bankNo"></param> 
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="alias"></param> 
        /// <param name="isCreditCard"></param> 
        /// <param name="isNonBank"></param> 
        /// <param name="isCoorperative"></param> 
        /// <param name="isFreeMortgage"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;BankDTO&gt;</returns>            
        public List<BankDTO> GetBankList(string bankNo, string nameTH, string nameEN, string alias, bool? isCreditCard, bool? isNonBank, bool? isCoorperative, bool? isFreeMortgage, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Banks";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bankNo != null) queryParams.Add("bankNo", ApiClient.ParameterToString(bankNo)); // query parameter
            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (alias != null) queryParams.Add("alias", ApiClient.ParameterToString(alias)); // query parameter
            if (isCreditCard != null) queryParams.Add("isCreditCard", ApiClient.ParameterToString(isCreditCard)); // query parameter
            if (isNonBank != null) queryParams.Add("isNonBank", ApiClient.ParameterToString(isNonBank)); // query parameter
            if (isCoorperative != null) queryParams.Add("isCoorperative", ApiClient.ParameterToString(isCoorperative)); // query parameter
            if (isFreeMortgage != null) queryParams.Add("isFreeMortgage", ApiClient.ParameterToString(isFreeMortgage)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBankList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankDTO>), response.Headers);
        }

    }
}
