using Furijat.Services.Base.Commands;
using Furijat.Services.Base.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("categories")]
public class CategoriesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : BaseController
{
    [HttpGet("")]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetCategoriesQuery();

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("add")]
    public async Task<IActionResult> AddCategory(string categoryName)
    {
        var command = new AddCategoryCommand(categoryName);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }
}