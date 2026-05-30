namespace ECommerce.Application.Interfaces;

using ECommerce.Domain.Entities;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken ct = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct = default);
}