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
            _dapper= dapper;
        }
        public int ChangePassword(int flag, long userId, string password)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetLoginDetail(string mobileno, string IpAddress,int RoleId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("MobileNo", mobileno, System.Data.DbType.String);
            dynamicParameters.Add("IpAddress", IpAddress, System.Data.DbType.String);
            dynamicParameters.Add("RoleId", RoleId, System.Data.DbType.Int32);

            return _dapper.ExecuteGet<UserViewModel>("proc_getLoginDetails", dynamicParameters);
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
            dynamicParameters.Add("UserID", UserID, System.Data.DbType.Guid);
            return _dapper.ExecuteGetAll<UserMappingViewModel>("proc_GetMappedUser", dynamicParameters);
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
    }
}
