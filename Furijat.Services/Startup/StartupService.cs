using Furijat.Data;
using Furijat.Data.Repositories.BlogRepository;
using Furijat.Data.Repositories.ProjectsRepository;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.PasswordHash;
using Furijat.Services.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services.Startup;

public static class Startup
{
    public static async Task Execute(IServiceProvider serviceProvider, IWebHostEnvironment webEnv)
    {
        var storageFolder = Path.Combine(webEnv.ContentRootPath, "Storage");
        Directory.CreateDirectory(storageFolder);

        var dbService = serviceProvider.GetRequiredService<DataContext>();
        await dbService.Database.MigrateAsync();

        var newsService = serviceProvider.GetRequiredService<IBlogRepository>();
        newsService.CreateNewsFolders();

        var projectsService = serviceProvider.GetRequiredService<IProjectsRepository>();
        await projectsService.CreateFoldersAsync();

        var usersService = serviceProvider.GetRequiredService<IUserRepository>();
        usersService.CreateFolders();

        var hashService = serviceProvider.GetRequiredService<IHashService>();
        await SeedService.SeedDatabase(dbService, hashService);
    }
}