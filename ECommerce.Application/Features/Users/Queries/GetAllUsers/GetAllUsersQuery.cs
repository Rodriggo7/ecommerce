namespace ECommerce.Application.Features.Users.Queries.GetAllUsers;

using MediatR;
using ECommerce.Application.DTOs;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;