using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySqlConnector;

namespace arroba.suino.webapi.Application.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is MySqlException mySqlException)
            {
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Title = "Erro de Conexão",
                    Status = StatusCodes.Status500InternalServerError
                })
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
            else
 if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Title = "Erro desconhecido",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = exception.Message
                })
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}