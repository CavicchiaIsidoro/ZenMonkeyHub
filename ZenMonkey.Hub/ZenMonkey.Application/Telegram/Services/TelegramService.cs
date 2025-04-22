using Microsoft.Extensions.Logging;
using System.Globalization;
using ZenMonkey.Application.Telegram.Mappers;
using ZenMonkey.Application.Telegram.Models;
using ZenMonkey.Application.Telegram.Services.Interfaces;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface;
using ZenMonkey.Hub.Infrastructure.Telegram.Interfaces;

namespace ZenMonkey.Application.Telegram.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramRepository _telegramRepository;
        private readonly ITradeRepository _tradeRepository;
        private readonly ILogger<TelegramService> _logger;

        public TelegramService(ITelegramRepository telegramRepository,
            ITradeRepository tradeRepository,
            ILogger<TelegramService> logger)
        {
            _telegramRepository = telegramRepository;
            _tradeRepository = tradeRepository;
            _logger = logger;
        }

        public async Task<CreateMessageTelegramResponse> PostMessageAsync(CreateMessageTelegramRequest request)
        {
            _logger.LogInformation("Send message to telegram");
            var now = DateTime.Now;
            try
            {
                var message = request.ToDomain();

                if (!string.IsNullOrEmpty(request.TradeId))
                {
                    _logger.LogInformation($"TradeId : {request.TradeId}");
                    var root = await _tradeRepository.GetAllAsync();
                    var trade = root.data?.FirstOrDefault(t => t.id == request.TradeId);

                    var usd = Convert.ToDecimal(trade?.attributes?.price_from_in_usd, CultureInfo.InvariantCulture) + 0.00M;
                    var amount = Convert.ToDecimal(trade?.attributes?.from_token_amount, CultureInfo.InvariantCulture);
                    var total = Math.Round((usd * amount), 2);

                    message.Text = $"""
                        Wallet : {trade?.attributes?.tx_from_address}
                        Total : {total} $
                        Token : {amount}

                        """;
                }

                if (string.IsNullOrEmpty(message.Text))
                    throw new ArgumentNullException(nameof(message.Text));

                if (string.IsNullOrEmpty(message.ImgUrl))
                    await _telegramRepository.PostMessageAsync(message);
                else
                    await _telegramRepository.PostMessageWithImgAsync(message);

                return new CreateMessageTelegramResponse(true, null, now);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + "\n" + e.InnerException!.ToString());
                return new CreateMessageTelegramResponse(false, e.Message, now);
            }
        }
        
    }
}
