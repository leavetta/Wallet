using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Text;
using WalletAspNetCore.Auth;

namespace WalletAspNetCore.Api.Helpers
{
    public static class AuthenticationHelper
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
                var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
                services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };

                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        context.Token = context.Request.Cookies["secretCookie"];

                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            services.AddAuthorization();
        }
    }
}
