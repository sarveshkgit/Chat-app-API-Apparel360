using Apperel360.Application.Interfaces;
using Apperel360.Application.Logic.Interfaces;
using Apperel360.Application.Services;
using Apperel360.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Apperel360.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IJwtToken _jwtToken;
        private IAppUtility _appUtility;
        private ISMSService _sMSService;
        public AccountController(IAccountService accountService, IJwtToken jwtToken, IAppUtility appUtility, ISMSService sMSService)
        {
            _accountService = accountService;
            _jwtToken = jwtToken;
            _appUtility = appUtility;
            _sMSService = sMSService;
        }

        [HttpPost]
        [ActionName("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest();
                }
                login.RoleId = (int)RoleEnum.Customer;
                login.IpAddress = _appUtility.GetIPAddress(this.HttpContext);
                var userDetails = _accountService.GetLoginDetail(login);

                if (userDetails != null)
                {
                    if (userDetails.IsActive)
                    {
                        if (string.IsNullOrEmpty(userDetails.MobileNo))
                        {
                            return Ok(new
                            {
                                Type = "fail",
                                Code = HttpStatusCode.NotFound.ToString(),
                                Message = MessageStream.UserNotFound
                            });
                        }

                        DateTime datecheck = userDetails.OTPSendDate ?? DateTime.Now;
                        datecheck = datecheck.AddHours(24);
                        if (datecheck < DateTime.Now)
                        {
                            userDetails.OtpCount = 0;
                        }
                        if (userDetails.OtpCount >= 100)
                        {
                            return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = "You crossed maximum limit of sending OTP Please try After 24 hours." });
                        }

                        string[] smsStatus = new string[2];
                        string smsContent = SMSContentModel.OTPContent.Replace("<For>", "Verification").Replace("<OTP>", userDetails.OTP);

                        if (!string.IsNullOrEmpty(userDetails.MobileNo))
                        {
                            //smsStatus = _sMSService.SendOTP(userDetails.MobileNo, "send OTP", "1607100000000032462");
                            smsStatus[0] = _appUtility.GenerateNo();
                            smsStatus[1] = "success";
                        }
                        if (smsStatus[1].ToLower() == "success")
                        {
                            userDetails.Status = "Online";
                            userDetails.OTP = smsStatus[0];
                            int result = _accountService.UpdateOtpCount(userDetails.UserID, userDetails.MobileNo, userDetails.OTP);

                            //userDetails.MobileNo = userDetails.MobileNo.Substring(0, 2) + "XXXXX" + userDetails.MobileNo.Substring(7, 3);

                            return Ok(new { Type = "success", Code = "001", Message = "We have shared OTP on the registered Mobile No.Kindly fill and verify.", Data = userDetails });
                        }
                        else
                        {
                            return Ok(new { Type = "fail", Code = "002", Message = "SMS Service is not working currently. please try again later." });
                        }
                    }
                    else
                    {
                        return Ok(new { Type = "fail", Code = "002", Message = "User account is deactivated." });
                    }
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message }); }

        }


        [HttpPost]
        [ActionName("VerifyOTP")]
        [AllowAnonymous]
        public IActionResult VerifyOTP([FromBody] VerificationModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.MobileNo) && string.IsNullOrEmpty(model.OTP))
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Invalid Request" });
                }
                var userData = _accountService.VerifyUserDetail(model.MobileNo, model.OTP);
                if (userData == null)
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Details not found." });
                }

                if (string.IsNullOrEmpty(userData.MobileNo))
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Mobile No.not exist." });
                }
                if (userData.IsActive)
                {
                    userData.Token = _jwtToken.GenerateToken(userData);
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "User account is deactivated." });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message });
            }

        }

        [HttpPost]
        [ActionName("ResendOTP")]
        [AllowAnonymous]
        public IActionResult ResendOTP([FromBody] LoginModel model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }


                model.RoleId = (int)RoleEnum.Customer;
                model.IpAddress = _appUtility.GetIPAddress(this.HttpContext);
                var userDetails = _accountService.GetLoginDetail(model);

                if (userDetails != null)
                {
                    if (userDetails.IsActive)
                    {
                        if (string.IsNullOrEmpty(userDetails.MobileNo))
                        {
                            return Ok(new
                            {
                                Type = "fail",
                                Code = HttpStatusCode.NotFound.ToString(),
                                Message = MessageStream.UserNotFound
                            });
                        }

                        DateTime datecheck = userDetails.OTPSendDate ?? DateTime.Now;
                        datecheck = datecheck.AddHours(24);
                        if (datecheck < DateTime.Now)
                        {
                            userDetails.OtpCount = 0;
                        }
                        if (userDetails.OtpCount >= 100)
                        {
                            return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = "You crossed maximum limit of sending OTP Please try After 24 hours." });
                        }

                        string[] smsStatus = new string[2];
                        string smsContent = SMSContentModel.OTPContent.Replace("<For>", "Verification").Replace("<OTP>", userDetails.OTP);

                        if (!string.IsNullOrEmpty(userDetails.MobileNo))
                        {
                            //smsStatus = _sMSService.SendOTP(userDetails.MobileNo, "send OTP", "1607100000000032462");
                            smsStatus[0] = _appUtility.GenerateNo();
                            smsStatus[1] = "success";
                        }
                        if (smsStatus[1].ToLower() == "success")
                        {
                            userDetails.Status = "Online";
                            userDetails.OTP = smsStatus[0];
                            int result = _accountService.UpdateOtpCount(userDetails.UserID, userDetails.MobileNo, userDetails.OTP);

                            //userDetails.MobileNo = userDetails.MobileNo.Substring(0, 2) + "XXXXX" + userDetails.MobileNo.Substring(7, 3);

                            return Ok(new { Type = "success", Code = "001", Message = "We have shared OTP on the registered Mobile No.Kindly fill and verify.", Data = userDetails });
                        }
                        else
                        {
                            return Ok(new { Type = "fail", Code = "002", Message = "SMS Service is not working currently. please try again later." });
                        }
                    }
                    else
                    {
                        return Ok(new { Type = "fail", Code = "002", Message = "User account is deactivated." });
                    }
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message }); }
        }

        [HttpGet]
        [ActionName("GetUsers")]
        public IActionResult GetUsers(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Invalid Request" });
                }
                var userData = _accountService.GetUsers(userId);
                if (userData.Count > 0)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("AdminLogin")]
        [AllowAnonymous]
        public IActionResult AdminLogin([FromBody] AdminLoginModel login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest();
                }

                login.IpAddress = _appUtility.GetIPAddress(this.HttpContext);
                var userDetails = _accountService.GetAdminLogin(login);

                if (userDetails != null)
                {
                    if (userDetails.IsActive)
                    {
                        userDetails.Token = _jwtToken.GenerateToken(userDetails);
                        if (string.IsNullOrEmpty(userDetails.MobileNo))
                        {
                            return Ok(new
                            {
                                Type = "fail",
                                Code = HttpStatusCode.NotFound.ToString(),
                                Message = MessageStream.UserNotFound
                            });
                        }
                        else
                        {
                            return Ok(new { Type = "success", Code = "001", Message = "", Data = userDetails });
                        }
                    }
                    else
                    {
                        return Ok(new { Type = "fail", Code = "002", Message = "Admin account is deactivated." });
                    }
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message }); }

        }


        [HttpPost]
        [ActionName("Save")]
        public IActionResult AddUser([FromBody] RegistrationModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                model.RoleId = (int)RoleEnum.Employee;
                model.CreatedByIP = _appUtility.GetIPAddress(this.HttpContext);
                var user = _accountService.AddEmployeeUser(model);
                if (user != null)
                {
                    if (user.IsSucess == 1)
                    {
                        return Ok(new { Type = "success", Code = "001", Message = MessageStream.RegisterSuccessfully, Data = user });
                    }
                    else if (user.IsSucess == 2)
                    {
                        return Ok(new { Type = "fail", Code = "002", Message = MessageStream.MobileNoAlreadyExist, Data = user });
                    }
                    else
                        return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });

                }
                else
                {
                    return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = "002", Message = ex.Message }); }

        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateUser([FromBody] RegistrationModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                model.RoleId = (int)RoleEnum.Employee;
                model.CreatedByIP = _appUtility.GetIPAddress(this.HttpContext);
                var user = _accountService.UpdateEmployeeUser(model);
                if (user != null)
                {
                    if (user.IsSucess == 1)
                    {
                        return Ok(new { Type = "success", Code = "001", Message = MessageStream.RecordUpdatedSuccessfully, Data = user });
                    }
                    else
                        return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });

                }
                else
                {
                    return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = "002", Message = ex.Message }); }

        }

        [HttpGet]
        [ActionName("GetAll")]
        public IActionResult GetEmployeeUsers()
        {
            try
            {

                var userData = _accountService.GetEmployeeUsersList();
                if (userData.Count > 0)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpGet("{Id}")]
        [ActionName("GetbyId")]
        public IActionResult GetEmployeeUserById(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Invalid Request" });
                }

                var userData = _accountService.GetEmployeeUserById(Id);
                if (userData != null)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete([FromBody] Value viewModel)
        {
            var resDetails = _accountService.Delete(viewModel.Id);
            if (resDetails != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }

        //[HttpPost, Route("ForgotPassword")]
        //[AllowAnonymous]
        //public IActionResult ForgotPassword([FromBody] LoginViewModel model)
        //{
        //    if (string.IsNullOrEmpty(model.Username))
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "Invalid Request" });
        //    }
        //    return Ok();
        //    /*
        //    var userDetails = _accountService.GetUserDetails<ForgotViewModel>(model);
        //    if (userDetails == null)
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "Details not found." });
        //    }

        //    if (string.IsNullOrEmpty(userDetails.MobileNo) && string.IsNullOrEmpty(userDetails.EmailId))
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "Mobile No. and Email Id not exist." });
        //    }

        //    if (!userDetails.IsMobileVarified && !userDetails.IsEmailVarified)
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "Mobile No. and Email Id not Verified kindly Contact to Department for Reset Your Password." });
        //    }

        //    DateTime datecheck = userDetails.OTPSendDate ?? DateTime.Now;
        //    datecheck = datecheck.AddHours(24);
        //    if (datecheck < DateTime.Now)
        //    {
        //        userDetails.OtpCount = 0;
        //    }

        //    if (userDetails.OtpCount >= 3)
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "You crossed maximum limit of sending OTP Please try After 24 hours." });
        //    }

        //    //userDetails.OTP = _appUtility.GenerateNo();

        //    string emailStatus = "";
        //    string[] smsStatus = new string[2];

        //    if (!string.IsNullOrEmpty(userDetails.MobileNo) && userDetails.IsMobileVarified)
        //    {
        //        smsStatus = _sMSService.SendOTP(userDetails.MobileNo, "forgot password", "1607100000000032462");
        //    }

        //    string smsContent = SMSContentModel.OTPContent.Replace("<For>", "forgot password").Replace("<OTP>", smsStatus[0]);
        //    if (!string.IsNullOrEmpty(userDetails.EmailId) && userDetails.IsEmailVarified)
        //    {
        //        emailStatus = _emailService.SendEmail(userDetails.EmailId, "Forgot Password", smsContent, "", "");
        //    }

        //    if (smsStatus[1].ToLower() == "success" || emailStatus.ToLower() == "success")
        //    {
        //        userDetails.OTP = smsStatus[0];

        //        int result = _accountService.UpdateOtpCount(userDetails.UserID, userDetails.MobileNo);

        //        return Ok(new { Type = "success", Code = "001", Message = "We have shared OTP on the registered Mobile No. and Email Id. Kindly fill and proceed.", Data = userDetails });
        //    }
        //    else
        //    {
        //        return Ok(new { Type = "fail", Code = "002", Message = "SMS Service is not working currently. please try again later." });
        //    }
        //    */
        //}

        [HttpGet]
        [ActionName("GetAllCustomer")]
        public IActionResult GetCustomerUsers()
        {
            try
            {

                var userData = _accountService.GetCustomerUsersList();
                if (userData.Count > 0)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("GetCustomerUsersMappedList")]
        public IActionResult GetCustomerUsersMappedList([FromBody] CustomerUsersMappingRequestModel model)
        {
            try
            {
                var userData = _accountService.GetCustomerUsersMappedList(model);
                if (userData.Count > 0)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("UserMapping")]
        public IActionResult UsersMapping([FromBody] UserMappingModel viewModel)
        {
            int count = _accountService.UsersMapping(viewModel);
            if (count > 0)
            {
                return Ok(new { Type = "success", Code = "001", Message = "Mapped User Successfully", Data = "" });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }

        [HttpGet]
        [ActionName("GetUserProfileByUserId")]
        public IActionResult GetUserProfileByUserId(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return Ok(new { Type = "fail", Code = "002", Message = "Invalid Request" });
                }
                var userData = _accountService.GetUserProfileByUserId(userId);
                if (userData != null)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = userData });
                }
                else
                {
                    return Ok(new
                    {
                        Type = "failed",
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = MessageStream.UserNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Type = "failed", Code = "002", Message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("UpdateUserProfile")]
        public IActionResult UpdateUserProfile([FromBody] UserProfileRequestModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var user = _accountService.UpdateUserProfile(model);
                if (user != null)
                {
                    if (user.IsSucess == 1)
                    {
                        return Ok(new { Type = "success", Code = "001", Message = MessageStream.RecordUpdatedSuccessfully, Data = user });
                    }
                    else
                        return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });

                }
                else
                {
                    return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
                }
            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = "002", Message = ex.Message }); }

        }

        [HttpPost, DisableRequestSizeLimit]
        [ActionName("UpdateProfileImage")]
        public IActionResult UpdateProfileImage([FromForm] ProfileImageUploadModel model)
        {
            try
            {
                if (model.ProfileImage == null || model.ProfileImage.Length == 0)
                {
                    return BadRequest();
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Allowed extensions
                var folderName = "ProfileImages";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var postedFile = model.ProfileImage;


                var originalFileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition).FileName.Trim('"');
                var extension = Path.GetExtension(originalFileName).ToLower(); // Always lowercase for comparison
                var newfileName = $"{model.UserId}{extension}";

                var fullPath = Path.Combine(pathToSave, newfileName);
                var dbPath = Path.Combine(folderName, newfileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                model.PicturePath = newfileName;

                var res = _accountService.UpdateProfileImage(model);
                //var res = model;

                if (res != null)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = res });
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }

            }
            catch (Exception ex) { return Ok(new { Type = "fail", Code = "002", Message = ex.Message }); }

        }


    }
}
