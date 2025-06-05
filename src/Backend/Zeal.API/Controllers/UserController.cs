using Microsoft.AspNetCore.Mvc;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;

namespace Zeal.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserjson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {

        return Created();
    }
}
