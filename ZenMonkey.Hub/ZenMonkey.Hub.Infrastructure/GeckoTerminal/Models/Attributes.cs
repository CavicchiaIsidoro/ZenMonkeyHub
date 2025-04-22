using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMonkey.Hub.Infrastructure.GeckoTerminal.Models
{
    public class Attributes
    {
        public int block_number { get; set; }
        public string? tx_hash { get; set; }
        public string? tx_from_address { get; set; }
        public string? from_token_amount { get; set; }
        public string? to_token_amount { get; set; }
        public string? price_from_in_currency_token { get; set; }
        public string? price_to_in_currency_token { get; set; }
        public string? price_from_in_usd { get; set; }
        public string? price_to_in_usd { get; set; }
        public DateTime block_timestamp { get; set; }
        public string? kind { get; set; }
        public string? volume_in_usd { get; set; }
        public string? from_token_address { get; set; }
        public string? to_token_address { get; set; }
    }
}
