using CoBudget.Communication.Responses;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoBudget.api.Filters;

public class Exceptionfilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        
        if (context.Exception is CoBudgetException)
        {
            HandleException(context);
        } else
        {
            ThrowNewException(context);
        }
        
    }

    private void HandleException(ExceptionContext context)
    {
        if (context.Exception is ValidationException)
        {
            var ex = (ValidationException)context.Exception;

            var errorResponse = new ResponseErrorJson(errorMessage: ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        } else
        {

            var errorResponse = new ResponseErrorJson(errorMessage: context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
    
    private void ThrowNewException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(errorMessage: ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
