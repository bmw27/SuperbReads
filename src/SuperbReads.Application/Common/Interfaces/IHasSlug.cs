using System.ComponentModel.DataAnnotations;

namespace SuperbReads.Application.Common.Interfaces;

public interface IHasSlug
{
    [MaxLength(Common.Slug.MaxLength)]
    public string Slug { get; set; }
}
