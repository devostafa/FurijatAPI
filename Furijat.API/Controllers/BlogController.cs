using Furijat.Services.Base.Commands;
using Furijat.Services.Base.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("blog")]
public class BlogController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : BaseController
{

    [HttpGet("")]
    public async Task<IActionResult> GetBlogPosts()
    {
        var query = new GetBlogPostsQuery();

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetBlogPost(string postId)
    {
        var query = new GetBlogPostQuery(postId);

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("add")]
    public async Task<IActionResult> AddPost(NewPostRequestDTO newPostRequest)
    {
        var command = new AddBlogPostCommand(newPostRequest);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }
}