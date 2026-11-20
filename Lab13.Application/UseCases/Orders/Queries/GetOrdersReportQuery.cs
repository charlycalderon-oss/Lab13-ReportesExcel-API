using Lab13.Application.DTOs;
using Lab13.Application.Interfaces;
using Lab13.Domain.Repositories;
using MediatR;

namespace Lab13.Application.UseCases.Orders.Queries;

public record GetOrdersReportQuery() : IRequest<byte[]>;

internal sealed class GetOrdersReportQueryHandler : IRequestHandler<GetOrdersReportQuery, byte[]>
{
    private readonly IOrderRepository _repo;
    private readonly IExcelReportService _excel;

    public GetOrdersReportQueryHandler(IOrderRepository repo, IExcelReportService excel)
    {
        _repo = repo;
        _excel = excel;
    }

    public async Task<byte[]> Handle(GetOrdersReportQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetAllWithDetailsAsync();
        var dtos = orders.Select(o => new OrderReportDto(
            o.OrderId,
            o.Client.Name,
            o.OrderDate,
            o.Orderdetails.Sum(d => d.Quantity),
            o.Orderdetails.Sum(d => d.Quantity * d.Product.Price)
        ));
        return _excel.GenerateOrdersReport(dtos);
    }
}