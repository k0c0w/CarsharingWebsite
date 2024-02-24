using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler: ICommandHandler<UpdatePostCommand>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if (post is null)
                return new Error("Post was not found");
            UpdateModel(post,request);
            await _postRepository.UpdateAsync(post).ConfigureAwait(false);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }

    private void UpdateModel(Post post, UpdatePostCommand request)
    {
        if (request.Body is not null)
            post.Body = request.Body;
        if (request.Title is not null)
            post.Title = request.Title;
    }
    
}