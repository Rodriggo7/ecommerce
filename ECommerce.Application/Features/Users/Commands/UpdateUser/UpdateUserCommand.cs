namespace ECommerce.Application.Features.Users.Commands.UpdateUser;
using MediatR;

public record UpdateUserCommand(Guid Id, string Name) : IRequest;