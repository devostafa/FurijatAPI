using System.Text;
using Furijat.Data;
using Furijat.Services.Authentication;
using Furijat.Services.AutoMapper;
using Furijat.Services.Base.Commands;
using Furijat.Services.Donation;
using Furijat.Services.Jwt;
using Furijat.Services.Mail;
using Furijat.Services.PasswordHash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scrutor;

namespace Furijat.Services;

public static class ServicesRegisterExtension
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IPasswordHash, PasswordHash.PasswordHash>();
        serviceCollection.AddScoped<IAuthentication, Authentication.Authentication>();
        serviceCollection.AddScoped<IJWTService, JWTService>();
        serviceCollection.AddScoped<IDonationService, DonationService>();
        serviceCollection.AddScoped<IMail, Mail.Mail>();

        serviceCollection.Scan(selector => selector
            .FromAssemblyOf<CommandDispatcher>()
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }

    public static void AddDatabaseServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();

        serviceCollection.Scan(selector => selector
            .FromAssemblyOf<DataContext>()
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
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