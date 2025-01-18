using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperbReads.Application.Domain.Entities;
using SuperbReads.Application.Infrastructure.Persistence;

namespace SuperbReads.Application.Features.Posts;

public class GetPostsQuery : IRequest<List<PostDto>>;

public record PostDto(long Id, string UserId, string Title, string Content, DateTime CreatedAt);

internal sealed class GetPostsQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetPostsQuery, List<PostDto>>
{
    public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Include(x => x.User)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(post => ToDto(post))
            .ToListAsync(cancellationToken);
    }

    private static PostDto ToDto(Post post)
    {
        return new PostDto(
            post.Id,
            post.UserId,
            post.Title,
            post.Content,
            post.CreatedAt
        );
    }
}
