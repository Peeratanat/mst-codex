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
    public interface IContactsApi
    {
        /// <summary>
        /// สร้าง Contact 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="similarContactID"></param>
        /// <param name="fromVisitorID"></param>
        /// <returns>ContactDTO</returns>
        ContactDTO CreateContact (ContactDTO input, Guid? similarContactID, Guid? fromVisitorID);
        /// <summary>
        /// สร้างที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน) 
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="input"></param>
        /// <returns>ContactAddressDTO</returns>
        ContactAddressDTO CreateContactAddress (Guid? contactID, ContactAddressDTO input);
        /// <summary>
        /// ลบ Contact 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteContact (Guid? id);
        /// <summary>
        /// ลบที่อยู่ที่ติดต่อได้ 
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteContactAddress (Guid? contactID, Guid? id);
        /// <summary>
        /// แก้ไข Contact 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>ContactDTO</returns>
        ContactDTO EditContact (Guid? id, ContactDTO input);
        /// <summary>
        /// แก้ไขที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน) 
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>ContactAddressDTO</returns>
        ContactAddressDTO EditContactAddress (Guid? contactID, Guid? id, ContactAddressDTO input);
        /// <summary>
        /// Get ข้อมูล Contact 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactDTO</returns>
        ContactDTO GetContact (Guid? id);
        /// <summary>
        /// Get ข้อมูลที่อยู่ 
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <returns>ContactAddressDTO</returns>
        ContactAddressDTO GetContactAddress (Guid? contactID, Guid? id);
        /// <summary>
        /// Get ข้อมูลที่อยู่ทั้งหมด 
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns>AddressDTO</returns>
        AddressDTO GetContactAddressList (Guid? contactID);
        /// <summary>
        /// Get ข้อมูล List Contact 
        /// </summary>
        /// <param name="contactNo"></param>
        /// <param name="firstNameTH"></param>
        /// <param name="lastNameTH"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="createdDateFrom"></param>
        /// <param name="createdDateTo"></param>
        /// <param name="updatedDateFrom"></param>
        /// <param name="updatedDateTo"></param>
        /// <param name="citizenIdentityNo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;ContactListDTO&gt;</returns>
        List<ContactListDTO> GetContactList (string contactNo, string firstNameTH, string lastNameTH, string phoneNumber, DateTime? createdDateFrom, DateTime? createdDateTo, DateTime? updatedDateFrom, DateTime? updatedDateTo, string citizenIdentityNo, int? page, int? pageSize, string sortBy, bool? ascending);
        /// <summary>
        /// Get ข้อมูล Contact เดิมที่มีอยู่ในระบบ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ContactSimilarPopupDTO</returns>
        ContactSimilarPopupDTO GetContactSimilar (ContactDTO input);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ContactsApi : IContactsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ContactsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ContactsApi(String basePath)
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
        /// สร้าง Contact 
        /// </summary>
        /// <param name="input"></param> 
        /// <param name="similarContactID"></param> 
        /// <param name="fromVisitorID"></param> 
        /// <returns>ContactDTO</returns>            
        public ContactDTO CreateContact (ContactDTO input, Guid? similarContactID, Guid? fromVisitorID)
        {
            
    
            var path = "/api/Contacts";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (similarContactID != null) queryParams.Add("similarContactID", ApiClient.ParameterToString(similarContactID)); // query parameter
 if (fromVisitorID != null) queryParams.Add("fromVisitorID", ApiClient.ParameterToString(fromVisitorID)); // query parameter
                                    postBody = ApiClient.Serialize(input); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateContact: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateContact: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactDTO) ApiClient.Deserialize(response.Content, typeof(ContactDTO), response.Headers);
        }
    
        /// <summary>
        /// สร้างที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน) 
        /// </summary>
        /// <param name="contactID"></param> 
        /// <param name="input"></param> 
        /// <returns>ContactAddressDTO</returns>            
        public ContactAddressDTO CreateContactAddress (Guid? contactID, ContactAddressDTO input)
        {
            
            // verify the required parameter 'contactID' is set
            if (contactID == null) throw new ApiException(400, "Missing required parameter 'contactID' when calling CreateContactAddress");
            
    
            var path = "/api/Contacts/{contactID}/Addresses";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "contactID" + "}", ApiClient.ParameterToString(contactID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateContactAddress: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateContactAddress: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactAddressDTO) ApiClient.Deserialize(response.Content, typeof(ContactAddressDTO), response.Headers);
        }
    
        /// <summary>
        /// ลบ Contact 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteContact (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteContact");
            
    
            var path = "/api/Contacts/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteContact: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteContact: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// ลบที่อยู่ที่ติดต่อได้ 
        /// </summary>
        /// <param name="contactID"></param> 
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteContactAddress (Guid? contactID, Guid? id)
        {
            
            // verify the required parameter 'contactID' is set
            if (contactID == null) throw new ApiException(400, "Missing required parameter 'contactID' when calling DeleteContactAddress");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteContactAddress");
            
    
            var path = "/api/Contacts/{contactID}/Addresses/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "contactID" + "}", ApiClient.ParameterToString(contactID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteContactAddress: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DeleteContactAddress: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// แก้ไข Contact 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>ContactDTO</returns>            
        public ContactDTO EditContact (Guid? id, ContactDTO input)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditContact");
            
    
            var path = "/api/Contacts/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling EditContact: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditContact: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactDTO) ApiClient.Deserialize(response.Content, typeof(ContactDTO), response.Headers);
        }
    
        /// <summary>
        /// แก้ไขที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน) 
        /// </summary>
        /// <param name="contactID"></param> 
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>ContactAddressDTO</returns>            
        public ContactAddressDTO EditContactAddress (Guid? contactID, Guid? id, ContactAddressDTO input)
        {
            
            // verify the required parameter 'contactID' is set
            if (contactID == null) throw new ApiException(400, "Missing required parameter 'contactID' when calling EditContactAddress");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditContactAddress");
            
    
            var path = "/api/Contacts/{contactID}/Addresses/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "contactID" + "}", ApiClient.ParameterToString(contactID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling EditContactAddress: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EditContactAddress: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactAddressDTO) ApiClient.Deserialize(response.Content, typeof(ContactAddressDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Contact 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>ContactDTO</returns>            
        public ContactDTO GetContact (Guid? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetContact");
            
    
            var path = "/api/Contacts/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetContact: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContact: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactDTO) ApiClient.Deserialize(response.Content, typeof(ContactDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูลที่อยู่ 
        /// </summary>
        /// <param name="contactID"></param> 
        /// <param name="id"></param> 
        /// <returns>ContactAddressDTO</returns>            
        public ContactAddressDTO GetContactAddress (Guid? contactID, Guid? id)
        {
            
            // verify the required parameter 'contactID' is set
            if (contactID == null) throw new ApiException(400, "Missing required parameter 'contactID' when calling GetContactAddress");
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetContactAddress");
            
    
            var path = "/api/Contacts/{contactID}/Addresses/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "contactID" + "}", ApiClient.ParameterToString(contactID));
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactAddress: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactAddress: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactAddressDTO) ApiClient.Deserialize(response.Content, typeof(ContactAddressDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูลที่อยู่ทั้งหมด 
        /// </summary>
        /// <param name="contactID"></param> 
        /// <returns>AddressDTO</returns>            
        public AddressDTO GetContactAddressList (Guid? contactID)
        {
            
            // verify the required parameter 'contactID' is set
            if (contactID == null) throw new ApiException(400, "Missing required parameter 'contactID' when calling GetContactAddressList");
            
    
            var path = "/api/Contacts/{contactID}/Addresses";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "contactID" + "}", ApiClient.ParameterToString(contactID));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactAddressList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactAddressList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (AddressDTO) ApiClient.Deserialize(response.Content, typeof(AddressDTO), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล List Contact 
        /// </summary>
        /// <param name="contactNo"></param> 
        /// <param name="firstNameTH"></param> 
        /// <param name="lastNameTH"></param> 
        /// <param name="phoneNumber"></param> 
        /// <param name="createdDateFrom"></param> 
        /// <param name="createdDateTo"></param> 
        /// <param name="updatedDateFrom"></param> 
        /// <param name="updatedDateTo"></param> 
        /// <param name="citizenIdentityNo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;ContactListDTO&gt;</returns>            
        public List<ContactListDTO> GetContactList (string contactNo, string firstNameTH, string lastNameTH, string phoneNumber, DateTime? createdDateFrom, DateTime? createdDateTo, DateTime? updatedDateFrom, DateTime? updatedDateTo, string citizenIdentityNo, int? page, int? pageSize, string sortBy, bool? ascending)
        {
            
    
            var path = "/api/Contacts";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (contactNo != null) queryParams.Add("contactNo", ApiClient.ParameterToString(contactNo)); // query parameter
 if (firstNameTH != null) queryParams.Add("firstNameTH", ApiClient.ParameterToString(firstNameTH)); // query parameter
 if (lastNameTH != null) queryParams.Add("lastNameTH", ApiClient.ParameterToString(lastNameTH)); // query parameter
 if (phoneNumber != null) queryParams.Add("phoneNumber", ApiClient.ParameterToString(phoneNumber)); // query parameter
 if (createdDateFrom != null) queryParams.Add("createdDateFrom", ApiClient.ParameterToString(createdDateFrom)); // query parameter
 if (createdDateTo != null) queryParams.Add("createdDateTo", ApiClient.ParameterToString(createdDateTo)); // query parameter
 if (updatedDateFrom != null) queryParams.Add("updatedDateFrom", ApiClient.ParameterToString(updatedDateFrom)); // query parameter
 if (updatedDateTo != null) queryParams.Add("updatedDateTo", ApiClient.ParameterToString(updatedDateTo)); // query parameter
 if (citizenIdentityNo != null) queryParams.Add("citizenIdentityNo", ApiClient.ParameterToString(citizenIdentityNo)); // query parameter
 if (page != null) queryParams.Add("page", ApiClient.ParameterToString(page)); // query parameter
 if (pageSize != null) queryParams.Add("pageSize", ApiClient.ParameterToString(pageSize)); // query parameter
 if (sortBy != null) queryParams.Add("sortBy", ApiClient.ParameterToString(sortBy)); // query parameter
 if (ascending != null) queryParams.Add("ascending", ApiClient.ParameterToString(ascending)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "Bearer" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ContactListDTO>) ApiClient.Deserialize(response.Content, typeof(List<ContactListDTO>), response.Headers);
        }
    
        /// <summary>
        /// Get ข้อมูล Contact เดิมที่มีอยู่ในระบบ 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>ContactSimilarPopupDTO</returns>            
        public ContactSimilarPopupDTO GetContactSimilar (ContactDTO input)
        {
            
    
            var path = "/api/Contacts/SimilarContacts";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactSimilar: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetContactSimilar: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ContactSimilarPopupDTO) ApiClient.Deserialize(response.Content, typeof(ContactSimilarPopupDTO), response.Headers);
        }
    
    }
}
