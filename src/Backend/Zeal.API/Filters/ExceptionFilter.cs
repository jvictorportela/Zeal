using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Zeal.Communication.Responses.Error;
using Zeal.Exceptions;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ZealException)
            HandleProjectException(context);
        else
            ThrowUnknowException(context);
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is InvalidLoginException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
        }

        else if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
        }
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.UNKNOW_ERROR));
    }
}

