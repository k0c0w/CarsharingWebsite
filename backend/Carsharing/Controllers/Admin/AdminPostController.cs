using Contracts.NewsService;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
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
    public async Task<IActionResult> CreatePost([FromBody]PostDto postDto)
    {
        await _postService.CreatePostAsync(postDto);
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
    public async Task<IActionResult> EditPost([FromRoute]int id,[FromBody]EditPostDto editPostDto)
    {
        try
        {
            await _postService.EditPostAsync(id,editPostDto);
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
            await _postService.GetPostByIdAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}