using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ZenMonkey.Application.Telegram.Models;
using ZenMonkey.Application.Telegram.Services.Interfaces;

namespace ZenMonkey.Hub.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Trade")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TelegramController : Controller
    {
        private readonly ITelegramService _telegramService;

        public TelegramController(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        [HttpPost]
        [Produces(typeof(CreateMessageTelegramResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateMessageTelegramResponse))]
        public async Task<IActionResult> Post([FromBody] CreateMessageTelegramRequest request)
        {
            var result = await _telegramService.PostMessageAsync(request);
            return Ok(result);
        }
    }
}
