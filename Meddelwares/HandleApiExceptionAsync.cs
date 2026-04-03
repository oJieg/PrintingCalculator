using printing_calculator.Exceptions;

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
        catch (ApiException apiEx)
        {
            _logger.LogWarning(apiEx, "API Exception caught: {ErrorMessage}", apiEx.ErrorMessage);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)apiEx.StatusCode;
            await context.Response.WriteAsync(apiEx.ErrorMessage);
        }
    }
}