

using AccessFintech.Exceptions;
using AccessFintech.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccessFintech.Services
{
    public class FileManagerService : IFileManagerService
    {
        public async Task<IEnumerable<Stock>> ReadJsonAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                var content = await File.ReadAllTextAsync(filePath);
                var stocksFromJson = JsonConvert.DeserializeObject<IEnumerable<Stock>>(content);

                return stocksFromJson;
            }
            throw new JsonNotFoundException($"The json filePath: {filePath} not correct.");
        }

        public async Task<IEnumerable<Stock>> ReadCsvAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                lines = lines.Skip(1).ToArray();
                var stocksFromCsv = lines.Select(line =>
                {
                    var parts = line.Split(',');
                    return new Stock { Name = parts[0], Price = double.Parse(parts[2]) };
                });
                return stocksFromCsv;
            }
            throw new JsonNotFoundException($"The csv filePath: {filePath} not correct.");
        }
    }
}
