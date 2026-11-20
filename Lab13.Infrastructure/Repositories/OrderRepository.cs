using Lab13.Domain.Entities;
using Lab13.Domain.Repositories;
using Lab13.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LinqExampleContext _context;
    public OrderRepository(LinqExampleContext context) => _context = context;

    public async Task<IEnumerable<Order>> GetAllWithDetailsAsync() =>
        await _context.Orders
            .Include(o => o.Client)
            .Include(o => o.Orderdetails)
            .ThenInclude(d => d.Product)
            .AsNoTracking()
            .ToListAsync();
}