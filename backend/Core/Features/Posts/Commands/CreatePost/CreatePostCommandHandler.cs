using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Posts.Commands.CreatePost;

public class CreatePostCommandHandler: ICommandHandler<CreatePostCommand>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post()
        {
            Body = request.Body,
            CreatedAt = DateTime.Now,
            Title = request.Title
        };

        try
        {
            await _postRepository.AddAsync(post).ConfigureAwait(false);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }
}