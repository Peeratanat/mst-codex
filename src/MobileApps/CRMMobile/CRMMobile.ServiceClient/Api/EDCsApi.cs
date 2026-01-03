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
    public interface IEDCsApi
    {
        /// <summary>
        /// สร้างเครื่องรูดบัตร 
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>EDCDTO</returns>
        EDCDTO CreateEDC(EDCDTO input);
        /// <summary>
        /// สร้างค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>EDCFeeDTO</returns>
        EDCFeeDTO CreateEDCFee(EDCFeeDTO input);
        /// <summary>
        /// ลบเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns></returns>
        void DeleteEDC(Guid? id);
        /// <summary>
        /// ลบค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns></returns>
        void DeleteEDCFee(Guid? id);
        /// <summary>
        /// ดึงธนาคารเครื่องรูดบัตร 
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="bankName"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;EDCBankDTO&gt;</returns>
        List<EDCBankDTO> GetEDCBankList(Guid? bankID, string bankName, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// ดึงรายละเอียดเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>List&lt;EDCDTO&gt;</returns>
        List<EDCDTO> GetEDCDetail(Guid? id);
        /// <summary>
        /// ดึงค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="paymentCardTypeKey"></param>
        /// <param name="creditCardTypeKey"></param>
        /// <param name="creditCardPaymentTypeKey"></param>
        /// <param name="feeFrom"></param>
        /// <param name="feeTo"></param>
        /// <param name="isEDCBankCreditCard"></param>
        /// <param name="bankID"></param>
        /// <param name="creditCardPromotionName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;EDCFeeDTO&gt;</returns>
        List<EDCFeeDTO> GetEDCFeeList(string paymentCardTypeKey, string creditCardTypeKey, string creditCardPaymentTypeKey, double? feeFrom, double? feeTo, bool? isEDCBankCreditCard, Guid? bankID, string creditCardPromotionName, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตร 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cardMachineTypeKey"></param>
        /// <param name="bankAccountID"></param>
        /// <param name="companyID"></param>
        /// <param name="projectID"></param>
        /// <param name="projectStatusKey"></param>
        /// <param name="receiveBy"></param>
        /// <param name="receiveDateFrom"></param>
        /// <param name="receiveDateTo"></param>
        /// <param name="cardMachineStatusKey"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;EDCDTO&gt;</returns>
        List<EDCDTO> GetEDCList(string code, string cardMachineTypeKey, Guid? bankAccountID, Guid? companyID, Guid? projectID, string projectStatusKey, string receiveBy, DateTime? receiveDateFrom, DateTime? receiveDateTo, string cardMachineStatusKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// แก้ไขเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        /// <returns>EDCDTO</returns>
        EDCDTO UpdateEDC(Guid? id, EDCDTO input);
        /// <summary>
        /// แก้ไขค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        /// <returns>EDCFeeDTO</returns>
        EDCFeeDTO UpdateEDCFee(Guid? id, EDCFeeDTO input);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class EDCsApi : IEDCsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EDCsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public EDCsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EDCsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public EDCsApi(String basePath)
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
        /// สร้างเครื่องรูดบัตร 
        /// </summary>
        /// <param name="input">Input.</param> 
        /// <returns>EDCDTO</returns>            
        public EDCDTO CreateEDC(EDCDTO input)
        {


            var path = "/api/EDCs";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateEDC: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateEDC: " + response.ErrorMessage, response.ErrorMessage);

            return (EDCDTO)ApiClient.Deserialize(response.Content, typeof(EDCDTO), response.Headers);
        }

        /// <summary>
        /// สร้างค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="input">Input.</param> 
        /// <returns>EDCFeeDTO</returns>            
        public EDCFeeDTO CreateEDCFee(EDCFeeDTO input)
        {


            var path = "/api/EDCs/Fees";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateEDCFee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateEDCFee: " + response.ErrorMessage, response.ErrorMessage);

            return (EDCFeeDTO)ApiClient.Deserialize(response.Content, typeof(EDCFeeDTO), response.Headers);
        }

        /// <summary>
        /// ลบเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <returns></returns>            
        public void DeleteEDC(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteEDC");


            var path = "/api/EDCs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteEDC: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteEDC: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// ลบค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <returns></returns>            
        public void DeleteEDCFee(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteEDCFee");


            var path = "/api/EDCs/Fees/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteEDCFee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteEDCFee: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// ดึงธนาคารเครื่องรูดบัตร 
        /// </summary>
        /// <param name="bankID"></param> 
        /// <param name="bankName"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;EDCBankDTO&gt;</returns>            
        public List<EDCBankDTO> GetEDCBankList(Guid? bankID, string bankName, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/EDCs/Banks";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (bankName != null) queryParams.Add("bankName", ApiClient.ParameterToString(bankName)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCBankList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCBankList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<EDCBankDTO>)ApiClient.Deserialize(response.Content, typeof(List<EDCBankDTO>), response.Headers);
        }

        /// <summary>
        /// ดึงรายละเอียดเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <returns>List&lt;EDCDTO&gt;</returns>            
        public List<EDCDTO> GetEDCDetail(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetEDCDetail");


            var path = "/api/EDCs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCDetail: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCDetail: " + response.ErrorMessage, response.ErrorMessage);

            return (List<EDCDTO>)ApiClient.Deserialize(response.Content, typeof(List<EDCDTO>), response.Headers);
        }

        /// <summary>
        /// ดึงค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="paymentCardTypeKey"></param> 
        /// <param name="creditCardTypeKey"></param> 
        /// <param name="creditCardPaymentTypeKey"></param> 
        /// <param name="feeFrom"></param> 
        /// <param name="feeTo"></param> 
        /// <param name="isEDCBankCreditCard"></param> 
        /// <param name="bankID"></param> 
        /// <param name="creditCardPromotionName"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;EDCFeeDTO&gt;</returns>            
        public List<EDCFeeDTO> GetEDCFeeList(string paymentCardTypeKey, string creditCardTypeKey, string creditCardPaymentTypeKey, double? feeFrom, double? feeTo, bool? isEDCBankCreditCard, Guid? bankID, string creditCardPromotionName, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/EDCs/Fees";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (paymentCardTypeKey != null) queryParams.Add("paymentCardTypeKey", ApiClient.ParameterToString(paymentCardTypeKey)); // query parameter
            if (creditCardTypeKey != null) queryParams.Add("creditCardTypeKey", ApiClient.ParameterToString(creditCardTypeKey)); // query parameter
            if (creditCardPaymentTypeKey != null) queryParams.Add("creditCardPaymentTypeKey", ApiClient.ParameterToString(creditCardPaymentTypeKey)); // query parameter
            if (feeFrom != null) queryParams.Add("feeFrom", ApiClient.ParameterToString(feeFrom)); // query parameter
            if (feeTo != null) queryParams.Add("feeTo", ApiClient.ParameterToString(feeTo)); // query parameter
            if (isEDCBankCreditCard != null) queryParams.Add("isEDCBankCreditCard", ApiClient.ParameterToString(isEDCBankCreditCard)); // query parameter
            if (bankID != null) queryParams.Add("bankID", ApiClient.ParameterToString(bankID)); // query parameter
            if (creditCardPromotionName != null) queryParams.Add("creditCardPromotionName", ApiClient.ParameterToString(creditCardPromotionName)); // query parameter
            if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
            if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
            if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
            if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCFeeList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCFeeList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<EDCFeeDTO>)ApiClient.Deserialize(response.Content, typeof(List<EDCFeeDTO>), response.Headers);
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตร 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="cardMachineTypeKey"></param> 
        /// <param name="bankAccountID"></param> 
        /// <param name="companyID"></param> 
        /// <param name="projectID"></param> 
        /// <param name="projectStatusKey"></param> 
        /// <param name="receiveBy"></param> 
        /// <param name="receiveDateFrom"></param> 
        /// <param name="receiveDateTo"></param> 
        /// <param name="cardMachineStatusKey"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;EDCDTO&gt;</returns>            
        public List<EDCDTO> GetEDCList(string code, string cardMachineTypeKey, Guid? bankAccountID, Guid? companyID, Guid? projectID, string projectStatusKey, string receiveBy, DateTime? receiveDateFrom, DateTime? receiveDateTo, string cardMachineStatusKey, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/EDCs";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (cardMachineTypeKey != null) queryParams.Add("cardMachineTypeKey", ApiClient.ParameterToString(cardMachineTypeKey)); // query parameter
            if (bankAccountID != null) queryParams.Add("bankAccountID", ApiClient.ParameterToString(bankAccountID)); // query parameter
            if (companyID != null) queryParams.Add("companyID", ApiClient.ParameterToString(companyID)); // query parameter
            if (projectID != null) queryParams.Add("projectID", ApiClient.ParameterToString(projectID)); // query parameter
            if (projectStatusKey != null) queryParams.Add("projectStatusKey", ApiClient.ParameterToString(projectStatusKey)); // query parameter
            if (receiveBy != null) queryParams.Add("receiveBy", ApiClient.ParameterToString(receiveBy)); // query parameter
            if (receiveDateFrom != null) queryParams.Add("receiveDateFrom", ApiClient.ParameterToString(receiveDateFrom)); // query parameter
            if (receiveDateTo != null) queryParams.Add("receiveDateTo", ApiClient.ParameterToString(receiveDateTo)); // query parameter
            if (cardMachineStatusKey != null) queryParams.Add("cardMachineStatusKey", ApiClient.ParameterToString(cardMachineStatusKey)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetEDCList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<EDCDTO>)ApiClient.Deserialize(response.Content, typeof(List<EDCDTO>), response.Headers);
        }

        /// <summary>
        /// แก้ไขเครื่องรูดบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <param name="input">Input.</param> 
        /// <returns>EDCDTO</returns>            
        public EDCDTO UpdateEDC(Guid? id, EDCDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling UpdateEDC");


            var path = "/api/EDCs/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling UpdateEDC: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling UpdateEDC: " + response.ErrorMessage, response.ErrorMessage);

            return (EDCDTO)ApiClient.Deserialize(response.Content, typeof(EDCDTO), response.Headers);
        }

        /// <summary>
        /// แก้ไขค่าธรรมเนียมบัตร 
        /// </summary>
        /// <param name="id">Identifier.</param> 
        /// <param name="input">Input.</param> 
        /// <returns>EDCFeeDTO</returns>            
        public EDCFeeDTO UpdateEDCFee(Guid? id, EDCFeeDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling UpdateEDCFee");


            var path = "/api/EDCs/Fees/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling UpdateEDCFee: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling UpdateEDCFee: " + response.ErrorMessage, response.ErrorMessage);

            return (EDCFeeDTO)ApiClient.Deserialize(response.Content, typeof(EDCFeeDTO), response.Headers);
        }

    }
}
