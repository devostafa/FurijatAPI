using System.Text;
using Furijat.Data;
using Furijat.Data.Repositories.BlogRepository;
using Furijat.Data.Repositories.CategoriesRepository;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Data.Services.PasswordHash;
using Furijat.Services.Authentication;
using Furijat.Services.AutoMapper;
using Furijat.Services.Donation;
using Furijat.Services.JWT;
using Furijat.Services.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Furijat.Services;

public static class ServicesRegisterExtension
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IPasswordHash, PasswordHash>();
        serviceCollection.AddScoped<IAuthentication, Authentication.Authentication>();
        serviceCollection.AddScoped<IJWT, Jwt>();
        serviceCollection.AddScoped<IDonate, Donate>();
        serviceCollection.AddScoped<IMail, Mail.Mail>();
        serviceCollection.AddScoped<IProjectsRepository, ProjectsRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IBlogRepository, BlogRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddAutoMapper(cfg => { }, typeof(MapperProfile));
    }

    public static void AddSecurityServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication().AddJwtBearer(options =>
        {
            var secretKey = configuration["SecretKey"];

            if (secretKey == null) return;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = configuration["URL"],
                ValidAudience = configuration["clientURL"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        serviceCollection.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins(configuration["ClientURL"], configuration["ApiUrl"]).AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}