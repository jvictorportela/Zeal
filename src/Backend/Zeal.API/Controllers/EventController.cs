using Microsoft.AspNetCore.Mvc;
using Zeal.API.Attributes;
using Zeal.Application.UseCases.Event;
using Zeal.Communication.Requests.Event;
using Zeal.Communication.Responses.Error;
using Zeal.Communication.Responses.Event;

namespace Zeal.API.Controllers;

[AuthenticatedUser]
public class EventController : ZealBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterEventUseCase useCase, [FromBody] RequestEventJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
