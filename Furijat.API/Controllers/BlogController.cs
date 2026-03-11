using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.BlogRepository;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("blog")]
public class BlogController : BaseController
{
    private readonly IBlogRepository _blogRepo;

    public BlogController(IBlogRepository blogRepo)
    {
        _blogRepo = blogRepo;
    }

    [HttpGet("articles")]
    public async Task<List<BlogArticleResponseDTO>> GetArticles()
    {
        return await _blogRepo.GetNews();
    }
}