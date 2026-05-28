namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

using MediatR;
using ECommerce.Application.DTOs;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;