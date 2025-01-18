using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperbReads.Application.Domain.Entities;
using SuperbReads.Application.Infrastructure.Persistence;

namespace SuperbReads.Application.Features.Authors;

public class GetAuthorsQuery : IRequest<List<AuthorDto>>;

public record AuthorDto(long Id, string FullName, string? Bio, DateTime CreatedAt);

internal sealed class GetAuthorsQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetAuthorsQuery, List<AuthorDto>>
{
    public async Task<List<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await context.Authors
            .AsNoTracking()
            .Select(author => ToDto(author))
            .ToListAsync(cancellationToken);
    }

    private static AuthorDto ToDto(Author author)
    {
        return new AuthorDto(
            author.Id,
            author.FullName,
            author.Bio,
            author.CreatedAt
        );
    }
}
