namespace ECommerce.Application.Features.Products.Commands.UpdateProduct;

using MediatR;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId) : IRequest;