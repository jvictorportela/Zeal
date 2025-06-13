using Microsoft.AspNetCore.Mvc;
using Zeal.Application.UseCases.User.Register;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;

namespace Zeal.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserjson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}