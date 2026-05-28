namespace ECommerce.Application.Behaviors;

using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull 
{ 
    private readonly IEnumerable<IValidator<TRequest>> _validators; 
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) 
    { 
        // Si no hay validadores registrados para este tipo, continuar
        if (!_validators.Any()) 
            return await next(); 
            
        var context = new ValidationContext<TRequest>(request); 
        
        // Ejecutar todas las validaciones y recolectar errores
        var failures = _validators 
            .Select(v => v.Validate(context)) 
            .SelectMany(result => result.Errors) 
            .Where(f => f != null) 
            .ToList(); 
            
        // Si hay errores, lanzar excepción (que luego atrapará el GlobalExceptionHandler)
        if (failures.Any()) 
            throw new ValidationException(failures); 
            
        // Si todo está ok, avanzar al Handler
        return await next(); 
    } 
}