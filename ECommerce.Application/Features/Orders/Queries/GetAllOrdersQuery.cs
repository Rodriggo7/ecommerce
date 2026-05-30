namespace ECommerce.Application.Features.Orders.Queries.GetAllOrders;

using MediatR;
using ECommerce.Application.DTOs;

public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderDto>>;