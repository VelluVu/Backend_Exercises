using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GameWebApi.Controllers
{
    internal class ShowMessageExceptionAttribute : ExceptionFilterAttribute
    {
        private Type _type;

        public ShowMessageExceptionAttribute ( Type exceptionType )
        {
            _type = exceptionType;
        }

        public override void OnException ( ExceptionContext context )
        {

            if ( !( _type.IsAssignableFrom ( context.Exception.GetType ( ) ) ) )
            {
                return;
            }
            JsonResult error = new JsonResult ( context.Exception.Message );
            error.StatusCode = StatusCodes.Status422UnprocessableEntity;

            context.ExceptionHandled = true;
            context.Result = error;

        }
    }
}