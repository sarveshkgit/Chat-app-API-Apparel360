using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Logic.Interfaces
{
    public interface IAppUtility
    {
        string GenerateNo();
        string GetIPAddress(HttpContext httpContext);
        T1 ExchangeModelData<T1, T2>(T2 Content);
        string CreateRandomPassword(int length = 6);
        List<T1> ExchangeModelData<T1, T2>(List<T2> Content);
    }
}
