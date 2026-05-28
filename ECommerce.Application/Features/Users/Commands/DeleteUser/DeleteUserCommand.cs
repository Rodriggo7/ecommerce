namespace ECommerce.Application.Features.Users.Commands.DeleteUser;
using MediatR;

public record DeleteUserCommand(Guid Id) : IRequest;