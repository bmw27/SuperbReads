namespace SuperbReads.Application.Common.Interfaces;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; }
}
