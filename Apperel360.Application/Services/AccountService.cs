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

        public UserViewModel GetAdminLogin(AdminLoginModel login)
        {
            var userlogin = _accountRepository.GetAdminLogin(login.UserName, login.Password, login.IpAddress);
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
                EmailId = userlogin.EmailId,
                ProfilePicPath = userlogin.ProfilePicPath,
                LastLogin = userlogin.LastLogin,
                LastChanged = userlogin.LastChanged,
                IsActive = userlogin.IsActive
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
        public int UpdateUserStatus(Guid userId, string Status)
        {
            return _accountRepository.UpdateUserStatus(userId, Status);
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

        public UserViewModel AddEmployeeUser(RegistrationModel registrationModel)
        {
            return _accountRepository.AddEmployeeUser(registrationModel);
        }

        public UserViewModel UpdateEmployeeUser(RegistrationModel registrationModel)
        {
            return _accountRepository.UpdateEmployeeUser(registrationModel);
        }

        public List<UserViewModel> GetEmployeeUsersList()
        {
            return _accountRepository.GetEmployeeUsersList();
        }

        public UserViewModel GetEmployeeUserById(int Id)
        {
            return _accountRepository.GetEmployeeUserById(Id);
        }
        public long Delete(int ID)
        {
            return _accountRepository.Delete(ID);
        }
        public List<UserViewModel> GetCustomerUsersList()
        {
            return _accountRepository.GetCustomerUsersList();
        }
        public int UsersMapping(UserMappingModel viewModel)
        {
            return _accountRepository.UsersMapping(viewModel);
        }
        public List<CustomerUsersMappedViewModel> GetCustomerUsersMappedList(CustomerUsersMappingRequestModel model)
        {
            return _accountRepository.GetCustomerUsersMappedList(model);
        }

        public UserProfileResponseModel GetUserProfileByUserId(Guid userId)
        {
            return _accountRepository.GetUserProfileByUserId(userId);
        }
        public UserProfileResponseModel UpdateUserProfile(UserProfileRequestModel model)
        {
            return _accountRepository.UpdateUserProfile(model);
        }

        public UserProfileResponseModel UpdateProfileImage(ProfileImageUploadModel model)
        {
            return _accountRepository.UpdateProfileImage(model);
        }
    }
}
