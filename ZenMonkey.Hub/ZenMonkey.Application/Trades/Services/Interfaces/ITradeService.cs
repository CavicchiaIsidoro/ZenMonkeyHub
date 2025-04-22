using ZenMonkey.Hub.Domain.GeckoTerminal;

namespace ZenMonkey.Application.Trades.Services.Interfaces
{
    public interface ITradeService
    {
        Task<Root> GetAllAsync();
    }
}
