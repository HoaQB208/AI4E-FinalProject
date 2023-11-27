using Binance.Net.Clients;
using Binance.Net.Enums;
using FinalProject.Core._Class;

namespace FinalProject.Core.Data
{
    public class Market
    {
        public static async Task<List<TKline>> Download(string symbol, Interval interval, DateTime? startTime, int limit = 1500)
        {
            List<TKline> klines = new();
            CryptoExchange.Net.Objects.WebCallResult<IEnumerable<Binance.Net.Interfaces.IBinanceKline>> get = await new BinanceRestClient().UsdFuturesApi.ExchangeData.GetKlinesAsync(symbol, (KlineInterval)(int)interval, startTime: startTime, limit: limit);
            if (get.Success)
            {
                klines = get.Data.Select(x => new TKline { D = x.OpenTime, O = x.OpenPrice, C = x.ClosePrice, H = x.HighPrice, L = x.LowPrice, V = x.Volume, B = x.TakerBuyBaseVolume }).ToList();
            }
            return klines;
        }
    }
}