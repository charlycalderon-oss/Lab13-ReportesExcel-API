using Lab13.Domain.Entities;
using Lab13.Domain.Repositories;
using Lab13.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly LinqExampleContext _context;
    public ClientRepository(LinqExampleContext context) => _context = context;

    public async Task<IEnumerable<Client>> GetAllWithOrdersAsync() =>
        await _context.Clients.Include(c => c.Orders).AsNoTracking().ToListAsync();
}