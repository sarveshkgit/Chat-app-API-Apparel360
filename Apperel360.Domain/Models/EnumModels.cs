using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    public class EnumModels
    {
    }

    public class MessageStream
    {
        public static string InValidRequest { get { return "Invalid Request."; } }
        public static string AllFieldsMandatory { get { return "All fields are required."; } }
        public static string RegisterSuccessfully { get { return "Register successfully."; } }
        public static string MessageSentSuccessfully { get { return "Message Sent successfully."; } }
        public static string RecordSavedSuccessfully { get { return "Record saved successfully."; } }
        public static string RecordUpdatedSuccessfully { get { return "Record updated successfully."; } }
        public static string RecordDeletedSuccessfully { get { return "Record deleted successfully."; } }
        public static string OTPSentSucess { get { return "OTP is sent to your registered Mobile No."; } }
        public static string EmailAlreadyExist { get { return "Email ID already exists!"; } }
        public static string MobileNoAlreadyExist { get { return "Mobile No. already exists!"; } }
        public static string ValidMobileNoRequired { get { return "Please enter valid Mobile No."; } }
        public static string SessionExpireMessage { get { return "Oh! It seems your session is expired."; } }
        public static string SomethingWentWrong { get { return "Sorry! Unable to process, something went wrong."; } }
        public static string InvalidCaptcha { get { return "Invalid captcha entered. Please Enter Valid Captcha."; } }
        public static string InvalidUser { get { return "Invalid User ID or Password is entered. Please enter correct details."; } }
        public static string UserNotFound { get { return "Invalid User ID Entered. Please enter valid User ID."; } }
    }
    public enum RoleEnum
    {
        Admin = 1,
        Employee = 2,
        Customer = 3
    }
}
