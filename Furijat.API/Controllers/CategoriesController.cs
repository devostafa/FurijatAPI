using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Services.Base.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("categories")]
public class CategoriesController(IQueryDispatcher queryDispatcher) : BaseController
{

    [HttpGet("")]
    public async Task<List<CategoryResponseDTO>> GetCategories()
    {
        return await _categoryrepo.GetCategories();
    }
}