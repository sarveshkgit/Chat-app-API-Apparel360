using Apperel360.Domain.Models;

namespace Apperel360.Domain.Interfaces
{
    public interface IAccountRepository
    {
        UserViewModel GetLoginDetail(string username, string IpAddress, int RoleId);
        UserViewModel Registration(RegistrationModel registrationModel);
        UserViewModel GetUserDetail(Guid userId);
        UserViewModel VerifyUserDetail(string mobileNo, string OTP);
        List<UserMappingViewModel> GetUsers(Guid UserID);
        int UpdateOtpCount(Guid userId, string mobileNo, string OTP);
        int ChangePassword(int flag, long userId, string password);
        int LoginLog(long userId, string username, bool loginStatus, string errorMsg, string ipAddress);
    }
}
