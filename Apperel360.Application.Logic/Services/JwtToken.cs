using Apperel360.Application.Logic.Interfaces;
using Apperel360.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Logic.Services
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _config;
        public JwtToken(IConfiguration config)
        {
            _config= config;
        }
        public string GenerateToken(UserViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.MobileNo),
                new Claim("name", user.Name.ToString()),
                new Claim("role",user.RoleName),
                new Claim("userid", user.UserID.ToString()),
                new Claim("mobileno", user.MobileNo),
                new Claim("rolename", user.RoleName),
                new Claim("roleid", user.RoleID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public LoginViewModel GetTokenDetails(HttpContext cntx)
        {
            IEnumerable<Claim> claims = cntx.User.Claims;

            return new LoginViewModel()
            {
                Name = claims.FirstOrDefault(x => x.Type == "name").Value,
                RoleName = claims.FirstOrDefault(x => x.Type == "rolename").Value,
                UserID = Guid.Parse(claims.FirstOrDefault(x => x.Type == "userid").Value),
                //Username = claims.FirstOrDefault(x => x.Type == "username").Value,
                RoleID = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == "roleid").Value),
                MobileNo = claims.FirstOrDefault(x => x.Type == "mobileno").Value,
            };
        }

       
    }
}
