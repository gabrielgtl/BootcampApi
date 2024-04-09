using Core.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;
    private const string _jsonContentType = "application/json";

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException notFoundEx)
        {
            await HandleNotFoundException(context, notFoundEx);
        }
        catch (BusinessLogicException businessLogicException)
        { 
            await HandleBusinessLogicException(context, businessLogicException); 
        }
        catch (Exception ex)
        {
            await HandleUnexpectedException(context, ex);
        }

    }

    private async Task HandleUnexpectedException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = _jsonContentType;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await Console.Out.WriteLineAsync($"Ha ocurrido un error inesperado. Detalles: {ex.StackTrace}");

        var error = new ErrorModel
        {
            Message = ex.Message
        };

        var errorJson = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(errorJson);
    }

    private async Task HandleNotFoundException(HttpContext context, NotFoundException notFoundEx)
    {
        context.Response.ContentType = _jsonContentType;
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        var error = new ErrorModel
        {
            Message = notFoundEx.Message
        };

        var errorJson = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(errorJson);
    }
    private async Task HandleBusinessLogicException(HttpContext context, BusinessLogicException businessLogicException)
    {
        context.Response.ContentType = _jsonContentType;
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var error = new ErrorModel
        {
            Message = businessLogicException.Message
        };

        var errorJson = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(errorJson);
    }
}