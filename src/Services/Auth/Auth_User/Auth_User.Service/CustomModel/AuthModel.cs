using System;
using System.Collections.Generic;

namespace Auth_User.Services.CustomModel
{
    public partial class AutorizeResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public AutorizeDataJWTReturnObject AutorizeData { get; set; }
        public List<UserProject> UserProjects { get; set; }
    }

    public partial class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AppCode { get; set; }
    }

    public class AutorizeDataJWT
    {
        public bool LoginResult { get; set; }
        public string LoginResultMessage { get; set; }
        public string UserPrincipalName { get; set; }
        public string DomainUserName { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string Division { get; set; }

        public string Token { get; set; }

        public DateTime? AccountExpirationDate { get; set; }
        public DateTime? LastLogon { get; set; }

        public string AuthenticationProvider { get; set; }
        public string SysUserId { get; set; }
        public string SysUserData { get; set; }
        public string SysUserRoles { get; set; }
        public string SysAppCode { get; set; }
        public string AppUserRole { get; set; }
        public string UserProject { get; set; }
        public string UserApp { get; set; }

    }
    public class AutorizeDataJWTReturnObject
    {
        public bool LoginResult { get; set; }
        public string LoginResultMessage { get; set; }
        public string UserPrincipalName { get; set; }
        public string DomainUserName { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string Division { get; set; }

        public string Token { get; set; }

        public DateTime? AccountExpirationDate { get; set; }
        public DateTime? LastLogon { get; set; }

        public string AuthenticationProvider { get; set; }
        public string SysUserId { get; set; }
        public UserModel SysUserData { get; set; }
        public vwUserRole SysUserRoles { get; set; }
        public string SysAppCode { get; set; }
        public string AppUserRole { get; set; }
        public List<UserProjectType> UserProject { get; set; }
        public List<vwUserApp> UserApp { get; set; }
    }

    public class UserModel
    {
        public vwUser User { get; set; }
        public string TitleMsg { get; set; }
        public string RedirectMsg { get; set; }
        public string TypeMsg { get; set; }

    }

    public partial class vwUserRole
    {
        public int? ID { get; set; }
        public int UserID { get; set; }
        public string UserGUID { get; set; }
        public string UserName { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
        public int RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string AssignType { get; set; }
        public string SourceType { get; set; }
        public string Remark { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class vwUser
    {
        public int UserID { get; set; }
        public string UserGUID { get; set; }
        public string UserName { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }
        public string FullCodeName { get; set; }
        public string UserNameLogin { get; set; }
        public string RGUID { get; set; }
    }

    public partial class vwUserApp
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string UserGUID { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int AppID { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string AssignType { get; set; }
        public string SourceType { get; set; }
        public string PositionName { get; set; }
        public string Remark { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public partial class UserProjectType
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string AssignType { get; set; }
        public string SourceType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Remark { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string producttypecate { get; set; }
    }

    public partial class UserProject
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string AssignType { get; set; }
        public string SourceType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Remark { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UserToken
    {
        public string Token { get; set; }
    }

    public partial class UserTokenResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UserTokenResultReturnObject data { get; set; }
    }

    public class UserTokenResultReturnObject
    {
        public Guid? userGUID { get; set; }
        public string userName { get; set; }
        public string empCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string positionCode { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public string divisionCode { get; set; }
        public string division { get; set; }
        public string email { get; set; }
        public string leaderCode { get; set; }
    }

    public class GenerateUserToken
    {
        public Guid? UserGUID { get; set; }
        public string AppCode { get; set; }
    }

    public partial class GenerateUserTokenResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public GenerateUserTokenResultReturnObject data { get; set; }
    }

    public class GenerateUserTokenResultReturnObject
    {
        public bool success { get; set; }
        public string messge { get; set; }
        public int userID { get; set; }
        public string userGUID { get; set; }
        public string empCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string token { get; set; }
        public DateTime? expireDate { get; set; }

    }
}
