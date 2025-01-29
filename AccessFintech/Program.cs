using AccessFintech.ApiClients.StockApiClient;
using AccessFintech.Models;
using AccessFintech.Services;
using AccessFintech.Services.AccessFintechManagerService;
using AccessFintech.Services.StocksService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AccessFintech
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PrintWelcome();
            RunAddOrUpdateStocksParallel();
            IStockService stockService = new StockService(new FileManagerService(), new StockApiClient());
            IAccessFintechManagerService accessFintechManagerService = new AccessFintechManagerService(stockService);

            DateTime startTime = DateTime.Now;
            await stockService.AddOrUpdateStocksAsync();

            while (true)
            {
                try
                {
                    Console.WriteLine("Type command...");
                    double command;
                    if (double.TryParse(Console.ReadLine(), out command))
                    {
                        if (command == 1)
                        {
                            await GetLowestPriceAsync(accessFintechManagerService);
                        }
                        if (command == 2)
                        {
                            await GetAllLowestPricesAsync(accessFintechManagerService);
                        }
                        if (command == 3)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input incorect!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something wrong happend: " + e.Message);
                }
            }
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Hello to Access fintech Task");
            Console.WriteLine("Here are the possible commands:");
            Console.WriteLine("[1] - get lowest GetLowestPrice(stockName)");
            Console.WriteLine("[2] - GetAllLowestPrices();");
            Console.WriteLine("[3] - Exit;");
        }

        private static async Task GetAllLowestPricesAsync(IAccessFintechManagerService accessFintechManagerService)
        {
            IEnumerable<Stock> result = await accessFintechManagerService.GetAllLowestPricesAsync();
            foreach (var item in result)
            {
                Console.WriteLine($"Stock - {item.Name}, {item.Price}");
            }
        }

        private static async Task GetLowestPriceAsync(IAccessFintechManagerService accessFintechManagerService)
        {
            Console.WriteLine("What is the stock name?");
            string stockName = Console.ReadLine();
            var result = await accessFintechManagerService.GetLowestPriceAsync(stockName);
            Console.WriteLine($"The answer for {stockName} - {result}");
        }


        private static void RunAddOrUpdateStocksParallel()
        {

            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(AddOrUpdateStocksAsync);
            timer.Start();

            Console.WriteLine($"Task completed at: {DateTime.Now}");
        }

        public static void AddOrUpdateStocksAsync(object sender, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                Console.WriteLine($"Updating stocks: {DateTime.Now}");

                StockService stockService = new StockService(new FileManagerService(), new StockApiClient());
                await stockService.AddOrUpdateStocksAsync();
            });
        }
    }
}
