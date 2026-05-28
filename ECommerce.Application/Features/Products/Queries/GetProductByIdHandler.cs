namespace ECommerce.Application.Features.Products.Queries.GetProductById;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad a través de la interfaz del repositorio
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        // 2. Si no existe, retornamos null (el controller luego devolverá un 404 Not Found)
        if (product is null)
            return null; 

        // 3. Mapeamos la entidad del dominio al DTO para no exponer la entidad
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }
}