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
    public interface ICompaniesApi
    {
        /// <summary>
        /// สร้างบริษัท 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>CompanyDTO</returns>
        CompanyDTO CreateCompany(CompanyDTO input);
        /// <summary>
        /// ลบบริษัท 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteCompany(Guid? id);
        /// <summary>
        /// แก้ไขบริษัท 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>CompanyDTO</returns>
        CompanyDTO EditCompany(Guid? id, CompanyDTO input);
        /// <summary>
        /// ข้อมูลบริษัท 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CompanyDTO</returns>
        CompanyDTO GetCompany(Guid? id);
        /// <summary>
        /// ลิสของบริษัท Dropdown 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns>List&lt;CompanyDropdownDTO&gt;</returns>
        List<CompanyDropdownDTO> GetCompanyDropdownList(string code, string name);
        /// <summary>
        /// ลิสของบริษัท 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nameTH"></param>
        /// <param name="nameEN"></param>
        /// <param name="taxID"></param>
        /// <param name="addressTH"></param>
        /// <param name="addressEN"></param>
        /// <param name="buildingTH"></param>
        /// <param name="buildingEN"></param>
        /// <param name="soiTH"></param>
        /// <param name="soiEN"></param>
        /// <param name="roadTH"></param>
        /// <param name="roadEN"></param>
        /// <param name="subDistrictID"></param>
        /// <param name="districtID"></param>
        /// <param name="provinceID"></param>
        /// <param name="postalCode"></param>
        /// <param name="telephone"></param>
        /// <param name="fax"></param>
        /// <param name="website"></param>
        /// <param name="sapCompanyID"></param>
        /// <param name="nameTHOld"></param>
        /// <param name="nameENOld"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="updatedTo"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        /// <returns>List&lt;CompanyDTO&gt;</returns>
        List<CompanyDTO> GetCompanyList(string code, string nameTH, string nameEN, string taxID, string addressTH, string addressEN, string buildingTH, string buildingEN, string soiTH, string soiEN, string roadTH, string roadEN, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string postalCode, string telephone, string fax, string website, string sapCompanyID, string nameTHOld, string nameENOld, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class CompaniesApi : ICompaniesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public CompaniesApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient;
            else
                this.ApiClient = apiClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CompaniesApi(String basePath)
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
        /// สร้างบริษัท 
        /// </summary>
        /// <param name="input"></param> 
        /// <returns>CompanyDTO</returns>            
        public CompanyDTO CreateCompany(CompanyDTO input)
        {


            var path = "/api/Companies";
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
                throw new ApiException((int)response.StatusCode, "Error calling CreateCompany: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling CreateCompany: " + response.ErrorMessage, response.ErrorMessage);

            return (CompanyDTO)ApiClient.Deserialize(response.Content, typeof(CompanyDTO), response.Headers);
        }

        /// <summary>
        /// ลบบริษัท 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>            
        public void DeleteCompany(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling DeleteCompany");


            var path = "/api/Companies/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCompany: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling DeleteCompany: " + response.ErrorMessage, response.ErrorMessage);

            return;
        }

        /// <summary>
        /// แก้ไขบริษัท 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="input"></param> 
        /// <returns>CompanyDTO</returns>            
        public CompanyDTO EditCompany(Guid? id, CompanyDTO input)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EditCompany");


            var path = "/api/Companies/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling EditCompany: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling EditCompany: " + response.ErrorMessage, response.ErrorMessage);

            return (CompanyDTO)ApiClient.Deserialize(response.Content, typeof(CompanyDTO), response.Headers);
        }

        /// <summary>
        /// ข้อมูลบริษัท 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>CompanyDTO</returns>            
        public CompanyDTO GetCompany(Guid? id)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetCompany");


            var path = "/api/Companies/{id}";
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCompany: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCompany: " + response.ErrorMessage, response.ErrorMessage);

            return (CompanyDTO)ApiClient.Deserialize(response.Content, typeof(CompanyDTO), response.Headers);
        }

        /// <summary>
        /// ลิสของบริษัท Dropdown 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="name"></param> 
        /// <returns>List&lt;CompanyDropdownDTO&gt;</returns>            
        public List<CompanyDropdownDTO> GetCompanyDropdownList(string code, string name)
        {


            var path = "/api/Companies/DropdownList";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (name != null) queryParams.Add("name", ApiClient.ParameterToString(name)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetCompanyDropdownList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCompanyDropdownList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<CompanyDropdownDTO>)ApiClient.Deserialize(response.Content, typeof(List<CompanyDropdownDTO>), response.Headers);
        }

        /// <summary>
        /// ลิสของบริษัท 
        /// </summary>
        /// <param name="code"></param> 
        /// <param name="nameTH"></param> 
        /// <param name="nameEN"></param> 
        /// <param name="taxID"></param> 
        /// <param name="addressTH"></param> 
        /// <param name="addressEN"></param> 
        /// <param name="buildingTH"></param> 
        /// <param name="buildingEN"></param> 
        /// <param name="soiTH"></param> 
        /// <param name="soiEN"></param> 
        /// <param name="roadTH"></param> 
        /// <param name="roadEN"></param> 
        /// <param name="subDistrictID"></param> 
        /// <param name="districtID"></param> 
        /// <param name="provinceID"></param> 
        /// <param name="postalCode"></param> 
        /// <param name="telephone"></param> 
        /// <param name="fax"></param> 
        /// <param name="website"></param> 
        /// <param name="sapCompanyID"></param> 
        /// <param name="nameTHOld"></param> 
        /// <param name="nameENOld"></param> 
        /// <param name="updatedBy"></param> 
        /// <param name="updatedFrom"></param> 
        /// <param name="updatedTo"></param> 
        /// <param name="page"></param> 
        /// <param name="pageSize"></param> 
        /// <param name="sortBy"></param> 
        /// <param name="ascending"></param> 
        /// <returns>List&lt;CompanyDTO&gt;</returns>            
        public List<CompanyDTO> GetCompanyList(string code, string nameTH, string nameEN, string taxID, string addressTH, string addressEN, string buildingTH, string buildingEN, string soiTH, string soiEN, string roadTH, string roadEN, Guid? subDistrictID, Guid? districtID, Guid? provinceID, string postalCode, string telephone, string fax, string website, string sapCompanyID, string nameTHOld, string nameENOld, string updatedBy, DateTime? updatedFrom, DateTime? updatedTo, int? page, int? pageSize, string sortBy, bool? ascending)
        {


            var path = "/api/Companies";
            path = path.Replace("{format}", "json");

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (code != null) queryParams.Add("code", ApiClient.ParameterToString(code)); // query parameter
            if (nameTH != null) queryParams.Add("nameTH", ApiClient.ParameterToString(nameTH)); // query parameter
            if (nameEN != null) queryParams.Add("nameEN", ApiClient.ParameterToString(nameEN)); // query parameter
            if (taxID != null) queryParams.Add("taxID", ApiClient.ParameterToString(taxID)); // query parameter
            if (addressTH != null) queryParams.Add("addressTH", ApiClient.ParameterToString(addressTH)); // query parameter
            if (addressEN != null) queryParams.Add("addressEN", ApiClient.ParameterToString(addressEN)); // query parameter
            if (buildingTH != null) queryParams.Add("buildingTH", ApiClient.ParameterToString(buildingTH)); // query parameter
            if (buildingEN != null) queryParams.Add("buildingEN", ApiClient.ParameterToString(buildingEN)); // query parameter
            if (soiTH != null) queryParams.Add("soiTH", ApiClient.ParameterToString(soiTH)); // query parameter
            if (soiEN != null) queryParams.Add("soiEN", ApiClient.ParameterToString(soiEN)); // query parameter
            if (roadTH != null) queryParams.Add("roadTH", ApiClient.ParameterToString(roadTH)); // query parameter
            if (roadEN != null) queryParams.Add("roadEN", ApiClient.ParameterToString(roadEN)); // query parameter
            if (subDistrictID != null) queryParams.Add("subDistrictID", ApiClient.ParameterToString(subDistrictID)); // query parameter
            if (districtID != null) queryParams.Add("districtID", ApiClient.ParameterToString(districtID)); // query parameter
            if (provinceID != null) queryParams.Add("provinceID", ApiClient.ParameterToString(provinceID)); // query parameter
            if (postalCode != null) queryParams.Add("postalCode", ApiClient.ParameterToString(postalCode)); // query parameter
            if (telephone != null) queryParams.Add("telephone", ApiClient.ParameterToString(telephone)); // query parameter
            if (fax != null) queryParams.Add("fax", ApiClient.ParameterToString(fax)); // query parameter
            if (website != null) queryParams.Add("website", ApiClient.ParameterToString(website)); // query parameter
            if (sapCompanyID != null) queryParams.Add("sapCompanyID", ApiClient.ParameterToString(sapCompanyID)); // query parameter
            if (nameTHOld != null) queryParams.Add("nameTHOld", ApiClient.ParameterToString(nameTHOld)); // query parameter
            if (nameENOld != null) queryParams.Add("nameENOld", ApiClient.ParameterToString(nameENOld)); // query parameter
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
                throw new ApiException((int)response.StatusCode, "Error calling GetCompanyList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetCompanyList: " + response.ErrorMessage, response.ErrorMessage);

            return (List<CompanyDTO>)ApiClient.Deserialize(response.Content, typeof(List<CompanyDTO>), response.Headers);
        }

    }
}
