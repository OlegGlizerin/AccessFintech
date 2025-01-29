using AccessFintech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessFintech.Services.AccessFintechManagerService
{
    public interface IAccessFintechManagerService
    {
        public Task<double> GetLowestPriceAsync(string stockName);

        public Task<IEnumerable<Stock>> GetAllLowestPricesAsync();
    }
}
