using System.Text;
using Furijat.Data;
using Furijat.Services.AutoMapper;
using Furijat.Services.Base.Commands;
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

        // Scans and registers class services from Services project
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

        // Scans and registers class services from Data project
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
            var secretKey = configuration["Jwt:SecretKey"];

            if (secretKey == null) return;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:ClientURL"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        serviceCollection.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins(configuration["Cors:ClientOrigin"], configuration["Cors:ApiOrigin"]).AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}