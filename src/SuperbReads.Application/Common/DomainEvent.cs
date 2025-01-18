using System.Collections.ObjectModel;

namespace SuperbReads.Application.Common;

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}

public interface IHasDomainEvent
{
    public Collection<DomainEvent> DomainEvents { get; }
}
