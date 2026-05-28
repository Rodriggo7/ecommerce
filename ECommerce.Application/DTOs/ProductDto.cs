namespace ECommerce.Application.DTOs;

public class ProductDto 
{ 
    public Guid Id { get; init; } 
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; } 
    public int Stock { get; init; } 
    public Guid CategoryId { get; init; } 
}