using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.BadRequest;

            var problemDetails =
                new ProblemDetails
                {
                    Status = 400,

                    Title = "Request Failed",

                    Detail = ex.Message
                };

            await context.Response
                .WriteAsJsonAsync(problemDetails);
        }
    }
}