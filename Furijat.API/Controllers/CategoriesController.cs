using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Services.Repositories.BlogRepository;
using Furijat.Services.Repositories.CategoriesRepository;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("Categories")]
public class CategoriesController : BaseController
{
    private readonly ICategoryRepository _categoryrepo;
    private IBlogRepository _newsrepo;

    public CategoriesController(ICategoryRepository categoryrepo)
    {
        _categoryrepo = categoryrepo;
    }

    [HttpGet("GetCategories")]
    public async Task<List<CategoryResponseDTO>> GetCategories()
    {
        return await _categoryrepo.GetCategories();
    }
}