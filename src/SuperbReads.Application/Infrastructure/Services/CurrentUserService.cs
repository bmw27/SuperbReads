using System.Security.Claims;
using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
}
