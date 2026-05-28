namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

using MediatR;

// Command: mensaje que expresa una intención
public record CreateProductCommand(
    string Name, 
    string Description,
    decimal Price,
    int Stock, 
    Guid CategoryId) : IRequest<CreateProductResponse>;

// Response: confirmación con los datos del resultado
public record CreateProductResponse(Guid Id, string Name, decimal Price);