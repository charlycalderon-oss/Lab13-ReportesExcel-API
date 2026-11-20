using Lab13.Application.UseCases.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;
    public ProductsController(ISender mediator) => _mediator = mediator;

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var fileBytes = await _mediator.Send(new GetProductsReportQuery());
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Productos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }
}