using Lab13.Application.DTOs;
using Lab13.Application.Interfaces;
using Lab13.Domain.Repositories;
using MediatR;

namespace Lab13.Application.UseCases.Clients.Queries;

public record GetClientsReportQuery() : IRequest<byte[]>;

internal sealed class GetClientsReportQueryHandler : IRequestHandler<GetClientsReportQuery, byte[]>
{
    private readonly IClientRepository _repo;
    private readonly IExcelReportService _excel;

    public GetClientsReportQueryHandler(IClientRepository repo, IExcelReportService excel)
    {
        _repo = repo;
        _excel = excel;
    }

    public async Task<byte[]> Handle(GetClientsReportQuery request, CancellationToken cancellationToken)
    {
        var clients = await _repo.GetAllWithOrdersAsync();
        var dtos = clients.Select(c => new ClientReportDto(c.ClientId, c.Name, c.Email, c.Orders.Count));
        return _excel.GenerateClientsReport(dtos);
    }
}