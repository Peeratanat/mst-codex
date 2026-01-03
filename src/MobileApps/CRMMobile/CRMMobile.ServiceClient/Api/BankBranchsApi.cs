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
    public interface IBankBranchsApi
    {
        /// <summary>
        /// สร้างสาขาธนาคาร 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>BankBranchDTO</returns>
        BankBranchDTO CreateBankBranch(BankBranchDTO input);
        /// <summary>
        /// ลบสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteBankBranch(Guid? id);
        /// <summary>
        /// แก้ไขสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>BankBranchDTO</returns>
        BankBranchDTO EditBankBranch(Guid? id, BankBranchDTO input);
        /// <summary>
        /// ข้อมูลสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BankBranchDTO</returns>
        BankBranchDTO GetBankBranch(Guid? id);
        /// <summary>
        /// ดึง Dropdown สาขาธนาคาร 
        /// </summary>
        /// <param name="bankID">Bank identifier.</param>
        /// <param name="name">Name.</param>
        /// <returns>List&lt;BankBranchDropdownDTO&gt;</returns>
        List<BankBranchDropdownDTO> GetBankBranchDropdownList(Guid? bankID, string name);
        /// <summary>
        /// ลิสของสาขาธนาคาร 
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="building"></param>
        /// <param name="soi"></param>
        /// <param name="road"></param>
        /// <param name="subDistrictID"></param>
        /// <param name="districtID"></param>
        /// <param name="provinceID"></param>
        /// <param name="postalCode"></param>
        /// <param name="telephone"></param>
        /// <param name="fax"></param>
        /// <param name="isCreditBank"></param>
        /// <param name="isDirectDebit"></param>
        /// <param name="isDirectCredit"></param>
        /// <param name="areaCode"></param>
        /// <param name="oldBankID"></param>
        /// <param name="oldBranchID"></param>
        /// <param name="isActive"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;BankBranchDTO&gt;</returns>
        List<BankBranchDTO> GetBankBranchList(Guid? bankID, string name, string address, string building, string soi, string road, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string postalCode, string telephone, string fax, bool? isCreditBank, bool? isDirectDebit, bool? isDirectCredit, string areaCode, string oldBankID, string oldBranchID, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class BankBranchsApi : IBankBranchsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankBranchsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public BankBranchsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankBranchsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BankBranchsApi(String basePath)
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
        /// สร้างสาขาธนาคาร 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>BankBranchDTO</returns>            
        public BankBranchDTO CreateBankBranch(BankBranchDTO input)
        {


            var path = "/api/BankBranchs";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateBankBranch: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateBankBranch: " + response.ErrorMessage, response.ErrorMessage);

            return (BankBranchDTO)ApiClient.Deserialize(response.Content, typeof(BankBranchDTO), response.Headers);
        }

        /// <summary>
        /// ลบสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteBankBranch(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteBankBranch");


            var path = "/api/BankBranchs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankBranch: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteBankBranch: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>BankBranchDTO</returns>            
        public BankBranchDTO EditBankBranch(Guid? id, BankBranchDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditBankBranch");


            var path = "/api/BankBranchs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditBankBranch: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditBankBranch: " + response.ErrorMessage, response.ErrorMessage);

            return (BankBranchDTO)ApiClient.Deserialize(response.Content, typeof(BankBranchDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลสาขาธนาคาร 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>BankBranchDTO</returns>            
        public BankBranchDTO GetBankBranch(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetBankBranch");


            var path = "/api/BankBranchs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranch: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranch: " + response.ErrorMessage, response.ErrorMessage);

            return (BankBranchDTO)ApiClient.Deserialize(response.Content, typeof(BankBranchDTO), response.Headers);
        }

        /// <summary>
        /// ดึง Dropdown สาขาธนาคาร 
        /// </summary>
        /// <param name="bankID">Bank identifier.</param> 
        /// <param name="name">Name.</param> 
        /// <returns>List&lt;BankBranchDropdownDTO&gt;</returns>            
        public List<BankBranchDropdownDTO> GetBankBranchDropdownList(Guid? bankID, string name)
        {


            var path = "/api/BankBranchs/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranchDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranchDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankBranchDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankBranchDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของสาขาธนาคาร 
        /// </summary>
        /// <param name="bankID"></param> 
        /// <param name="name"></param> 
        /// <param name="address"></param> 
        /// <param name="building"></param> 
        /// <param name="soi"></param> 
        /// <param name="road"></param> 
        /// <param name="subDistrictID"></param> 
        /// <param name="districtID"></param> 
        /// <param name="provinceID"></param> 
        /// <param name="postalCode"></param> 
        /// <param name="telephone"></param> 
        /// <param name="fax"></param> 
        /// <param name="isCreditBank"></param> 
        /// <param name="isDirectDebit"></param> 
        /// <param name="isDirectCredit"></param> 
        /// <param name="areaCode"></param> 
        /// <param name="oldBankID"></param> 
        /// <param name="oldBranchID"></param> 
        /// <param name="isActive"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;BankBranchDTO&gt;</returns>            
        public List<BankBranchDTO> GetBankBranchList(Guid? bankID, string name, string address, string building, string soi, string road, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string postalCode, string telephone, string fax, bool? isCreditBank, bool? isDirectDebit, bool? isDirectCredit, string areaCode, string oldBankID, string oldBranchID, bool? isActive, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/BankBranchs";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter
            if (address != null) queryParams.Add("address", ApiClient.ParameterToString(address)); // query parameter
            if (building != null) queryParams.Add("building", ApiClient.ParameterToString(building)); // query parameter
            if (soi != null) queryParams.Add("soi", ApiClient.ParameterToString(soi)); // query parameter
            if (road != null) queryParams.Add("road", ApiClient.ParameterToString(road)); // query parameter
            if (subDistrictID != null) queryParams.Add("subDistrictID", ApiClient.ParameterToString(subDistrictID)); // query parameter
            if (districtID != null) queryParams.Add("districtID", ApiClient.ParameterToString(districtID)); // query parameter
            if (provinceID != null) queryParams.Add("provinceID", ApiClient.ParameterToString(provinceID)); // query parameter
            if (postalCode != null) queryParams.Add("postalCode", ApiClient.ParameterToString(postalCode)); // query parameter
            if (telephone != null) queryParams.Add("telephone", ApiClient.ParameterToString(telephone)); // query parameter
            if (fax != null) queryParams.Add("fax", ApiClient.ParameterToString(fax)); // query parameter
            if (isCreditBank != null) queryParams.Add("isCreditBank", ApiClient.ParameterToString(isCreditBank)); // query parameter
            if (isDirectDebit != null) queryParams.Add("isDirectDebit", ApiClient.ParameterToString(isDirectDebit)); // query parameter
            if (isDirectCredit != null) queryParams.Add("isDirectCredit", ApiClient.ParameterToString(isDirectCredit)); // query parameter
            if (areaCode != null) queryParams.Add("areaCode", ApiClient.ParameterToString(areaCode)); // query parameter
            if (oldBankID != null) queryParams.Add("oldBankID", ApiClient.ParameterToString(oldBankID)); // query parameter
            if (oldBranchID != null) queryParams.Add("oldBranchID", ApiClient.ParameterToString(oldBranchID)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranchList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetBankBranchList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<BankBranchDTO>)ApiClient.Deserialize(response.Content, typeof(List<BankBranchDTO>), response.Headers);
        }

    }
}
