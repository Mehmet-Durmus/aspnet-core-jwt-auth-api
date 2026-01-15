using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.Concretes;
using LogInSignUp.BusinessLogic.Configuration.Mail;
using LogInSignUp.BusinessLogic.Configuration.Token;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Password.Concretes;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<TokenSettings>>().Value);
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<ITokenHasher, TokenHasher>();
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}
