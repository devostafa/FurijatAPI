using Furijat.Data.Data;
using Furijat.Services.Services.Repositories.NewsRepository;
using Furijat.Services.Services.Repositories.ProjectsRepository;
using Furijat.Services.Services.Repositories.UsersRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services.Services.StartupService;

public static class Startup
{
    public static void Execute(IServiceProvider serviceProvider, IWebHostEnvironment webEnv)
    {
        var storageFolder = Path.Combine(webEnv.ContentRootPath, "Storage");
        Directory.CreateDirectory(storageFolder);
        
        var dbService = serviceProvider.GetRequiredService<DataContext>();
        dbService.Database.Migrate();
        
        var newsService = serviceProvider.GetRequiredService<INewsRepository>();
        newsService.CreateNewsFolders();
        
        var projectsService = serviceProvider.GetRequiredService<IProjectsRepository>();
        projectsService.CreateFolders();
        
        var usersService = serviceProvider.GetRequiredService<IUserRepository>();
        usersService.CreateFolders();
    }
}