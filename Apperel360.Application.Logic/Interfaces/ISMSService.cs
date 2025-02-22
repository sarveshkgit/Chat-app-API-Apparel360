using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Logic.Interfaces
{
    public interface ISMSService
    {
        string[] SendOTP(string Recipient, string otpFor, string TemplateId);

        string SendSMS(String Message, String Recipient, string TemplateId = "");
    }
}
