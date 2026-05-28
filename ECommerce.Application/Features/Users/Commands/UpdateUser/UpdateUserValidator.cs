namespace ECommerce.Application.Features.Users.Commands.UpdateUser;
using FluentValidation;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es requerido.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido.");
    }
}