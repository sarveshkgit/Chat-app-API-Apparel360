using Apperel360.Application.Logic.Interfaces;
using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Logic.Services
{
    public class SMSService : ISMSService
    {
        private IAppUtility _appUtility;
        public SMSService(IAppUtility appUtility)
        {
            _appUtility = appUtility;
        }
        public string[] SendOTP(string Recipient, string otpFor, string TemplateId)
        {
            string[] str = new string[2];
            str[0] = _appUtility.GenerateNo();
            string content = SMSContentModel.OTPContent.Replace("<For>", otpFor).Replace("<OTP>", str[0]);

            str[1] = SendSMS(content, Recipient, TemplateId);

            return str;
        }

        public string SendSMS(string Message, string Recipient, string TemplateId = "")
        {
            try
            {
                WebClient Client = new WebClient();
                Recipient = Recipient.Trim();
                if (Recipient.Substring(Recipient.Length - 1, 1) == ",")
                    Recipient = Recipient.Substring(0, Recipient.Length - 1);

                //string baseurl = "http://103.16.101.52/sendsms/bulksms?username=omnt-FILMUP&password=JHK456IO&type=0&dlr=1&destination=" + Recipient + "&source=FILMUP&message=" + WebUtility.UrlEncode(Message) + "&entityid=1601663160757691660&tempid=";

                //string baseurl = "http://103.16.101.52/bulksms/bulksms?username=omni-omnint&password=ZX45I90O&type=0&dlr=1&destination=" + Recipient + "&source=OMNINT&message=" + WebUtility.UrlEncode(Message) + "&&entityid=1601100000000007326&tempid=" + TemplateId;
                string baseurl = "";
                Stream data = Client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
