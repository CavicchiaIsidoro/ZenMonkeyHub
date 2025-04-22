using ZenMonkey.Application.Telegram.Models;

namespace ZenMonkey.Application.Telegram.Services.Interfaces
{
    public interface ITelegramService
    {
        Task<CreateMessageTelegramResponse> PostMessageAsync(CreateMessageTelegramRequest request);
    }
}
