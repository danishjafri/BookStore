using AutoMapper;
using BookStore.API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.API.Utilities
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = "Internal Server Error from the custom middleware.";
            object controller;
            context.Request.RouteValues.TryGetValue("controller", out controller);
            object action;
            context.Request.RouteValues.TryGetValue("action", out action);
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(DbUpdateException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = "Failed to update the database.";
            }

            if (exception.GetType() == typeof(ArgumentException) || exception.GetType() == typeof(ArgumentNullException) || exception.GetType() == typeof(InvalidOperationException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = "Request contained information that was not valid.";
            }

            if (exception.GetType() == typeof(AutoMapperMappingException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.FailedDependency;
                message = "Error encountered by the server during process.";
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            _logger.LogError($"Controller: {controller}; Action: {action}; Message: {exception.Message}; Inner Exception: {exception.InnerException}");

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}