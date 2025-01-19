using System.ComponentModel.DataAnnotations;
using SuperbReads.Application.Common;
using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Domain.Entities;

public class Author : AuditableEntity, IHasExternalId, IHasSlug, IHasDomainEvent
{
    public const int FullNameMaxLength = 100;
    public const int BioMaxLength = 500;

    public Guid ExternalId { get; set; }

    public string Slug { get; set; } = null!;

    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    public string? Bio { get; set; }

    public List<DomainEvent> DomainEvents { get; } = [];
}
