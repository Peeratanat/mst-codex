using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.Auth
{
    public class IntrospecDTO
    {
        [JsonProperty("aud")]
        public string[]? Aud { get; set; }
        [JsonProperty("client_id")]
        public string? ClientID { get; set; }
        [JsonProperty("sub")]
        public string? Sub { get; set; }
        [JsonProperty("auth_time")]
        public string? AuthTime { get; set; }
        [JsonProperty("idp")]
        public string? Idp { get; set; }
        [JsonProperty("amr")]
        public string? Amr { get; set; }
        [JsonProperty("ap_empCode")]
        public string EmpCode { get; set; } = string.Empty;
        [JsonProperty("ap_firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("ap_lastName")]
        public string? LastName { get; set; }
        [JsonProperty("ap_email")]
        public string? Email { get; set; }
        [JsonProperty("ap_displayName")]
        public string? DisplayName { get; set; }
        [JsonProperty("ap_division")]
        public string? Division { get; set; }
        [JsonProperty("ap_userRole")]
        public string[]? AP_UserRole { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; } = false;
        [JsonProperty("scope")]
        public string? Scope { get; set; }
        [JsonProperty("ap_AppRoles")]
        public string[]? AP_AppRoles { get; set; } = Array.Empty<string>();
        [JsonProperty("user_id")]
        public Guid? user_id { get; set; }

        public List<UserApp> ExtraUserApps
        {
            get
            {
                if (AP_AppRoles == null || AP_AppRoles.Length == 0)
                {
                    return new List<UserApp>();
                }
                else
                {
                    int dummyCount = AP_AppRoles.Where(x => x.Contains("Dummy")).Count();
                    if (dummyCount == AP_AppRoles.Length)
                    {
                        return new List<UserApp>();
                    }

                    if (AP_AppRoles[1] != null && AP_AppRoles[1].Length > 0)
                    {
                        return JsonConvert.DeserializeObject<List<UserApp>>(AP_AppRoles[1]) ?? new List<UserApp>();
                    }
                    else
                    {
                        return new List<UserApp>();
                    }
                }
            }
        }

        public class UserApp
        {
            [JsonProperty("extraUserApps")]
            public ExtraUserApp ExtraUserApps { get; set; } = new ExtraUserApp();
            [JsonProperty("userRoles")]
            public List<AppRole> UserRoles { get; set; } = new List<AppRole>();
        }

        public class ExtraUserApp
        {
            public int AppID { get; set; }
            public string AppCode { get; set; } = string.Empty;
            public string AppName { get; set; } = string.Empty;
        }

        public class AppRole
        {
            public string RoleCode { get; set; } = string.Empty;
        }
    }
}
