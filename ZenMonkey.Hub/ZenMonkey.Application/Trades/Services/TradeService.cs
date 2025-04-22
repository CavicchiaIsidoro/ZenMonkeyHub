using Microsoft.Extensions.Logging;
using ZenMonkey.Application.Trades.Mappers;
using ZenMonkey.Application.Trades.Services.Interfaces;
using ZenMonkey.Hub.Domain.GeckoTerminal;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface;

namespace ZenMonkey.Application.Trades.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly ILogger<TradeService> _logger;

        public TradeService(ITradeRepository tradeRepository,
            ILogger<TradeService> logger)
        {
            _tradeRepository = tradeRepository;
            _logger = logger;
        }

        public async Task<Root> GetAllAsync()
        {
            try
            {
                var trades = await _tradeRepository.GetAllAsync();
                _logger.Log(LogLevel.Information, "Get all trades succed");
                return trades.ToDomain();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, e.Message);
                return null!;
            }
        }
    }
}
