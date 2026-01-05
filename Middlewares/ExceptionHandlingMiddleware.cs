
using ChineseAuctionAPI.Models.Exceptions;
namespace ChineseAuctionAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                if (ex is ErrorResponse errorEx)
                {
                  
                    _logger.LogWarning("HTTP {Method} Business Logic Error: {Message} | Status: {StatusCode} | Detiled: {DetailedMessage} | Func: {Func} | Location: {Location} ",
                       errorEx.Method, errorEx.Message, errorEx.StatusCode, errorEx.DetailedMessage, errorEx.Func, errorEx.Location);
                    
                }
                else
                {
                    
                    _logger.LogError(ex, "An unhandled system exception occurred. RequestId: {RequestId}",
                        context.TraceIdentifier);
                }
               

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            object responsePayload;
            if (exception is ErrorResponse errorResponse)
            {
                context.Response.StatusCode = errorResponse.StatusCode;

                
                responsePayload = new
                {
                    errorResponse.Message,
                    errorResponse.StatusCode,
                    Timestamp = DateTime.UtcNow 
                };
            }
            else
            {
                context.Response.StatusCode = 500;
                responsePayload = new { Message = "Internal Server Error" };
            }

            return context.Response.WriteAsJsonAsync(responsePayload);
        }

    }
}
