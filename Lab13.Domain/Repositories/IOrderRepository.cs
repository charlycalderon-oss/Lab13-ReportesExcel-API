using Lab13.Domain.Entities;

namespace Lab13.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllWithDetailsAsync();
}