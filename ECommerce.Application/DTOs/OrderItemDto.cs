namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public record OrderItemDto(Guid ProductId, int Quantity);