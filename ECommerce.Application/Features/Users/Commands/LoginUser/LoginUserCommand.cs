namespace ECommerce.Application.Features.Users.Commands.LoginUser;

using MediatR;

// Se retorna un string que es el token JWT generado para el usuario autenticado
public record LoginUserCommand(string Email, string Password) : IRequest<string>;