using System.ComponentModel.DataAnnotations;
using SuperbReads.Application.Common;

namespace SuperbReads.Application.Domain.Entities;

public class Author : AuditableEntity, IHasDomainEvent
{
    public const int FullNameMaxLength = 100;
    public const int BioMaxLength = 500;

    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    public string? Bio { get; set; }

    public List<DomainEvent> DomainEvents { get; } = [];
}
