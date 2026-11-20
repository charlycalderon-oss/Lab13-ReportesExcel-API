using Lab13.Domain.Entities;

namespace Lab13.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}