namespace Lab13.Application.DTOs;
public record OrderReportDto(int OrderId, string ClientName, DateTime OrderDate, int ItemsCount, decimal TotalAmount);