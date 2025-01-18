using Microsoft.AspNetCore.Mvc;
using SuperbReads.Application.Features.Authors;

namespace SuperbReads.Api.Controllers;

[Route("api/authors")]
public class AuthorController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuthorDto>>> Get()
    {
        return await Mediator.Send(new GetAuthorsQuery());
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AuthorDto>> GetById(long id)
    {
        return await Mediator.Send(new GetAuthorByIdQuery { Id = id });
    }

    [HttpGet("test")]
    public ActionResult<AuthorDto> Test()
    {
        return new AuthorDto(1, "Test", "Test Bio", DateTime.UtcNow);
    }
}
