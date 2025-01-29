using AccessFintech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessFintech.ApiClients.StockApiClient
{
    public interface IStockApiClient
    {
        public Task<IEnumerable<Stock>> GetStocksAsync(string url);
    }
}
