namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

using MediatR;

public record DeleteProductCommand(Guid Id) : IRequest;