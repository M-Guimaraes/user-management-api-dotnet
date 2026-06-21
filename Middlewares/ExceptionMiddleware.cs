using UserManagementApi.DTOs;
using UserManagementApi.Exceptions;

namespace UserManagementApi.Middlewares;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            logger.LogWarning(ex, ex.Message);

            await HandleAppExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            await HandleUnexpectedExceptionAsync(context);
        }
    }

    private static async Task HandleAppExceptionAsync(
        HttpContext context,
        AppException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
        new ErrorResponseDto
        {
            StatusCode = exception.StatusCode,
            Message = exception.Message,
            TraceId = context.TraceIdentifier,
        });
    }

    private static async Task HandleUnexpectedExceptionAsync(
        HttpContext context)
    {
        context.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
        new ErrorResponseDto
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An unexpected error occurred.",
        });
    }
}
