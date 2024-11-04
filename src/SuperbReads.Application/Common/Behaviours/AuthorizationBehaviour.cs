using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>(ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (!authorizeAttributes.Any()) return next();

        // Must be authenticated user
        if (currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }

        // User is authorized / authorization not required
        return next();
    }
}
