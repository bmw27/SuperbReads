using SuperbReads.Application.Common;
using SuperbReads.Application.Common.Interfaces;
using SuperbReads.Application.Common.Models;

namespace SuperbReads.Application.Infrastructure.Services;

public class DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator) : IDomainEventService
{
    public Task Publish(DomainEvent domainEvent)
    {
        logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        return mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}
