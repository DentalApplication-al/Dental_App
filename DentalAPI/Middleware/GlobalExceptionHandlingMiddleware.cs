using DentalApplication.Behavior;
using DentalApplication.Errors;
using DentalApplication.Resources;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Text.Json;

namespace DentalAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IStringLocalizer<SharedResource> localizer)
        {
            _next = next;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (ValidationnException ex)
            {
                await ValidationExceptionAsync(context, ex);
            }

            catch (NotFoundException ex)
            {
                await NotFoundExceptionAsync(context, ex);
            }
            catch (NotAuthenticatedException ex)
            {
                await NotAuthenticatedException(context, ex);
            }
            catch (NotAuthorizedException ex)
            {
                await NotAuthorizedException(context, ex);
            }
            catch (BadRequestException ex)
            {
                await BadRequestException(context, ex);
            }
            catch (NotDeletedException ex)
            {
                await NotDeletedException(context, ex);
            }
            catch (EmailNotSentException ex)
            {
                await EmailNotSentException(context, ex);
            }
            catch (ServerError ex)
            {
                await HandleServerError(context, ex);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    await HandleExceptionAsyncDevelopment(context, ex);
                }
                else
                {
                    await HandleExceptionAsync(context, ex, _localizer.Get(Error.SOMETHING_WENT_WRONG));
                }
            }
        }

        private static Task ValidationExceptionAsync(HttpContext context, ValidationnException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ErrorResponse
            {
                errorCode = "Validation failed",
                statusCode = StatusCodes.Status400BadRequest,
                error = exception.Errors[0]
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private static Task NotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new ErrorResponse
            {
                errorCode = "Not found",
                statusCode = StatusCodes.Status404NotFound,
                error = exception.Errors[0]
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private static Task NotAuthenticatedException(HttpContext context, NotAuthenticatedException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var response = new ErrorResponse
            {
                errorCode = "Unauthenticated",
                statusCode = StatusCodes.Status401Unauthorized,
                error = exception.Errors[0]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task NotAuthorizedException(HttpContext context, NotAuthorizedException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var response = new ErrorResponse
            {
                errorCode = "Forbiden",
                statusCode = StatusCodes.Status401Unauthorized,
                error = exception.Errors[0]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task BadRequestException(HttpContext context, BadRequestException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ErrorResponse
            {
                errorCode = "Forbiden",
                statusCode = StatusCodes.Status400BadRequest,
                error = exception.Errors[0]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task NotDeletedException(HttpContext context, NotDeletedException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            var response = new ErrorResponse
            {
                errorCode = "Forbiden",
                statusCode = StatusCodes.Status409Conflict,
                error = exception.Errors[0]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task EmailNotSentException(HttpContext context, EmailNotSentException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status502BadGateway;

            var response = new ErrorResponse
            {
                errorCode = "Email Could not be sent",
                statusCode = StatusCodes.Status502BadGateway,
                error = exception.Errors[0]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ErrorResponse
            {
                errorCode = "An unexpected error occurred.",
                statusCode = StatusCodes.Status500InternalServerError,
                error = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static Task HandleServerError(HttpContext context, ServerError exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ErrorResponse
            {
                errorCode = "Server Error",
                statusCode = StatusCodes.Status500InternalServerError,
                error = exception.Errors.First()
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static Task HandleExceptionAsyncDevelopment(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ErrorResponse
            {
                errorCode = "An unexpected error occurred.",
                statusCode = StatusCodes.Status500InternalServerError,
                error = exception.InnerException.ToString()
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
