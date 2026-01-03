using Base.DTOs.SAL;
using Database.Models;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using FileStorage;
using Microsoft.Extensions.Configuration;
using static Database.Models.DbQueries.DBQueryParam;
using static Base.DTOs.SAL.UnitInfoListDTO;
using System.Data.SqlClient;
using Database.Models.DbQueries;
using Base.DTOs;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using PRJ_UnitInfos.Params.Outputs;
using PRJ_UnitInfos.Params.Filters;
using Dapper;
using Database.Models.DbQueries.SAL;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PRJ_UnitInfos.Services;
using Common.Helper.Logging;
using Newtonsoft.Json;
using Base.DTOs.PRJ;
using Confluent.Kafka;
using NPOI.XWPF.UserModel;

namespace PRJ_UnitInfos.Services
{
    public class HomeInspectionService : IHomeInspectionService
    {
        private readonly DatabaseContext DB; 
        public LogModel logModel { get; set; }
        public HomeInspectionService(DatabaseContext db)
        {
            logModel = new LogModel("HomeInspectionService", null);
            DB = db;
            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL"); 
        }
        public async Task<List<PublicAppointmentDTO>> GetPublicAppointmentCalendar(PublicAppointmentFilter input, Guid? usrID, CancellationToken cancellationToken = default)
        {
            List<PublicAppointmentDTO> result = new List<PublicAppointmentDTO>();

            var empcode = await DB.Users.AsNoTracking().Where(o => o.ID == usrID).Select(o => o.EmployeeNo).FirstOrDefaultAsync(cancellationToken);

            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");

            var requestUrl = $"{defectApiUrl}PublicAppointment/GetAppointmentCalendar";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
                client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);
                if (!string.IsNullOrEmpty(empcode))
                {
                    client.DefaultRequestHeaders.Add("EmpCode", empcode);
                }
                var body = JsonConvert.SerializeObject(input);
                using (var stringContent = new StringContent(body, System.Text.Encoding.UTF8, "application/json"))
                using (var Response = await client.PostAsync(requestUrl, stringContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingResult Result = JsonConvert.DeserializeObject<DefecttrackingResult>(resultObj);
                        if (Result?.data?.sourceData != null)
                            result = Result?.data?.sourceData.OrderBy(x=>x.appointmentDatetime).ToList();
                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingResult Result = JsonConvert.DeserializeObject<DefecttrackingResult>(resultObj);
                        string message = Result.message;
                        throw new UnauthorizedException(message);
                    }
                }
                return result;
            }
        }
        public async Task<bool> CreateAppointment(CreateAppointmentDTO input, Guid? usrID, CancellationToken cancellationToken = default)
        {
            var emp = await DB.Users.AsNoTracking().Where(o => o.ID == usrID).FirstOrDefaultAsync(cancellationToken) ?? new User();
            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");
            var requestUrl = $"{defectApiUrl}PublicAppointment/CreateAppointment";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
                client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);
                input.createdBy = emp.EmployeeNo;
                input.createdByName = $"{emp.Title}{emp.DisplayName}";

                var body = JsonConvert.SerializeObject(input);
                using (var stringContent = new StringContent(body, System.Text.Encoding.UTF8, "application/json"))
                using (var Response = await client.PostAsync(requestUrl, stringContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingCreateResult Result = JsonConvert.DeserializeObject<DefecttrackingCreateResult>(resultObj);
                        string message = Result.message;
                        var errMsg = new ErrorMessage();
                        ValidateException ex = new ValidateException();
                        ex.AddError("ERR9999", message, 1);
                        throw ex;
                    }
                }
            }
        }
        public async Task<bool> UpdateAppointment(CreateAppointmentDTO body, Guid? usrID, CancellationToken cancellationToken = default)
        {
            var emp = await DB.Users.AsNoTracking().Where(o => o.ID == usrID).FirstOrDefaultAsync(cancellationToken) ?? new User();
            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");
            var requestUrl = $"{defectApiUrl}PublicAppointment/UpdateAppointment";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
                client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);
                body.createdBy = emp.EmployeeNo;
                body.createdByName = $"{emp.Title}{emp.DisplayName}";
                var bodyStr = JsonConvert.SerializeObject(body);
                using (var stringContent = new StringContent(bodyStr, System.Text.Encoding.UTF8, "application/json"))
                using (var Response = await client.PutAsync(requestUrl, stringContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingCreateResult Result = JsonConvert.DeserializeObject<DefecttrackingCreateResult>(resultObj);
                        string message = Result.message;
                        var errMsg = new ErrorMessage();
                        ValidateException ex = new ValidateException();
                        ex.AddError("ERR9999", message, 1);
                        throw ex;
                    }
                }
            }
        }
        public async Task<List<PublicAppointmentListDTO>> GetPublicAppointment(PublicAppointmentFilter input, Guid? usrID, CancellationToken cancellationToken = default)
        {
            List<PublicAppointmentListDTO> result = new List<PublicAppointmentListDTO>();

            var empcode = await DB.Users.AsNoTracking().Where(o => o.ID == usrID).Select(o => o.EmployeeNo).FirstOrDefaultAsync(cancellationToken);

            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");

            var requestUrl = $"{defectApiUrl}PublicAppointment/GetAppointments";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
                client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);
                if (!string.IsNullOrEmpty(empcode))
                {
                    client.DefaultRequestHeaders.Add("EmpCode", empcode);
                }
                var body = JsonConvert.SerializeObject(input);
                using (var stringContent = new StringContent(body, System.Text.Encoding.UTF8, "application/json"))
                using (var Response = await client.PostAsync(requestUrl, stringContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingListResult Result = JsonConvert.DeserializeObject<DefecttrackingListResult>(resultObj);
                        if (Result?.data?.sourceData != null)
                            result = Result?.data?.sourceData;
                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingListResult Result = JsonConvert.DeserializeObject<DefecttrackingListResult>(resultObj);
                        string message = Result.message;
                        throw new UnauthorizedException(message);
                    }
                }
                return result;
            }
        }
        public async Task<bool> DeleteAppointment(DeleteDefecttrackingInput body, Guid? usrID, CancellationToken cancellationToken = default)
        {
            var emp = await DB.Users.AsNoTracking().Where(o => o.ID == usrID).FirstOrDefaultAsync(cancellationToken) ?? new User();
            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");
            var requestUrl = $"{defectApiUrl}PublicAppointment/DeleteAppointment/{body.appointmentID}?appointmentID={body.appointmentID}&deletedBy={emp.EmployeeNo}&deletedByName={emp.Title}{emp.DisplayName}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
                client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);  
                using (var Response = await client.DeleteAsync(requestUrl))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        string resultObj = await Response.Content.ReadAsStringAsync();
                        DefecttrackingCreateResult Result = JsonConvert.DeserializeObject<DefecttrackingCreateResult>(resultObj);
                        string message = Result.message;
                        var errMsg = new ErrorMessage();
                        ValidateException ex = new ValidateException();
                        ex.AddError("ERR9999", message, 1);
                        throw ex;
                    }
                }
            }
        }
        public async Task<List<SeDTO>> GetSEByProjectDropdown(string projectCode)
        {
            List<SeDTO> result = new List<SeDTO>();
             
            var client = new HttpClient();

            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");


            client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
            client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret); 

            var requestUrl = $"{defectApiUrl}PublicAppointment/SearchDropdownSEByProject";
            var body = JsonConvert.SerializeObject(new { projectCode =  projectCode });
            using (var stringContent = new StringContent(body, System.Text.Encoding.UTF8, "application/json"))
            using (var Response = await client.PostAsync(requestUrl, stringContent))
            {
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string resultObj = await Response.Content.ReadAsStringAsync();
                    SEListResult Result = JsonConvert.DeserializeObject<SEListResult>(resultObj);
                    if (Result?.data?.sourceData != null)
                        result = Result?.data?.sourceData;
                }
                else
                {
                    string resultObj = await Response.Content.ReadAsStringAsync();
                    SEListResult Result = JsonConvert.DeserializeObject<SEListResult>(resultObj);
                    string message = Result.message;
                    throw new UnauthorizedException(message);
                }
            }
            return result;
        }

        public async Task<List<InspectionTypeDTO>> GetMasterInspectionType()
        {
            List<InspectionTypeDTO> result = new List<InspectionTypeDTO>();

            var client = new HttpClient();

            string defectApiUrl = Environment.GetEnvironmentVariable("DefectApiUrl");
            string defectPublicAPIKey = Environment.GetEnvironmentVariable("DefectPublic_API_Key");
            string defectPublicAPISecret = Environment.GetEnvironmentVariable("DefectPublic_API_Secret");


            client.DefaultRequestHeaders.Add("Public_API_Key", defectPublicAPIKey);
            client.DefaultRequestHeaders.Add("Public_API_Secret", defectPublicAPISecret);
            //client.DefaultRequestHeaders.Add("EmpCode", empcode);

            var requestUrl = $"{defectApiUrl}PublicAppointment/GetMasterInspectionType";

            using (var Response = await client.GetAsync(requestUrl))
            {

                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string resultObj = await Response.Content.ReadAsStringAsync();
                    InspectionTypeListResult Result = JsonConvert.DeserializeObject<InspectionTypeListResult>(resultObj);
                    if (Result?.data != null)
                        result = Result?.data;
                }
                else
                {
                    string resultObj = await Response.Content.ReadAsStringAsync();
                    InspectionTypeListResult Result = JsonConvert.DeserializeObject<InspectionTypeListResult>(resultObj);
                    string message = Result.message;
                    throw new UnauthorizedException(message);
                }
            }
            
            return result;
        }

    }
}
