using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.MST
{
    public class dbqVPUser
    {
        public Guid? ProjectID { get; set; } 
        public string ProjectNo { get; set; } 
        public string ProjectName { get; set; } 
        public string BG { get; set; } 
        public string SubBG { get; set; } 
        public string Department { get; set; } 
        public string UserGUID { get; set; } 
        public string EmpCode { get; set; } 
        public string TitleName { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public string NickName { get; set; } 
        public string Mobile { get; set; } 
        public Guid? CRMID { get; set; } 
    }
}
