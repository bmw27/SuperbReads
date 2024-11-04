using SuperbReads.Application.Common.Interfaces;
using SuperbReads.Application.Domain.Entities;
using SuperbReads.Application.Infrastructure.Persistence;

namespace SuperbReads.Application.Features.Posts;

public class CreatePostCommand : IRequest<long>
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(Post.TitleMaxLength)
            .NotEmpty();

        RuleFor(v => v.Content)
            .MaximumLength(Post.ContentMaxLength)
            .NotEmpty();
    }
}

internal sealed class CreatePostCommandHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
    : IRequestHandler<CreatePostCommand, long>
{
    public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException();
        }

        var post = new Post
        {
            UserId = userId,
            Title = request.Title,
            Content = request.Content
        };

        // post.DomainEvents.Add(new TodoItemCreatedEvent(post));

        context.Posts.Add(post);

        await context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}

// public class PostCreatedEvent : DomainEvent
// {
//     public Post Item { get; }
//
//     public PostCreatedEvent(Post item)
//     {
//         Item = item;
//     }
// }
