namespace ECommerce.Application.DTOs;

public class OrderItemResponseDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Subtotal { get; init; }
}