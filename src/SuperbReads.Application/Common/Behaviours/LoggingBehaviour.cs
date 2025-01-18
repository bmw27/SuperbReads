using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string userId = currentUserService.UserId ?? string.Empty;

        return Task.Run(
            () => _logger.LogInformation(
                "CartCARE Request: {Name} {@UserId} {@Request}",
                requestName,
                userId,
                request),
            cancellationToken);
    }
}
