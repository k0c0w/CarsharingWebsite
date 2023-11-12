using Contracts.NewsService;

namespace Services.Abstractions;

public interface IPostService
{
    public Task<IEnumerable<PostDto>> GetAllPostsAsync();
    public Task<PostDto> GetPostByIdAsync(int id);
}