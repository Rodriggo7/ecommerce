namespace ECommerce.Application.DTOs;

public class OrderDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Status { get; init; } = string.Empty;
    public decimal Total { get; init; }
    public List<OrderItemResponseDto> Items { get; init; } = new();
}