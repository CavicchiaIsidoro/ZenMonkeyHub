using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenMonkey.Application.Telegram.Models;
using ZenMonkey.Hub.Domain.Telegram;

namespace ZenMonkey.Application.Telegram.Mappers
{
    public static class MessageMapper
    {
        public static Message ToDomain(this CreateMessageTelegramRequest request)
        {
            return new Message
            {
                ImgUrl = request.ImgUrl,
                Text = request.Message
            };
        }
    }
}
