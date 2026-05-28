namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

using MediatR;
using ECommerce.Application.Interfaces;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
            throw new InvalidOperationException("El producto que intenta eliminar no existe.");

        await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}