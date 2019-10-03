using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZVSE_Scoreboard.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware ( RequestDelegate next )
        {
            _next = next;
        }
        public async Task Invoke ( HttpContext context )
        {
            try
            {
                await _next ( context );
            }
            catch ( Exception e )
            {
                throw new NotFoundException ( "Not Found ! ", context );
            }
        }
    }
}
