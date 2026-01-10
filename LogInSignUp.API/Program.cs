
using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.Concretes;
using LogInSignUp.BusinessLogic.Configuration.Mail;
using LogInSignUp.BusinessLogic.Configuration.Token;
using LogInSignUp.BusinessLogic.Mapping;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Password.Concretes;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Concretes;
using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Concretes;
using LogInSignUp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LogInSignUp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
            );
            
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddScoped<ITokenHasher, TokenHasher>();
            builder.Services.AddScoped<IMailService, MailService>();


            builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
            builder.Services.AddSingleton(sp =>sp.GetRequiredService<IOptions<TokenSettings>>().Value);
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddSingleton(sp =>sp.GetRequiredService<IOptions<MailSettings>>().Value);

            builder.Services.AddAutoMapper(typeof(GeneralMapping));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
