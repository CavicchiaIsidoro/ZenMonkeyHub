using ZenMonkey.Hub.Domain.GeckoTerminal;
using Infra = ZenMonkey.Hub.Infrastructure.GeckoTerminal.Models;

namespace ZenMonkey.Application.Trades.Mappers
{
    public static class TradeMapper
    {
        public static Attributes ToDomain(this Infra.Attributes model)
        {
            return new Attributes
            {
                price_from_in_currency_token = model.price_from_in_currency_token,
                price_from_in_usd = model.price_from_in_usd,
                block_number = model.block_number,
                block_timestamp = model.block_timestamp,
                from_token_address = model.from_token_address,
                from_token_amount = model.from_token_amount,
                kind = model.kind,
                price_to_in_currency_token = model.price_to_in_currency_token,
                price_to_in_usd = model.price_to_in_usd,
                to_token_address = model.to_token_address,
                to_token_amount = model.to_token_amount,
                tx_from_address = model.tx_from_address,
                tx_hash = model.tx_hash,
                volume_in_usd = model.volume_in_usd
            };
        }

        public static Datum ToDomain(this Infra.Datum model)
        {
            return new Datum
            {
                id = model.id,
                type = model.type,
                attributes = model.attributes?.ToDomain()
            };
        }

        public static IEnumerable<Datum> ToDomain(this IEnumerable<Infra.Datum> listModel)
            => listModel.Select(d => d.ToDomain());

        public static Root ToDomain(this Infra.Root model)
        {
            return new Root
            {
                data = model.data?.ToDomain()
            };
        }
    }
}
