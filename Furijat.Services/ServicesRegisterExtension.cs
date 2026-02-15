using Furijat.Data.Data;
using Furijat.Data.Services.PasswordHash;
using Furijat.Services.Services.Authentication;
using Furijat.Services.Services.AutoMapper;
using Furijat.Services.Services.Donate;
using Furijat.Services.Services.JWT;
using Furijat.Services.Services.Mail;
using Furijat.Services.Services.Repositories.CategoriesRepository;
using Furijat.Services.Services.Repositories.NewsRepository;
using Furijat.Services.Services.Repositories.ProjectsRepository;
using Furijat.Services.Services.Repositories.UsersRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services;

public static class ServicesRegisterExtension
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IPasswordHash, PasswordHash>();
        serviceCollection.AddScoped<IAuthentication,Authentication>();
        serviceCollection.AddScoped<IJWT,Jwt>();
        serviceCollection.AddScoped<IDonate,Donate>();
        serviceCollection.AddScoped<IMail,Mail>();
        serviceCollection.AddScoped<IProjectsRepository,ProjectsRepository>();
        serviceCollection.AddScoped<ICategoryRepository,CategoryRepository>();
        serviceCollection.AddScoped<INewsRepository,NewsRepository>();
        serviceCollection.AddScoped<IUserRepository,UserRepository>();
        serviceCollection.AddAutoMapper(cfg => { }, typeof(MapperProfile));
    }
}