namespace ECommerce.Application.Features.Users.Queries.GetUserById;

using MediatR;
using ECommerce.Application.DTOs;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;