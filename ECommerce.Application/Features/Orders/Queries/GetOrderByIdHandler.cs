namespace ECommerce.Application.Features.Orders.Queries.GetOrderById;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null) return null;

        // Mapeamos la Entidad del Dominio hacia el DTO de Respuesta
        return new OrderDto
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
        };
    }
}