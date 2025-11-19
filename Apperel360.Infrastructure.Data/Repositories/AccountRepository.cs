using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Apperel360.Infrastructure.Data.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Apperel360.Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        IDapperDbContext _dapper;
        public AccountRepository(IDapperDbContext dapper)
        {
            _dapper = dapper;
        }
        public int ChangePassword(int flag, long userId, string password)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetLoginDetail(string mobileno, string IpAddress, int RoleId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("MobileNo", mobileno, System.Data.DbType.String);
            dynamicParameters.Add("IpAddress", IpAddress, System.Data.DbType.String);
            dynamicParameters.Add("RoleId", RoleId, System.Data.DbType.Int32);

            return _dapper.ExecuteGet<UserViewModel>("proc_getLoginDetails", dynamicParameters);
        }

        public UserViewModel GetAdminLogin(string mobileno, string password, string IpAddress)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UserName", mobileno, System.Data.DbType.String);
            dynamicParameters.Add("Password", password, System.Data.DbType.String);
            dynamicParameters.Add("IpAddress", IpAddress, System.Data.DbType.String);
            return _dapper.ExecuteGet<UserViewModel>("proc_getAdminLogin", dynamicParameters);
        }

        public UserViewModel GetUserDetail(Guid userId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("userId", userId, System.Data.DbType.Guid);
            return _dapper.ExecuteGet<UserViewModel>("proc_getUserDetails", dynamicParameters);
        }

        public UserViewModel VerifyUserDetail(string mobileNo, string OTP)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("mobileNo", mobileNo, System.Data.DbType.String);
            dynamicParameters.Add("OTP", OTP, System.Data.DbType.String);
            return _dapper.ExecuteGet<UserViewModel>("proc_VerifyUserDetail", dynamicParameters);
        }

        public List<UserMappingViewModel> GetUsers(Guid UserID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("userId", UserID, System.Data.DbType.Guid);
            return _dapper.ExecuteGetAll<UserMappingViewModel>("proc_GetMappedUser", dynamicParameters);
        }

        public int UpdateUserStatus(Guid userId, string Status)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UserID", userId, System.Data.DbType.Guid);
            dynamicParameters.Add("Status", Status, System.Data.DbType.String);
            return _dapper.Execute("proc_UpdateUserStatus", dynamicParameters);
        }
        public int LoginLog(long userId, string username, bool loginStatus, string errorMsg, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public UserViewModel Registration(RegistrationModel registrationModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@roleId", registrationModel.RoleId, System.Data.DbType.Int32);
            dynamicParameters.Add("@name", registrationModel.Name, System.Data.DbType.String);
            dynamicParameters.Add("@mobileNo", registrationModel.MobileNo, System.Data.DbType.String);
            dynamicParameters.Add("@password", registrationModel.Password, System.Data.DbType.String);
            dynamicParameters.Add("@createdByIP", registrationModel.CreatedByIP, System.Data.DbType.String);

            return _dapper.ExecuteGet<UserViewModel>("sp_Registration", dynamicParameters);
        }

        public int UpdateOtpCount(Guid userId, string mobileNo, string OTP)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UserID", userId, System.Data.DbType.Guid);
            dynamicParameters.Add("MobileNo", mobileNo, System.Data.DbType.String);
            dynamicParameters.Add("OTP", OTP, System.Data.DbType.String);

            return _dapper.Execute("proc_UpdateOtpCount", dynamicParameters);
        }


        public UserViewModel AddEmployeeUser(RegistrationModel registrationModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@UserTypeId", registrationModel.RoleId, System.Data.DbType.Int32);
            dynamicParameters.Add("@MobileNo", registrationModel.MobileNo, System.Data.DbType.String);
            dynamicParameters.Add("@Name", registrationModel.Name, System.Data.DbType.String);
            dynamicParameters.Add("@IsActive", registrationModel.IsActive, System.Data.DbType.Boolean);

            return _dapper.ExecuteGet<UserViewModel>("InsertUserMaster", dynamicParameters);
        }
        public UserViewModel UpdateEmployeeUser(RegistrationModel registrationModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", registrationModel.Id, System.Data.DbType.Int32);
            dynamicParameters.Add("UserTypeId", registrationModel.RoleId, System.Data.DbType.Int32);
            dynamicParameters.Add("MobileNo", registrationModel.MobileNo, System.Data.DbType.String);
            dynamicParameters.Add("Name", registrationModel.Name, System.Data.DbType.String);
            dynamicParameters.Add("IsActive", registrationModel.IsActive, System.Data.DbType.Boolean);

            return _dapper.ExecuteGet<UserViewModel>("UpdateUserMaster", dynamicParameters);
        }
        public List<UserViewModel> GetEmployeeUsersList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<UserViewModel>("proc_GetEmployeeUsersList", dynamicParameters);
        }
        public UserViewModel GetEmployeeUserById(int Id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", Id, System.Data.DbType.Int32);
            return _dapper.ExecuteGet<UserViewModel>("proc_GetEmployeeUserById", dynamicParameters);
        }

        public long Delete(int ID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ID", ID, System.Data.DbType.Int32);
            return _dapper.Execute("DeleteEmployeeUserMaster", dynamicParameters);
        }

        public List<UserViewModel> GetCustomerUsersList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<UserViewModel>("proc_GetCustomerUsersList", dynamicParameters);
        }
        public int UsersMapping(UserMappingModel viewModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("EmployeeUserId", viewModel.EmployeeUserId, System.Data.DbType.Guid);
            dynamicParameters.Add("CustomerUserId", viewModel.CustomerUserId, System.Data.DbType.Guid);
            return _dapper.Execute("proc_UsersMapping", dynamicParameters);
        }
        public List<CustomerUsersMappedViewModel> GetCustomerUsersMappedList(CustomerUsersMappingRequestModel model)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("FromDate", model.FromDate, System.Data.DbType.DateTime);
            dynamicParameters.Add("ToDate", model.ToDate, System.Data.DbType.DateTime);
            return _dapper.ExecuteGetAll<CustomerUsersMappedViewModel>("GetEmployeesByDateRange", dynamicParameters);
        }

        public UserProfileResponseModel GetUserProfileByUserId(Guid userId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("userId", userId, System.Data.DbType.Guid);
            return _dapper.ExecuteGet<UserProfileResponseModel>("proc_GetUserProfileByUserId", dynamicParameters);
        }

        public UserProfileResponseModel UpdateUserProfile(UserProfileRequestModel model)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UserId", model.UserId, System.Data.DbType.Guid);
            dynamicParameters.Add("Name", model.Name, System.Data.DbType.String);
            dynamicParameters.Add("ProfilePicPath", model.ProfilePicPath, System.Data.DbType.String);
            dynamicParameters.Add("ShopName", model.ShopName, System.Data.DbType.String);
            dynamicParameters.Add("GstNo", model.GstNo, System.Data.DbType.String);
            dynamicParameters.Add("City", model.City, System.Data.DbType.String);
            dynamicParameters.Add("PinCode", model.PinCode, System.Data.DbType.String);
            dynamicParameters.Add("UserType", model.UserType, System.Data.DbType.String);
            dynamicParameters.Add("PurchaseQty", model.PurchaseQty, System.Data.DbType.String);

            return _dapper.ExecuteGet<UserProfileResponseModel>("proc_UpdateUserProfile", dynamicParameters);
        }

        public UserProfileResponseModel UpdateProfileImage(ProfileImageUploadModel model)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UserId", model.UserId, System.Data.DbType.Guid);
            dynamicParameters.Add("ProfilePicPath", model.PicturePath, System.Data.DbType.String);
            return _dapper.ExecuteGet<UserProfileResponseModel>("proc_UpdateProfileImage", dynamicParameters);
        }
    }
}
