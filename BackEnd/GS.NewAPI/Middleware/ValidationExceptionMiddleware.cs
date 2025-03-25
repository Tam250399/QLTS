using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace GS.NewAPI.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                // Handle the ValidationException and return a 400 BadRequest with the validation errors
                httpContext.Response.StatusCode = 400; // Bad Request
                var errorMessages = ex.Errors.Select(e => e.ErrorMessage);
                await httpContext.Response.WriteAsync($"Validation failed: {string.Join(", ", errorMessages)}");
            }
        }
    }
}
