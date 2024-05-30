using Domain.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;

namespace Features.Posts.Commands.CreatePost;

public class CreatePostCommandHandler: ICommandHandler<CreatePostCommand, int>
{
    private readonly IPostRepository _postRepository;

    private readonly ILogger<Exception> _logger;

    public CreatePostCommandHandler(ILogger<Exception> logger, IPostRepository postRepository)
    {
        _postRepository = postRepository;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post()
        {
            Body = request.Body,
            CreatedAt = DateTime.Now,
            Title = request.Title
        };

        try
        {
            await _postRepository.AddAsync(post);

            return new Ok<int>(post.Id);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error,"Exception: {ex}", ex);

            return new Error<int>("Could not create post!");
        }
    }
}