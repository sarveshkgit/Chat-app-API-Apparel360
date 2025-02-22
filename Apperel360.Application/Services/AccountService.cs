using Apperel360.Application.Interfaces;
using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public int ChangePassword(int flag, long userId, string password)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetLoginDetail(LoginModel login)
        {
            var userlogin = _accountRepository.GetLoginDetail(login.MobileNo, login.IpAddress, login.RoleId);
            if (userlogin == null)
            {
                return null;
            }
            return new UserViewModel()
            {
                UserName = userlogin.UserName,
                RoleName = userlogin.RoleName,
                UserID = userlogin.UserID,
                Name = userlogin.Name,
                MobileNo = userlogin.MobileNo,
                //EmailId = userlogin.EmailId,
                ProfilePicPath = userlogin.ProfilePicPath,
                LastLogin = userlogin.LastLogin,
                LastChanged = userlogin.LastChanged,
                IsActive = userlogin.IsActive,
                OtpCount = userlogin.OtpCount,
                OTPSendDate = userlogin.OTPSendDate,
            };
        }

        public UserViewModel GetUserDetail(Guid userId)
        {
            var data = _accountRepository.GetUserDetail(userId);
            if (data == null)
            {
                return null;
            }
            return new UserViewModel()
            {
                UserID = data.UserID,
                Name = data.Name,
                MobileNo = data.MobileNo,
                Status = data.Status
            };
        }
        public UserViewModel VerifyUserDetail(string mobileNo, string OTP)
        {
            return _accountRepository.VerifyUserDetail(mobileNo, OTP);
        }
        public List<UserMappingViewModel> GetUsers(Guid UserID)
        {
            return _accountRepository.GetUsers(UserID);

        }
        public int LoginLog(long userId, string username, bool loginStatus, string errorMsg, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public UserViewModel Registration(RegistrationModel registrationModel)
        {
            var registrationDetails = _accountRepository.Registration(registrationModel);
            if (registrationDetails == null)
            {
                return null;
            }
            return registrationDetails;
        }

        public int UpdateOtpCount(Guid userId, string mobileNo, string OTP)
        {
            return _accountRepository.UpdateOtpCount(userId, mobileNo, OTP);
        }
    }
}
