using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Apperel360.Domain.Models
{
    public class AccountModel
    {
    }
    public class LoginModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string MobileNo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string IpAddress { get; set; } = string.Empty;

    }
    public class AdminLoginModel
    {

        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;

    }
    public class VerificationModel
    {
        [Required(ErrorMessage = "Required")]
        public string MobileNo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string OTP { get; set; } = string.Empty;
    }
    public class RegistrationModel : BaseRequest
    {
        public Guid UserID { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string MobileNo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public string AdharCardNo { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string CreatedByIP { get; set; } = string.Empty;
        public int Id { get; set; }
    }
    public class LoginViewModel
    {
        public Guid UserID { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
    public class UserViewModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string ProfilePicPath { get; set; } = string.Empty;
        public string LastLogin { get; set; } = string.Empty;
        public string LastChanged { get; set; } = string.Empty;
        public int IsFirstLogin { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; } = string.Empty;
        public int IsSucess { get; set; }
        public DateTime? OTPSendDate { get; set; }
        public int OtpCount { get; set; }
        public string OTP { get; set; } = string.Empty;
        public int Id { get; set; }
    }
    public class ForgotViewModel
    {
        public Guid UserID { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public bool IsMobileVarified { get; set; }
        public bool IsEmailVarified { get; set; }
        public DateTime? OTPSendDate { get; set; }
        public int OtpCount { get; set; }
        public string OTP { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
    public class UserMappingViewModel
    {
        public Guid MappedUserId { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;
        public string? ProfilePicPath { get; set; }

    }
    public class UserMappingModel
    {
        public Guid EmployeeUserId { get; set; }
        public Guid CustomerUserId { get; set; }
    }

    public class UserProfileRequestModel
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? ShopName { get; set; }
        public string? GstNo { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string UserType { get; set; }
        public string PurchaseQty { get; set; }
    }

    public class ProfileImageUploadModel
    {
        public Guid UserId { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string PicturePath { get; set; } = string.Empty;
    }
    public class UserProfileResponseModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? ShopName { get; set; }
        public string? GstNo { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string UserType { get; set; }
        public string PurchaseQty { get; set; }
        public int IsSucess { get; set; }
    }
    public class CustomerUsersMappingRequestModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class CustomerUsersMappedViewModel
    {
        public Guid CustomerUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNo { get; set; }
        public Guid EmployeeUserId { get; set; }
        public string Status { get; set; }
    }
    public static class PoliciesModel
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string Customer = "Customer";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy EmployeePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Employee).Build();
        }
        public static AuthorizationPolicy CustomerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Customer).Build();
        }
    }
}
