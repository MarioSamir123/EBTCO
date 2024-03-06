using EBTCO.Core.Contract;
using EBTCO.Core.Contract.Identity;
using EBTCO.DB;
using EBTCO.Domain.Identity;
using EBTCO.RDS.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToursYard.Core.Helpers;

namespace ToursYard.ServicesRegisteration
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            string secretKey = configuration.GetValue<String>("JWTKey") ?? String.Empty;

            services.AddIdentity<User, Role>(options =>
            {
                options.User = new UserOptions()
                {
                    RequireUniqueEmail = true,
                };
                options.Password = new PasswordOptions()
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = true,
                    RequiredLength = 8
                };
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAESEncryptor, AESEncrpytor>();
            
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidAlgorithms = new[] { "HS256" },
                    ValidateTokenReplay = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
