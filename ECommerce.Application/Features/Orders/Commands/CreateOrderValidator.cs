namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

using FluentValidation;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("El ID del usuario es requerido.");
        
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("La orden debe tener al menos un producto.")
            .Must(items => items.All(i => i.Quantity > 0))
            .WithMessage("Todas las cantidades deben ser mayores a cero.");
    }
}