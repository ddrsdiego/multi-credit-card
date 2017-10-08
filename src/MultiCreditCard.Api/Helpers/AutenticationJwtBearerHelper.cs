using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MultiCreditCard.Api.Helpers
{
    public static class AutenticationJwtBearerHelper
    {
        public static void AddJwtBearer(this IServiceCollection services, IConfiguration config)
        {
            var authCongi = config.GetSection("AuthConfig");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://api.multicreditcard.com.br",
                    ValidAudience = "http://api.multicreditcard.com.br",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fcebf5457e484be1ab50772e236ccd22fcb32d345e41459986cdb973d2d1a34e"))
                };
            });
        }
    }
}