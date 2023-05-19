using Carsharing.ViewModels.Admin.Post;
using Contracts.NewsService;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Admin;

namespace Carsharing.Controllers;

[Route("post")]
[ApiController]
public class AdminPostController: ControllerBase
{
    private readonly IAdminPostService _postService;

    public AdminPostController(IAdminPostService postService)
    {
        _postService = postService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromBody]CreatePostVM postVm)
    {
        await _postService.CreatePostAsync(new PostDto
        {
            Title = postVm.Title,
            Body = postVm.Body
        });
        return Created("posts", null);
    }
    
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeletePost([FromRoute]int id)
    {
        try
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpPost("edit/{id:int}")]
    public async Task<IActionResult> EditPost([FromRoute]int id,[FromBody]EditPostVM editPostVm)
    {
        try
        {
            await _postService.EditPostAsync(id,new EditPostDto
            {
                Title = editPostVm.Title,
                Body = editPostVm.Body
            });
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("posts")]
    public async Task<IActionResult> GetAllPosts()
    {
        return new JsonResult(await _postService.GetAllPostsAsync());
    }
    
    [HttpGet("posts/{id:int}")]
    public async Task<IActionResult> GetPostById([FromRoute]int id)
    {
        try
        {
            return new JsonResult(await _postService.GetPostByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}