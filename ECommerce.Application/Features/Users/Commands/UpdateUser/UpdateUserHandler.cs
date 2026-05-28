namespace ECommerce.Application.Features.Users.Commands.UpdateUser;
using MediatR;
using ECommerce.Application.Interfaces;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (user is null)
            throw new InvalidOperationException("El usuario no existe.");

        user.UpdateProfile(request.Name);
        await _repository.UpdateAsync(user, cancellationToken);
    }
}