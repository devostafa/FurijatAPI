using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;

namespace Furijat.Services.Repositories.BlogRepository;

public interface IBlogRepository
{
    public Task<List<BlogArticleResponseDTO>> GetNews();

    public Task<BlogArticleResponseDTO> GetNewsArticle(string newsId);

    public Task CreateNewsFolders();

    public Task AddNews(BlogArticle blogArticleEntry);
}