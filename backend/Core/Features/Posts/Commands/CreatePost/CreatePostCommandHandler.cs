using Domain.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Results;

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
            var postId = await _postRepository.AddAsync(post).ConfigureAwait(false);
            return new Ok<int>(postId);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message, ex);

            return new Error<int>("Could not create post!");
        }
    }
}