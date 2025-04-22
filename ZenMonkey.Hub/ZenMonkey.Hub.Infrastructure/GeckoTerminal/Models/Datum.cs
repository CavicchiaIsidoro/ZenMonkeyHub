using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMonkey.Hub.Infrastructure.GeckoTerminal.Models
{
    public class Datum
    {
        public string id { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public Attributes? attributes { get; set; }
    }
}
