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
    public IActionResult Register([FromBody] RequestRegisterUserJson request, [FromServices] RegisterUserUseCase useCase)
    {
        var result = useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
