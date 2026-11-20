using Lab13.Application.UseCases.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ISender _mediator;
    public ClientsController(ISender mediator) => _mediator = mediator;

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var fileBytes = await _mediator.Send(new GetClientsReportQuery());
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Clientes_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }
}