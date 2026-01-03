

namespace Base.DTOs.AppAuth
{
    public class ValidateLoginDTO
    {
        public string? UserLogin { get; set; }
        public string[]? AppRoles { get; set; }
        public string? MainRole { get; set; }
        public string? UserID { get; set; }
    }
}
