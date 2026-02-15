using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Services.Services.Repositories.CategoriesRepository;
using Furijat.Services.Services.Repositories.NewsRepository;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("Categories")]
public class CategoriesController : BaseController
{
    private INewsRepository _newsrepo;
    private readonly ICategoryRepository _categoryrepo;

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