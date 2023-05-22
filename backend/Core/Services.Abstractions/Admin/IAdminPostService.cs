using Contracts.NewsService;

namespace Services.Abstractions.Admin;

public interface IAdminPostService : IPostService
{
    public Task CreatePostAsync(PostDto postDto);
    public Task DeletePostAsync(int id);
    public Task EditPostAsync(int id, EditPostDto editPostDto);
}