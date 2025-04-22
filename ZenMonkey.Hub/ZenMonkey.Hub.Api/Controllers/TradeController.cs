using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ZenMonkey.Application.Trades.Services.Interfaces;
using ZenMonkey.Hub.Domain.GeckoTerminal;

namespace ZenMonkey.Hub.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Trade")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TradeController : Controller
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Produces(typeof(Root))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var trades = await _tradeService.GetAllAsync();
                return Ok(trades);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
