namespace ECommerce.Application.Features.Orders.Queries.GetAllOrders;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        // Mapeamos la colección de Entidades a DTOs
        return orders.Select(order => new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            Total = order.Total,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Subtotal = i.Subtotal
            }).ToList()
        });
    }
}