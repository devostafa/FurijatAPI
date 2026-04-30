using Furijat.Data;
using Furijat.Data.Enums;
using Furijat.Data.Models;
using Furijat.Services.PasswordHash;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Services.Seed;

public static class SeedService
{
    public static async Task SeedDatabase(DataContext dbContext, IHashService hashService)
    {
        if (await dbContext.Users.AnyAsync()) return;

        List<User> usersSeedData = new List<User>
        {
            new User
            {
                Id = Guid.Parse("c0c343f3-a9d0-4ae6-93e4-0d1923b04e60"),
                Name = "testuser1",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test1@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            },
            new User
            {
                Id = Guid.Parse("913eedbd-a304-478e-beee-4c8db66bd86a"),
                Name = "testuser2",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test2@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            },
            new User
            {
                Id = Guid.Parse("2e445054-8f22-4812-adb7-38cd849c976b"),
                Name = "testuser3",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test3@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            },
            new User
            {
                Id = Guid.Parse("a5379337-e6a4-4222-aa88-233358bda6e9"),
                Name = "testuser4",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test4@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            },
            new User
            {
                Id = Guid.Parse("9bdfe044-4b02-40a7-ade7-4570e68af19c"),
                Name = "testuser5",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test5@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            },
            new User
            {
                Id = Guid.Parse("c8b590f1-c920-4c1b-9237-852bc0b43518"),
                Name = "testadmin",
                PasswordHash = hashService.CreateHashedPassword("1234"),
                Usertype = UserTypeEnum.User,
                PhoneNumber = "123456789",
                Email = "test6@gmail.com",
                Facebook = "",
                Instagram = "",
                X = "",
                Profileimage = "profile.jpg"
            }
        };

        List<BlogArticle> blogArticlesSeedData = new List<BlogArticle>
        {
            new BlogArticle
            {
                Id = Guid.Parse("0f97ea1d-e247-4cf5-a6d9-5f9d3265e220"),
                Title = "Innovative Breakthroughs: College Students Secure Funding for Groundbreaking Projects",
                Subtitle = "Subtitle Test",
                Description = "Desc Test",
                Published = new DateOnly(2024, 1, 1),
                CoverName = "cover.jpg"
            }
        };

        List<Category> categoriesSeedData = new List<Category>
        {
            new Category
            {
                Id = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"), Name = "product"
            },
            new Category
            {
                Id = Guid.Parse("fafaad46-3fbe-40ac-ad63-c311829668a4"), Name = "society"
            },
            new Category
            {
                Id = Guid.Parse("59cb7c8b-8e33-45d6-b066-214f3145a3c0"), Name = "environment"
            }
        };

        List<Project> projectsSeedData = new List<Project>
        {
            new Project
            {
                Id = Guid.Parse("7e4788cd-77a9-4b03-9412-385a482cf489"),
                Title = "Greener Egypt",
                Description = "Description Test",
                CategoryId = Guid.Parse("59cb7c8b-8e33-45d6-b066-214f3145a3c0"),
                UserId = Guid.Parse("c0c343f3-a9d0-4ae6-93e4-0d1923b04e60"),
                CurrentFund = 500,
                FundRequired = 2000,
                ImagesNames = new[]
                {
                    "1.jpg", "2.jpg"
                },
                SocialMedia = new SocialMedia
                {
                    Platform = SocialMediaPlatformEnum.Facebook,
                    Url = "https://facebook.com/greener-egypt"
                }
            },
            new Project
            {
                Id = Guid.Parse("694d6683-d3e6-4bc1-ab5d-f2f67f887332"),
                Title = "My Super Awesome Health Machine Research Paper",
                Description = "Description Test",
                CategoryId = Guid.Parse("fafaad46-3fbe-40ac-ad63-c311829668a4"),
                CurrentFund = 500,
                FundRequired = 1000000,
                UserId = Guid.Parse("913eedbd-a304-478e-beee-4c8db66bd86a"),
                ImagesNames = new[]
                {
                    "1.jpg", "2.jpg"
                },
                SocialMedia = new SocialMedia
                {
                    Platform = SocialMediaPlatformEnum.Facebook,
                    Url = "https://facebook.com/health-machine"
                }
            },
            new Project
            {
                Id = Guid.Parse("a9437a37-1d37-4a9b-adbd-a18ef0490942"),
                Title = "Electric Koshary Machine",
                Description = "Description Test",
                CategoryId = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"),
                CurrentFund = 500,
                FundRequired = 120000,
                UserId = Guid.Parse("2e445054-8f22-4812-adb7-38cd849c976b"),
                ImagesNames = new[]
                {
                    "1.jpg", "2.jpg"
                },
                SocialMedia = new SocialMedia
                {
                    Platform = SocialMediaPlatformEnum.Facebook,
                    Url = "https://facebook.com/koshary-machine"
                }
            },
            new Project
            {
                Id = Guid.Parse("e9c8eccf-76aa-42d6-be67-803d8622c951"),
                Title = "Indie Egyptian Game",
                Description = "Description Test",
                CategoryId = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"),
                UserId = Guid.Parse("a5379337-e6a4-4222-aa88-233358bda6e9"),
                CurrentFund = 500,
                FundRequired = 60000,
                ImagesNames = new[]
                {
                    "1.jpg", "2.jpg"
                },
                SocialMedia = new SocialMedia
                {
                    Platform = SocialMediaPlatformEnum.Facebook,
                    Url = "https://facebook.com/indie-game"
                }
            }
        };

        await dbContext.Users.AddRangeAsync(usersSeedData);
        await dbContext.BlogArticles.AddRangeAsync(blogArticlesSeedData);
        await dbContext.Categories.AddRangeAsync(categoriesSeedData);
        await dbContext.Projects.AddRangeAsync(projectsSeedData);
        await dbContext.SaveChangesAsync();
    }
}