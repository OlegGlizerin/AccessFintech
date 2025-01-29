using AccessFintech.ApiClients.StockApiClient;
using AccessFintech.Models;
using AccessFintech.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessFintech.Services.StocksService
{
    public class StockService : IStockService
    {
        private IFileManagerService _fileManagerService;
        private IStockApiClient _stockApiClient;

        private Dictionary<string, List<Stock>> _stocks = new Dictionary<string, List<Stock>>();

        public StockService(IFileManagerService fileManagerService, IStockApiClient stockApiClient)
        {
            _fileManagerService = fileManagerService;
            _stockApiClient = stockApiClient;
        }

        public async Task AddOrUpdateStocksAsync()
        {
            _stocks = new Dictionary<string, List<Stock>>();

            Task<IEnumerable<Stock>> jsonStocksTask = _fileManagerService.ReadJsonAsync(C.JSON_PATH);
            Task<IEnumerable<Stock>> csvStocksTask = _fileManagerService.ReadCsvAsync(C.CSV_PATH);
            Task<IEnumerable<Stock>> apiStocksTask = _stockApiClient.GetStocksAsync(C.TEST_PUBLIC_API_URL);

            await Task.WhenAll(jsonStocksTask, csvStocksTask, apiStocksTask);

            FillStocks(jsonStocksTask.Result);
            FillStocks(csvStocksTask.Result);
            FillStocks(apiStocksTask.Result);
        }

        public Dictionary<string, List<Stock>> GetStocks()
        {
            return _stocks;
        }

        private void FillStocks(IEnumerable<Stock> externalStocks)
        {
            foreach(var stock in externalStocks)
            {
                if (_stocks.ContainsKey(stock.Name))
                {
                    _stocks[stock.Name].Add(stock);
                }
                else
                {
                    _stocks.Add(stock.Name, new List<Stock> { stock });
                }
            }
        }
    }
}
