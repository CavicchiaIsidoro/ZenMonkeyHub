using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Models;

namespace ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface
{
    public interface ITradeRepository
    {
        Task<Root> GetAllAsync();
    }
}
