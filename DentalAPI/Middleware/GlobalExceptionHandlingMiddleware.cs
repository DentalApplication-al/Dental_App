using DentalApplication.Behavior;
using DentalApplication.Errors;
using System.Text.Json;

namespace DentalAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationnException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }

            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (NotAuthenticatedException ex)
            {
                await HandleNotAuthenticatedException(context, ex);
            }
            catch(NotAuthorizedException ex)
            {
                await HandleNotAuthorizedException(context, ex);
            }
            catch (BadRequestException ex)
            {
                await HandleBadRequestException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationnException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ErrorResponse
            {
                Error = "Validation failed",
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = exception.Errors
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private static Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new ErrorResponse
            {
                Error = "Not found",
                StatusCode = StatusCodes.Status404NotFound,
                Errors = exception.Errors
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private static Task HandleNotAuthenticatedException(HttpContext context, NotAuthenticatedException exception) 
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode= StatusCodes.Status401Unauthorized;

            var response = new ErrorResponse
            {
                Error = "Unauthenticated",
                StatusCode= StatusCodes.Status401Unauthorized,
                Errors = exception.Errors
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task HandleNotAuthorizedException(HttpContext context, NotAuthorizedException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var response = new ErrorResponse
            {
                Error = "Forbiden",
                StatusCode = StatusCodes.Status401Unauthorized,
                Errors = exception.Errors
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task HandleBadRequestException(HttpContext context, BadRequestException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ErrorResponse
            {
                Error = "Forbiden",
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = exception.Errors
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorResponse
            {
                Error = "An unexpected error occurred.",
                StatusCode = StatusCodes.Status500InternalServerError,
                Errors = ["Somethig went wrong."]
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
