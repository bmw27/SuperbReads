using FluentValidation;
using MediatR;
using SuperbReads.Application.Common;
using SuperbReads.Application.Domain.Entities;
using SuperbReads.Application.Infrastructure.Persistence;

namespace SuperbReads.Application.Features.Authors;

public class CreateAuthorCommand : IRequest<long>
{
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }
}

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(Author.FullNameMaxLength)
            .NotEmpty();

        RuleFor(v => v.Bio)
            .MaximumLength(Author.BioMaxLength);
    }
}

internal sealed class CreateAuthorCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateAuthorCommand, long>
{
    public async Task<long> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Slug = Slug.Create(true, request.FullName),
            FullName = request.FullName,
            Bio = request.Bio
        };

#pragma warning disable S125
        // author.DomainEvents.Add(new AuthorCreatedEvent(author));
#pragma warning restore S125

        context.Authors.Add(author);

        await context.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}

public class AuthorCreatedEvent : DomainEvent
{
    public AuthorCreatedEvent(Author item)
    {
        Item = item;
    }

    public Author Item { get; }
}
