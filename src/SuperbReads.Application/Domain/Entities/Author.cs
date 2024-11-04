using System.ComponentModel.DataAnnotations;
using SuperbReads.Application.Common;

namespace SuperbReads.Application.Domain.Entities;

public class Author : AuditableEntity
{
    public const int FullNameMaxLength = 100;
    public const int BioMaxLength = 500;


    public string UserId { get; set; } = null!;

    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    public string? Bio { get; set; }

    public ApplicationUser User { get; set; }
    public List<Post> Posts { get; set; } = [];
    // public List<PostComment> PostComments { get; set; } = [];
}
