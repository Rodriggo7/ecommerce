namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

using FluentValidation;

public class CreateProductValidator : AbstractValidator<CreateProductCommand> 
{ 
    public CreateProductValidator() 
    { 
        RuleFor(x => x.Name) 
            .NotEmpty().WithMessage("El nombre es requerido") 
            .MaximumLength(200).WithMessage("Máximo 200 caracteres"); 
            
        RuleFor(x => x.Price) 
            .GreaterThan(0).WithMessage("El precio debe ser positivo"); 
            
        RuleFor(x => x.Stock) 
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
            
        RuleFor(x => x.CategoryId) 
            .NotEmpty().WithMessage("La categoría es requerida"); 
    } 
}