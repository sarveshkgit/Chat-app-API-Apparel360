using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Interfaces
{
    public interface IAccountService
    {
        UserViewModel GetLoginDetail(LoginModel login);
        UserViewModel Registration(RegistrationModel registrationModel);
        UserViewModel GetUserDetail(Guid userId);
        UserViewModel GetAdminLogin(AdminLoginModel login);
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
        int UsersMapping(UserMappingModel vew);
        List<CustomerUsersMappedViewModel> GetCustomerUsersMappedList(CustomerUsersMappingRequestModel model);
        UserProfileResponseModel GetUserProfileByUserId(Guid userId);
        UserProfileResponseModel UpdateUserProfile(UserProfileRequestModel model);
        UserProfileResponseModel UpdateProfileImage(ProfileImageUploadModel model);
    }
}
