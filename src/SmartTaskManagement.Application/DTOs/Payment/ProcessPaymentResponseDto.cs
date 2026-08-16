namespace SmartTaskManagement.Application.DTOs.Payment;

public class ProcessPaymentResponseDto
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
}