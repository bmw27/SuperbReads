using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SuperbReads.Api;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
}
