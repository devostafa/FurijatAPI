using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Data.Data.Models;

namespace Furijat.Services.Services.Repositories.NewsRepository;

public interface INewsRepository
{
    public Task<List<NewsResponseDTO>> GetNews();
    public Task<NewsResponseDTO> GetNewsArticle(string newsId);
    public Task CreateNewsFolders();
    public Task AddNews(News newsEntry);
}