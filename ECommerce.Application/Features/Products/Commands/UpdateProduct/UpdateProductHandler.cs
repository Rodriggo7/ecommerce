namespace ECommerce.Application.Features.Products.Commands.UpdateProduct;

using MediatR;
using ECommerce.Application.Interfaces;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar la entidad existente
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        // 2. Si no existe, lanzamos una excepción controlada (nuestro GlobalException la transformará en un 422 o 404)
        if (product is null)
            throw new InvalidOperationException("El producto que intenta actualizar no existe.");

        // 3. Ejecutar el método del dominio aplicando las reglas de negocio
        product.UpdateDetails(request.Name, request.Description, request.Price, request.Stock, request.CategoryId);

        // 4. Persistir los cambios
        await _repository.UpdateAsync(product, cancellationToken);
    }
}