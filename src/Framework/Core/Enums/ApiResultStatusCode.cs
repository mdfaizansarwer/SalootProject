using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum ApiResultStatusCode : byte
    {
        [Display(Name = "Operation done successfully")]
        Success = 1,

        [Display(Name = "Server error occurred")]
        ServerError = 2,

        [Display(Name = "Invalid arguments")]
        BadRequest = 4,

        [Display(Name = "Not found")]
        NotFound = 8,

        [Display(Name = "Authentication error")]
        UnAuthorized = 16,

        [Display(Name = "ExpiredSecurityToken")]
        ExpiredSecurityToken = 32,

        [Display(Name = "Forbidden error (User does not have permission)")]
        Forbidden = 64,

        [Display(Name = "Error in data processing")]
        LogicError = 128,
    }
}