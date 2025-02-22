using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Interfaces
{
    public interface IAccountService
    {
        UserViewModel GetLoginDetail(LoginModel login);
        UserViewModel Registration(RegistrationModel registrationModel);
        UserViewModel GetUserDetail(Guid userId);
        UserViewModel VerifyUserDetail(string mobileNo, string OTP);
        List<UserMappingViewModel> GetUsers(Guid UserID);
        int UpdateOtpCount(Guid userId, string mobileNo, string OTP);
        int ChangePassword(int flag, long userId, string password);
        int LoginLog(long userId, string username, bool loginStatus, string errorMsg, string ipAddress);
    }
}
