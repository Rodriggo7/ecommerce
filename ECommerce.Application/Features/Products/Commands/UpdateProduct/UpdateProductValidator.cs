namespace ECommerce.Application.Features.Products.Commands.UpdateProduct;

using FluentValidation;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El Id del producto es requerido.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido.").MaximumLength(200);
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("La categoría es requerida.");
    }
}