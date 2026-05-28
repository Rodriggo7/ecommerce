namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _repository;

    // Inyectamos la INTERFAZ, nunca la implementación de EF Core
    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Instanciar la entidad del dominio
        var product = new Product(
            request.Name, 
            request.Description, 
            request.Price, 
            request.Stock, 
            request.CategoryId);

        // 2. Persistir a través del contrato
        await _repository.AddAsync(product, cancellationToken);

        // 3. Retornar el DTO/Record, NUNCA la entidad de dominio
        return new CreateProductResponse(product.Id, product.Name, product.Price);
    }
}