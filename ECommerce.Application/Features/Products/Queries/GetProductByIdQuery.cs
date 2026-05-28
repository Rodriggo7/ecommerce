namespace ECommerce.Application.Features.Products.Queries.GetProductById;

using MediatR;
using ECommerce.Application.DTOs;

// Query: solicitud de información. Retorna el DTO.
public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;