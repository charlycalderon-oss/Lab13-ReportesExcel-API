using Lab13.Application.DTOs;

namespace Lab13.Application.Interfaces;

public interface IExcelReportService
{
    byte[] GenerateClientsReport(IEnumerable<ClientReportDto> clients);
    byte[] GenerateProductsReport(IEnumerable<ProductReportDto> products);
    byte[] GenerateOrdersReport(IEnumerable<OrderReportDto> orders);
    byte[] GenerateOrderDetailsReport(IEnumerable<OrderDetailReportDto> details);
}