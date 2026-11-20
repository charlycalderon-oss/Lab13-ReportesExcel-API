using Lab13.Application.UseCases.OrderDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly ISender _mediator;
    public OrderDetailsController(ISender mediator) => _mediator = mediator;

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var fileBytes = await _mediator.Send(new GetOrderDetailsReportQuery());
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_DetalleOrdenes_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }
}