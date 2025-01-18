using System.ComponentModel.DataAnnotations;

namespace SuperbReads.Application.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public const int IdMaxLength = 450;
    public const int NameMaxLength = 100;
    public const int BioMaxLength = 500;

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    public string? Bio { get; set; }

    public List<Post> Posts { get; set; } = [];
    public List<PostComment> PostComments { get; set; } = [];
}
