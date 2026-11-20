namespace Lab13.Application.DTOs;
public record OrderDetailReportDto(int OrderDetailId, int OrderId, string ClientName, string ProductName, int Quantity, decimal UnitPrice, decimal Subtotal);