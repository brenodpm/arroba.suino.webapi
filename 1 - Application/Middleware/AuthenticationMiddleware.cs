using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace arroba.suino.webapi.Application.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var dummyCookieValue = context.Request.Cookies["DummyCookies"];
            var isValidDummyCookieValue = Guid.TryParse(dummyCookieValue, out Guid result);

            if (!isValidDummyCookieValue)
            {
                context.Response.Cookies.Append("DummyCookies", Guid.NewGuid().ToString());
            }
            
            return _next(context);
        }
    }
}