using ClosedXML.Excel;
using Lab13.Application.DTOs;
using Lab13.Application.Interfaces;

namespace Lab13.Infrastructure.Services;

public class ExcelReportService : IExcelReportService
{
    public byte[] GenerateClientsReport(IEnumerable<ClientReportDto> clients)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Clientes");

        ws.Cell(1, 1).Value = "ID Cliente";
        ws.Cell(1, 2).Value = "Nombre";
        ws.Cell(1, 3).Value = "Correo Electrónico";
        ws.Cell(1, 4).Value = "Total Órdenes";
        StyleHeader(ws.Row(1));

        int row = 2;
        foreach (var item in clients)
        {
            ws.Cell(row, 1).Value = item.ClientId;
            ws.Cell(row, 2).Value = item.Name;
            ws.Cell(row, 3).Value = item.Email;
            ws.Cell(row, 4).Value = item.TotalOrders;
            row++;
        }

        FormatSheet(ws, row - 1, 4);
        return SaveToBytes(workbook);
    }

    public byte[] GenerateProductsReport(IEnumerable<ProductReportDto> products)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Productos");

        ws.Cell(1, 1).Value = "ID Producto";
        ws.Cell(1, 2).Value = "Nombre";
        ws.Cell(1, 3).Value = "Descripción";
        ws.Cell(1, 4).Value = "Precio (S/)";
        StyleHeader(ws.Row(1));

        int row = 2;
        foreach (var item in products)
        {
            ws.Cell(row, 1).Value = item.ProductId;
            ws.Cell(row, 2).Value = item.Name;
            ws.Cell(row, 3).Value = item.Description;
            ws.Cell(row, 4).Value = item.Price;
            ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0.00";
            row++;
        }

        FormatSheet(ws, row - 1, 4);
        return SaveToBytes(workbook);
    }

    public byte[] GenerateOrdersReport(IEnumerable<OrderReportDto> orders)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Ordenes");

        ws.Cell(1, 1).Value = "ID Orden";
        ws.Cell(1, 2).Value = "Cliente";
        ws.Cell(1, 3).Value = "Fecha Orden";
        ws.Cell(1, 4).Value = "Cant. Items";
        ws.Cell(1, 5).Value = "Monto Total (S/)";
        StyleHeader(ws.Row(1));

        int row = 2;
        foreach (var item in orders)
        {
            ws.Cell(row, 1).Value = item.OrderId;
            ws.Cell(row, 2).Value = item.ClientName;
            ws.Cell(row, 3).Value = item.OrderDate.ToString("yyyy-MM-dd HH:mm");
            ws.Cell(row, 4).Value = item.ItemsCount;
            ws.Cell(row, 5).Value = item.TotalAmount;
            ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
            row++;
        }

        FormatSheet(ws, row - 1, 5);
        return SaveToBytes(workbook);
    }

    public byte[] GenerateOrderDetailsReport(IEnumerable<OrderDetailReportDto> details)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("DetalleOrdenes");

        ws.Cell(1, 1).Value = "ID Detalle";
        ws.Cell(1, 2).Value = "ID Orden";
        ws.Cell(1, 3).Value = "Cliente";
        ws.Cell(1, 4).Value = "Producto";
        ws.Cell(1, 5).Value = "Cantidad";
        ws.Cell(1, 6).Value = "Precio Unit.";
        ws.Cell(1, 7).Value = "Subtotal";
        StyleHeader(ws.Row(1));

        int row = 2;
        foreach (var item in details)
        {
            ws.Cell(row, 1).Value = item.OrderDetailId;
            ws.Cell(row, 2).Value = item.OrderId;
            ws.Cell(row, 3).Value = item.ClientName;
            ws.Cell(row, 4).Value = item.ProductName;
            ws.Cell(row, 5).Value = item.Quantity;
            ws.Cell(row, 6).Value = item.UnitPrice;
            ws.Cell(row, 7).Value = item.Subtotal;
            ws.Cell(row, 6).Style.NumberFormat.Format = "#,##0.00";
            ws.Cell(row, 7).Style.NumberFormat.Format = "#,##0.00";
            row++;
        }

        FormatSheet(ws, row - 1, 7);
        return SaveToBytes(workbook);
    }

    private static void StyleHeader(IXLRow headerRow)
    {
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Font.FontColor = XLColor.White;
        headerRow.Style.Fill.BackgroundColor = XLColor.DarkBlue;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static void FormatSheet(IXLWorksheet ws, int lastRow, int lastCol)
    {
        if (lastRow >= 2)
        {
            ws.Range(1, 1, lastRow, lastCol).CreateTable();
        }
        ws.Columns().AdjustToContents();
    }

    private static byte[] SaveToBytes(XLWorkbook workbook)
    {
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}