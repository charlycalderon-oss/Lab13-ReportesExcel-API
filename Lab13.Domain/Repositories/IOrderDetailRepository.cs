using Lab13.Domain.Entities;

namespace Lab13.Domain.Repositories;

public interface IOrderDetailRepository
{
    Task<IEnumerable<Orderdetail>> GetAllDetailedAsync();
}