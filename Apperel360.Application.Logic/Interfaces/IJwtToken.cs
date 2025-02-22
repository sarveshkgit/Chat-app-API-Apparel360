using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Apperel360.Domain.Models;

namespace Apperel360.Application.Logic.Interfaces
{
    public interface IJwtToken
    {
        public string GenerateToken(UserViewModel user);
        public LoginViewModel GetTokenDetails(HttpContext cntx);
    }
}
