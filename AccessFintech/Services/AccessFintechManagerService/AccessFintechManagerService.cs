using AccessFintech.Models;
using AccessFintech.Services.StocksService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessFintech.Services.AccessFintechManagerService
{
    public class AccessFintechManagerService : IAccessFintechManagerService
    {
        private IStockService _stockService;

        public AccessFintechManagerService(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IEnumerable<Stock>> GetAllLowestPricesAsync()
        {
            Dictionary<string, List<Stock>> stocks = _stockService.GetStocks();
            List<string> allStocksNames = stocks.Keys.ToList();
            List<Stock> result = new List<Stock>();
            foreach(var stockName in allStocksNames)
            {
                result.Add(new Stock() { Name = stockName, Price = await GetLowestPriceAsync(stockName) } );
            }
            return result;
        }

        public async Task<double> GetLowestPriceAsync(string stockName)
        {
            Task<double> task = Task.Run(() => {
                Dictionary<string, List<Stock>> stocks = _stockService.GetStocks();
                return stocks[stockName].Min(stock => stock.Price);
            });
            await task;
            return task.Result;
        }
    }
}
