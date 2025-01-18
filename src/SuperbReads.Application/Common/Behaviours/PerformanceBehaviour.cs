using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500)
        {
            return response;
        }

        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.UserId ?? string.Empty;

        logger.LogWarning(
            "CartCARE Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
            requestName,
            elapsedMilliseconds,
            userId,
            request);

        return response;
    }
}
