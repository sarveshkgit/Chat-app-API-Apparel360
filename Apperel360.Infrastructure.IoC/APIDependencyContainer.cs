using Apperel360.Application.Interfaces;
using Apperel360.Application.Logic.Interfaces;
using Apperel360.Application.Logic.Services;
using Apperel360.Application.Services;
using Apperel360.Domain.DBConNameRepositories;
using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Apperel360.Infrastructure.Data.Context;
using Apperel360.Infrastructure.Data.Repositories;
using Apperel360.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Infrastructure.IoC
{
    public static class APIDependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };

                });

            // Add Authorization
            services.AddAuthorization(config =>
            {
                config.AddPolicy(PoliciesModel.Admin, PoliciesModel.AdminPolicy());
                config.AddPolicy(PoliciesModel.User, PoliciesModel.UserPolicy());
            });


            services.AddSingleton(new ConnectionString(configuration.GetConnectionString("ChatAppCon")));

            var connectionDict = new Dictionary<DatabaseConnectionName, string>
            {
                {
                    DatabaseConnectionName.Apperel360App, configuration.GetConnectionString("ChatAppCon")
                },

                //{ 
                //    DatabaseConnectionName.PayUMoneyEcom, configuration.GetConnectionString("PayUMoneyPG") 
                //}
            };

            services.AddSingleton<IDictionary<DatabaseConnectionName, string>>(connectionDict);
            services.AddScoped<IDapperDbContext, DapperDbContext>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<IJwtToken, JwtToken>();

            services.AddScoped<IAppUtility, AppUtility>();
            services.AddScoped<ISMSService, SMSService>();

            return services;

        }
    }
}
