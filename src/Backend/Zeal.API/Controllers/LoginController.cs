using Microsoft.AspNetCore.Mvc;
using Zeal.Application.UseCases.User.Login.DoLogin;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.Error;
using Zeal.Communication.Responses.User;

namespace Zeal.API.Controllers;

public class LoginController : ZealBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserjson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}