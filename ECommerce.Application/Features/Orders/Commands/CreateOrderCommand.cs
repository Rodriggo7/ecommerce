namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

using MediatR;

// Recibe el ID del usuario que compra y una lista de los productos que quiere
public record CreateOrderCommand(Guid UserId, List<OrderItemDto> Items) : IRequest<Guid>;