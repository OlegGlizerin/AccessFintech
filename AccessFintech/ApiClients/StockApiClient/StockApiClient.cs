using AccessFintech.Exceptions;
using AccessFintech.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccessFintech.ApiClients.StockApiClient
{
    public class StockApiClient : IStockApiClient
    {
        public async Task<IEnumerable<Stock>> GetStocksAsync(string url)
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Stock>>(content)
                      ?? throw new StockApiClientException($"StockApiClient - Failed to perform api call to {url}");
        }
    }
}
