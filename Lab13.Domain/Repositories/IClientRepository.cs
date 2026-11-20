using Lab13.Domain.Entities;

namespace Lab13.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllWithOrdersAsync();
}