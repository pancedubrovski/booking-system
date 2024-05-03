using System.Globalization;

namespace MockBookingSystem.NewFolder
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/text";
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
