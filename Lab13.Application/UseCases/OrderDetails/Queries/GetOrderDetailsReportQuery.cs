using Lab13.Application.DTOs;
using Lab13.Application.Interfaces;
using Lab13.Domain.Repositories;
using MediatR;

namespace Lab13.Application.UseCases.OrderDetails.Queries;

public record GetOrderDetailsReportQuery() : IRequest<byte[]>;

internal sealed class GetOrderDetailsReportQueryHandler : IRequestHandler<GetOrderDetailsReportQuery, byte[]>
{
    private readonly IOrderDetailRepository _repo;
    private readonly IExcelReportService _excel;

    public GetOrderDetailsReportQueryHandler(IOrderDetailRepository repo, IExcelReportService excel)
    {
        _repo = repo;
        _excel = excel;
    }

    public async Task<byte[]> Handle(GetOrderDetailsReportQuery request, CancellationToken cancellationToken)
    {
        var details = await _repo.GetAllDetailedAsync();
        var dtos = details.Select(d => new OrderDetailReportDto(
            d.OrderDetailId,
            d.OrderId,
            d.Order.Client.Name,
            d.Product.Name,
            d.Quantity,
            d.Product.Price,
            d.Quantity * d.Product.Price
        ));
        return _excel.GenerateOrderDetailsReport(dtos);
    }
}