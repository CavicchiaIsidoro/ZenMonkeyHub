using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using ZenMonkey.Hub.Domain.Telegram;
using ZenMonkey.Hub.Infrastructure.Telegram.Interfaces;

namespace ZenMonkey.Hub.Infrastructure.Telegram.Repository
{
    public class TelegramRepository : ITelegramRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly long _chatId;
        private readonly string _token;

        public TelegramRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["Telegram:Url"] ?? throw new NullReferenceException("No config for Telegram");
            _chatId = long.Parse(_configuration["Telegram:ChatId"] ?? throw new NullReferenceException("No config for Telegram"));
            _token = _configuration["Telegram:Token"] ?? throw new NullReferenceException("No config for Telegram");
        }
        public async Task PostMessageAsync(Message message)
        {
            var botClient = new TelegramBotClient(_token);
            await botClient.SendMessage(_chatId, message.Text!);
            await botClient.Close();
        }

        public async Task PostMessageWithImgAsync(Message message)
        {
            var botClient = new TelegramBotClient(_token);
            await botClient.SendPhoto(_chatId, message.ImgUrl!, message.Text);
            await botClient.Close();
        }
    }
}
