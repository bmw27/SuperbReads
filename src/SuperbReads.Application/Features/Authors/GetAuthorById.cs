using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperbReads.Application.Domain.Entities;
using SuperbReads.Application.Infrastructure.Persistence;

namespace SuperbReads.Application.Features.Authors;

public class GetAuthorByIdQuery : IRequest<AuthorDto>
{
    public long Id { get; set; }
}

internal sealed class GetAuthorByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Authors
            .AsNoTracking()
            .Where(author => author.Id == request.Id)
            .Select(author => ToDto(author))
            .FirstAsync(cancellationToken);
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
