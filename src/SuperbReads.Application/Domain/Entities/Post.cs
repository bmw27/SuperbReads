using System.ComponentModel.DataAnnotations;
using SuperbReads.Application.Common;

namespace SuperbReads.Application.Domain.Entities;

public class Post : AuditableEntity
{
    public const int TitleMaxLength = 100;
    public const int ContentMaxLength = 1000;

    [MaxLength(ApplicationUser.IdMaxLength)]
    public string UserId { get; set; } = null!;

    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}
