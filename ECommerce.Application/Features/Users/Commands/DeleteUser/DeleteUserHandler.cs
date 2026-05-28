namespace ECommerce.Application.Features.Users.Commands.DeleteUser;
using MediatR;
using ECommerce.Application.Interfaces;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(request.Id, cancellationToken))
            throw new InvalidOperationException("El usuario no existe.");

        await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}