using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Talabat.APIS.G02.Extensions
{
    public static class IdentityServicesConfigurations
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped(typeof(ITokenService), typeof(TokenService));

            Services.AddIdentity<AppUser, IdentityRole>()
           .AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => {
                   options.TokenValidationParameters = new TokenValidationParameters() {
                       ValidateIssuer = true,
                       ValidIssuer = configuration["JWT:Issuer"],
                       ValidateAudience = true,
                       ValidAudience = configuration["JWT:Audience"],
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                   };
                }

                );


            return Services;
        }
    }
}
