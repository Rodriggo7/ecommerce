namespace ECommerce.Application.Features.Orders.Queries.GetOrderById;

using MediatR;
using ECommerce.Application.DTOs;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;