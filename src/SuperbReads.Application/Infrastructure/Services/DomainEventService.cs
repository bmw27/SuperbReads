using MediatR;
using Microsoft.Extensions.Logging;
using SuperbReads.Application.Common;
using SuperbReads.Application.Common.Interfaces;
using SuperbReads.Application.Common.Models;

namespace SuperbReads.Application.Infrastructure.Services;

public class DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator) : IDomainEventService
{
    public Task Publish(DomainEvent domainEvent)
    {
        logger.DomainEventPublishing(domainEvent.GetType().Name);
        return mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}

public static partial class DomainEventServiceLoggerExtensions
{
    [LoggerMessage(
        EventId = 0,
        EventName = "DomainEventPublished",
        Level = LogLevel.Information,
        Message = "Publishing domain event. Event - {DomainEventName}")]
    public static partial void DomainEventPublishing(this ILogger logger, string domainEventName);
}
