using Furijat.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Furijat.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration _configEnv;

    public DataContext(IConfiguration configEnv)
    {
        _configEnv = configEnv;
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BlogArticle> BlogArticles { get; set; }
    public DbSet<Donation> DonationLogs { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configEnv.GetConnectionString("DatabaseConnection"));
    }
}