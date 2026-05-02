using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Threading.Tasks;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Exceptions;

namespace AgileAi.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessValidationException ex)
            {
                await WriteError(context, HttpStatusCode.BadRequest, ex.Message, ex.Code);
            }
            catch (ResourceConflictException ex)
            {
                await WriteError(context, HttpStatusCode.Conflict, ex.Message, ex.Code);
            }
            catch (DbUpdateException)
            {
                await WriteError(context, HttpStatusCode.Conflict, "Database update failed because of a conflicting or invalid record.", "DATABASE_CONFLICT");
            }
            catch (SecurityTokenException)
            {
                await WriteError(context, HttpStatusCode.Unauthorized, "Invalid or expired token.", "INVALID_TOKEN");
            }
            catch (Exception)
            {
                await WriteError(context, HttpStatusCode.InternalServerError, "An unexpected server error occurred.", "SERVER_ERROR");
            }
        }

        private static async Task WriteError(HttpContext context, HttpStatusCode statusCode, string message, string code)
        {
            if (context.Response.HasStarted)
                return;

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResponse
            {
                Message = message,
                Code = code
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
