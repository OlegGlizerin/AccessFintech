using AccessFintech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessFintech.Services
{
    public interface IFileManagerService
    {
        public Task<IEnumerable<Stock>> ReadJsonAsync(string filePath);
        public Task<IEnumerable<Stock>> ReadCsvAsync(string filePath);
    }
}
