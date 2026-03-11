using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Data.Repositories.BlogRepository;

public class BlogRepository : IBlogRepository
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _hostEnv;
    private readonly IMapper _mapper;

    public BlogRepository(DataContext db, IMapper mapper, IWebHostEnvironment hostEnv)
    {
        _db = db;
        _hostEnv = hostEnv;
        _mapper = mapper;
    }

    public async Task<List<BlogArticleResponseDTO>> GetNews()
    {
        return await _db.BlogArticles.ProjectTo<BlogArticleResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<BlogArticleResponseDTO> GetNewsArticle(string newsId)
    {
        return await _db.BlogArticles.ProjectTo<BlogArticleResponseDTO>(_mapper.ConfigurationProvider).FirstAsync(n => n.Id == Guid.Parse(newsId));
    }

    public async Task CreateNewsFolders()
    {
        try
        {
            List<BlogArticle> newsList = await _db.BlogArticles.ToListAsync();

            foreach (var news in newsList)
            {
                var newsFolderPath = Path.Combine(_hostEnv.ContentRootPath, "Storage", "BlogArticle",
                    $"{news.Id}", "Images");
                Directory.CreateDirectory(newsFolderPath);
            }

            Console.WriteLine("Created BlogArticle folders successfully");
        }
        catch (Exception err)
        {
            throw err;
        }
    }

    public async Task AddNews(BlogArticle blogArticleEntry)
    {
        await _db.BlogArticles.AddAsync(blogArticleEntry);
    }
}