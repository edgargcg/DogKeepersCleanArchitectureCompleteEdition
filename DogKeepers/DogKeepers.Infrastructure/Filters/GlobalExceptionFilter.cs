using DogKeepers.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DogKeepers.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)filterContext.Exception;
                var validation = new
                {
                    status = 400,
                    title = "Bad request",
                    detail = exception.Message
                };

                var json = new
                {
                    errors = new[] { validation }
                };

                filterContext.Result = new BadRequestObjectResult(json);
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
