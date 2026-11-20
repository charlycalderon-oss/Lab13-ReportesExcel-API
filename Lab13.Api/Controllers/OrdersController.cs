using Lab13.Application.UseCases.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _mediator;
    public OrdersController(ISender mediator) => _mediator = mediator;

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var fileBytes = await _mediator.Send(new GetOrdersReportQuery());
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Ordenes_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }
}