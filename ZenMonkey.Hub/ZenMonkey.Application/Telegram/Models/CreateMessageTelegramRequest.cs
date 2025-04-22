namespace ZenMonkey.Application.Telegram.Models
{
    public class CreateMessageTelegramRequest
    {
        public string? TradeId { get; set; }
        public string? ImgUrl { get; set; }
        public string? Message { get; set; }
    }
}
