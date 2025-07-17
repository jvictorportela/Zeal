using Microsoft.AspNetCore.Mvc;
using Zeal.API.Filters;

namespace Zeal.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}