using Apperel360.Application.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Logic.Services
{
    public class AppUtility : IAppUtility
    {
        private readonly IConfiguration _Configure;
        public AppUtility(IConfiguration configuration)
        {
            _Configure = configuration;
        }
        public string CreateRandomPassword(int length = 6)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public T1 ExchangeModelData<T1, T2>(T2 Content)
        {
            throw new NotImplementedException();
        }

        public List<T1> ExchangeModelData<T1, T2>(List<T2> Content)
        {
            throw new NotImplementedException();
        }

        public string GenerateNo()
        {
            string characters = string.Empty;

            string numbers = "1234567890";
            characters += numbers;

            string otp = string.Empty;
            string otpUser = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public string GetIPAddress(HttpContext httpContext)
        {
            String ip = "";
            try
            {
                ip = httpContext.Connection.RemoteIpAddress?.ToString();
            }
            catch
            {
                ip = httpContext.Connection.LocalIpAddress?.ToString();
            }
            return ip;
        }

        
    }
}
