using Furijat.Data;
using Furijat.Services.Repositories.BlogRepository;
using Furijat.Services.Repositories.ProjectsRepository;
using Furijat.Services.Repositories.UsersRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services.StartupService;

public static class Startup
{
    public static void Execute(IServiceProvider serviceProvider, IWebHostEnvironment webEnv)
    {
        var storageFolder = Path.Combine(webEnv.ContentRootPath, "Storage");
        Directory.CreateDirectory(storageFolder);

        var dbService = serviceProvider.GetRequiredService<DataContext>();
        dbService.Database.Migrate();

        var newsService = serviceProvider.GetRequiredService<IBlogRepository>();
        newsService.CreateNewsFolders();

        var projectsService = serviceProvider.GetRequiredService<IProjectsRepository>();
        projectsService.CreateFolders();

        var usersService = serviceProvider.GetRequiredService<IUserRepository>();
        usersService.CreateFolders();
    }
}