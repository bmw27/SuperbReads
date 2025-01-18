using MediatR;
using Microsoft.Extensions.Logging;

namespace SuperbReads.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
#pragma warning disable S2139
        catch (Exception ex)
#pragma warning restore S2139
        {
            string requestName = typeof(TRequest).Name;

            logger.LogError(
                ex,
                "CartCARE Request: Unhandled Exception for Request {Name} {@Request}",
                requestName,
                request);

            throw;
        }
    }
}
