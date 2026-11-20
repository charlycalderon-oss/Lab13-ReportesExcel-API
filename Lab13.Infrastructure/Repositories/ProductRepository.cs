using Lab13.Domain.Entities;
using Lab13.Domain.Repositories;
using Lab13.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LinqExampleContext _context;
    public ProductRepository(LinqExampleContext context) => _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.AsNoTracking().ToListAsync();
}