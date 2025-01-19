namespace SuperbReads.Application.Common;

public abstract class AuditableEntity : BaseEntity
{
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
