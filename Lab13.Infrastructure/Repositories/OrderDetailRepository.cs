using Lab13.Domain.Entities;
using Lab13.Domain.Repositories;
using Lab13.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly LinqExampleContext _context;
    public OrderDetailRepository(LinqExampleContext context) => _context = context;

    public async Task<IEnumerable<Orderdetail>> GetAllDetailedAsync() =>
        await _context.Orderdetails
            .Include(d => d.Order)
            .ThenInclude(o => o.Client)
            .Include(d => d.Product)
            .AsNoTracking()
            .ToListAsync();
}