using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Json;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Models;

namespace ZenMonkey.Hub.Infrastructure.GeckoTerminal.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TradeRepository> _logger;
        private string _network;
        private string _url;
        private string _address;

        public TradeRepository(IConfiguration configuration,
            ILogger<TradeRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _url = _configuration["GeckoTerminal:Url"] ?? throw new NullReferenceException("No config for GeckoTerminal");
            _network = _configuration["GeckoTerminal:Network"] ?? throw new NullReferenceException("No config for GeckoTerminal");
            _address = _configuration["GeckoTerminal:Address"] ?? throw new NullReferenceException("No config for GeckoTerminal");
        }

        public async Task<Root> GetAllAsync()
        {
            _logger.Log(LogLevel.Information, "Get all trades begin");
            var url = $"{_url}/{_network}/pools/{_address}/trades?trade_volume_in_usd_greater_than=100";
            var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url);
            var root = await response.Content.ReadFromJsonAsync<Root>();

            if (root == null)
                throw new NullReferenceException(nameof(root));

            return root;
        }
    }
}
