using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    public class ContentModels
    {
    }
    public class SMSContentModel
    {
        public static string OTPContent { get; } = "Your OTP for <For> is <OTP> -Apparel 360 Pvt. Ltd";
        public static string CnangePasswordContent { get; } = "Your password has been changed.Your User ID is <Username> and Password is <Password>. Apparel 360 Pvt. Ltd.";

    }
}
