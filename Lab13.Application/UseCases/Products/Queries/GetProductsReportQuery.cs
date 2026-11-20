using Lab13.Application.DTOs;
using Lab13.Application.Interfaces;
using Lab13.Domain.Repositories;
using MediatR;

namespace Lab13.Application.UseCases.Products.Queries;

public record GetProductsReportQuery() : IRequest<byte[]>;

internal sealed class GetProductsReportQueryHandler : IRequestHandler<GetProductsReportQuery, byte[]>
{
    private readonly IProductRepository _repo;
    private readonly IExcelReportService _excel;

    public GetProductsReportQueryHandler(IProductRepository repo, IExcelReportService excel)
    {
        _repo = repo;
        _excel = excel;
    }

    public async Task<byte[]> Handle(GetProductsReportQuery request, CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync();
        var dtos = products.Select(p => new ProductReportDto(p.ProductId, p.Name, p.Description ?? "Sin descripción", p.Price));
        return _excel.GenerateProductsReport(dtos);
    }
}