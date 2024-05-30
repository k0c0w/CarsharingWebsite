using Carsharing.ViewModels.Admin.Post;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Features.Posts;
using Carsharing.Helpers;
using System.Net;

namespace Carsharing.Controllers;

[Route("api/admin/post")]
[ApiController]
public class AdminPostController: ControllerBase
{
    private readonly ISender _mediator;

    public AdminPostController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromBody]CreatePostVM postVm)
    {
        var createPostResult = await _mediator.Send(new CreatePostCommand(postVm.Title, postVm.Body));

        if (createPostResult)
            return Created("posts", createPostResult.Value);

        return BadRequestWithErrorMessage(createPostResult.ErrorMessage);
    }
    
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeletePost([FromRoute]int id)
    {
        if (id <=0 )
            return BadRequest(new { error = new { code = (int)ErrorCode.ViewModelError, messages = new[] { "Invalid argument value." } } });

        var deletePostResult = await _mediator.Send(new DeletePostCommand(id));

        if (deletePostResult)
            return NoContent();

        return BadRequestWithErrorMessage(deletePostResult.ErrorMessage);
    }
    
    [HttpPut("edit/{id:int}")]
    public async Task<IActionResult> EditPost([FromRoute]int id, [FromBody]EditPostVM editPostVm)
    {
        if (id <= 0)
            return BadRequest(new { error = new { code = (int)ErrorCode.ViewModelError, messages = new[] { "Invalid argument value." } } });

        var editPostResult = await _mediator.Send(new UpdatePostCommand(editPostVm.Body, editPostVm.Title, id));
        if (editPostResult)
            return NoContent();

        return BadRequestWithErrorMessage(editPostResult.ErrorMessage);
    }
    
    [HttpGet("posts")]
    public async Task<IActionResult> GetAllPosts()
    {
        var postsResult = await _mediator.Send(new GetPostsQuery());

        if (!postsResult)
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);

        return new JsonResult(postsResult.Value!
            .Select(x => new PostVM()
        {
           Id = x.Id,
           Title = x.Title,
           Body = x.Body,
           CreatedAt = 
               new DateTime(x.CreatedAt.Year,x.CreatedAt.Month,x.CreatedAt.Day,
                   x.CreatedAt.Hour,x.CreatedAt.Minute,x.CreatedAt.Second,
                   DateTimeKind.Utc)
        }));
    }
    
    [HttpGet("posts/{id:int}")]
    public async Task<IActionResult> GetPostById([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        var getPostByIdResult = await _mediator.Send(new GetPostByIdQuery(id));

        if (getPostByIdResult)
            return new JsonResult(getPostByIdResult.Value);

        return NotFound();
    }

    private IActionResult BadRequestWithErrorMessage(string? message) => BadRequest(GenerateServiceError(message));

    private static object GenerateServiceError(string? message) => new { error = new { code = (int)ErrorCode.ServiceError, messages = new[] { message } } };
}