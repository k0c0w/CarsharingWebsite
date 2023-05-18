using Contracts.NewsService;
using Domain;
using Domain.Entities;
using Services.Abstractions.Admin;
using Services.Exceptions;

namespace Services;

public class PostService: IAdminPostService
{
    private readonly CarsharingContext _carsharingContext;

    public PostService(CarsharingContext carsharingContext)
    {
        _carsharingContext = carsharingContext;
    }

    public async Task CreatePostAsync(PostDto postDto)
    {
        var post = new Post()
        {
            Body = postDto.Body,
            CreatedAt = DateTime.Now,
            Title = postDto.Title,
            Id = postDto.Id
        };

        await _carsharingContext.News.AddAsync(post);
        await _carsharingContext.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await _carsharingContext.News.FindAsync(id);
        if(post == null) throw new ObjectNotFoundException(nameof(Post));

        _carsharingContext.News.Remove(post);
        await _carsharingContext.SaveChangesAsync();
    }

    public async Task EditPostAsync(int id, EditPostDto editPostDto)
    {
        var oldPost = await _carsharingContext.News.FindAsync(id);
        if(oldPost == null) throw new ObjectNotFoundException(nameof(Post));

        if (editPostDto.Body != null)
            oldPost.Body = editPostDto.Body;
        if (editPostDto.Title != null)
            oldPost.Body = editPostDto.Title;

        _carsharingContext.News.Update(oldPost);
        await _carsharingContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        return _carsharingContext.News.Select(x => new PostDto()
        {
            Body = x.Body,
            Title = x.Title,
            Id = x.Id
        });
    }

    public async Task<PostDto> GetPostByIdAsync(int id)
    {
        var post = await _carsharingContext.News.FindAsync(id);
        if(post == null) throw new ObjectNotFoundException(nameof(Post));
        return new PostDto(){
            Body = post.Body,
            Title = post.Title,
            Id = post.Id
        };
    }
}