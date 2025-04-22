using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface;
using ZenMonkey.Hub.Infrastructure.Telegram.Interfaces;

namespace ZenMonkey.Application.Telegram.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly ITelegramRepository _telegramRepository;
        private readonly ILogger<BotService> _logger;
        private Timer? _timer = null;

        public BotService(IServiceScopeFactory serviceScopeFactory)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            _logger = scope.ServiceProvider.GetRequiredService<ILogger<BotService>>();
            _tradeRepository = scope.ServiceProvider.GetRequiredService<ITradeRepository>();
            _telegramRepository = scope.ServiceProvider.GetRequiredService<ITelegramRepository>();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {           
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            await Task.CompletedTask;
        }

        private async void DoWork(object? obj)
        {
            try
            {
                var root = await _tradeRepository.GetAllAsync();

                if (root.data == null)
                    throw new NullReferenceException(nameof(root.data));

                foreach (var trade in root.data)
                {
                    _logger.LogInformation($"Send trade for {trade.id}");
                    var usd = Convert.ToDecimal(trade?.attributes?.price_from_in_usd, CultureInfo.InvariantCulture) + 0.00M;
                    var amount = Convert.ToDecimal(trade?.attributes?.from_token_amount, CultureInfo.InvariantCulture);
                    var total = Math.Round((usd * amount), 2);

                    var text = $"""
                        Wallet : {trade?.attributes?.tx_from_address}
                        Total : {total} $
                        Token : {amount}

                        """;

                    await _telegramRepository.PostMessageWithImgAsync(new Hub.Domain.Telegram.Message()
                    {
                        ImgUrl = "https://image.noelshack.com/fichiers/2025/16/3/1744811378-opa26yx.jpg",
                        Text = text
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + "\n" + e.InnerException!.Message);
            }
            
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop job");
            _timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }
    }
}
