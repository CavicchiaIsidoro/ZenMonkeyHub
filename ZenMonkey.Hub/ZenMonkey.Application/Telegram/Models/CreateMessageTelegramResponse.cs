namespace ZenMonkey.Application.Telegram.Models
{
    public record CreateMessageTelegramResponse(bool IsSuccess, string? Message, DateTime? RequestDateTime);
}
