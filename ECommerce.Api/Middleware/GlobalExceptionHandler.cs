namespace ECommerce.Api.Middleware;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        // 1. Manejo de Errores de Validación (FluentValidation)
        if (exception is ValidationException fluentException)
        {
            problemDetails.Title = "Error de Validación";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = "Se encontraron uno o más errores de validación en la petición.";
            // Detalle exacto de qué campos fallaron
            problemDetails.Extensions["errors"] = fluentException.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
        }
        // 2. Manejo de Errores del Dominio
        else if (exception is ArgumentException || exception is InvalidOperationException)
        {
            problemDetails.Title = "Regla de negocio violada";
            problemDetails.Status = StatusCodes.Status422UnprocessableEntity; 
            problemDetails.Detail = exception.Message;
        }
        // 3. Cualquier otro error no controlado
        else
        {
            problemDetails.Title = "Error interno del servidor";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = "Ha ocurrido un error inesperado. Por favor, contacte a soporte.";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/problem+json"; // Estándar RFC 7807

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; // Indicamos que el error fue manejado y no debe romper la app
    }
}