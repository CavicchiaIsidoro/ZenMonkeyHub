namespace ZenMonkey.Hub.Infrastructure.Telegram.Interfaces
{
    public interface ITelegramRepository
    {
        Task PostMessageAsync(Domain.Telegram.Message message);
        Task PostMessageWithImgAsync(Domain.Telegram.Message message);
    }
}
