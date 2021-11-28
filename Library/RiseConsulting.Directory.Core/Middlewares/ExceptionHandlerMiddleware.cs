using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.Core.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                // Hata Yönetimi
            }
        }
    }
}