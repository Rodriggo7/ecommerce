namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public CreateOrderHandler(
        IOrderRepository orderRepository, 
        IProductRepository productRepository, 
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que el usuario exista
        if (!await _userRepository.ExistsAsync(request.UserId, cancellationToken))
            throw new UserNotFoundException(request.UserId.ToString());;

        // 2. Creamos tu Entidad de Dominio
        var order = new Order(request.UserId);

        // 3. Orquestamos la lógica
        foreach (var itemDto in request.Items)
        {
            // Buscamos el producto en la BD
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId, cancellationToken);
            
            if (product is null)
                throw new ProductNotFoundException(itemDto.ProductId);

            order.AddItem(product, itemDto.Quantity);

            await _productRepository.UpdateAsync(product, cancellationToken);
        }

        // 4. Guardamos la orden completa (EF Core guardará la orden y sus ítems juntos)
        await _orderRepository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}