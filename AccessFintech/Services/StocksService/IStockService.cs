using AccessFintech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessFintech.Services.StocksService
{
    public interface IStockService
    {
        public Dictionary<string, List<Stock>> GetStocks();
        public Task AddOrUpdateStocksAsync();
    }
}
