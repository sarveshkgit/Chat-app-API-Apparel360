using Apperel360.Domain.Models;

namespace Apperel360.Domain.Interfaces
{
    public interface IAccountRepository
    {
        UserViewModel GetLoginDetail(string username, string IpAddress, int RoleId);
        UserViewModel GetAdminLogin(string username, string password, string IpAddress);
        UserViewModel Registration(RegistrationModel registrationModel);
        UserViewModel GetUserDetail(Guid userId);
        int UpdateUserStatus(Guid userId, string Status);
        UserViewModel VerifyUserDetail(string mobileNo, string OTP);
        List<UserMappingViewModel> GetUsers(Guid UserID);
        int UpdateOtpCount(Guid userId, string mobileNo, string OTP);
        int ChangePassword(int flag, long userId, string password);
        int LoginLog(long userId, string username, bool loginStatus, string errorMsg, string ipAddress);

        UserViewModel AddEmployeeUser(RegistrationModel registrationModel);
        UserViewModel UpdateEmployeeUser(RegistrationModel registrationModel);
        List<UserViewModel> GetEmployeeUsersList();
        UserViewModel GetEmployeeUserById(int Id);
        long Delete(int ID);
        List<UserViewModel> GetCustomerUsersList();
        int UsersMapping(UserMappingModel viewModel);
        List<CustomerUsersMappedViewModel> GetCustomerUsersMappedList(CustomerUsersMappingRequestModel model);
        UserProfileResponseModel GetUserProfileByUserId(Guid userId);
        UserProfileResponseModel UpdateUserProfile(UserProfileRequestModel model);
        UserProfileResponseModel UpdateProfileImage(ProfileImageUploadModel model);
    }
}
