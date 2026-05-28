namespace ECommerce.Application.Features.Users.Commands.RegisterUser;

using MediatR;
using ECommerce.Application.DTOs;

public record RegisterUserCommand(string Email, string Name, string Password) : IRequest<UserDto>;