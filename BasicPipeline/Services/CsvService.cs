using CsvHelper;
using System.Globalization;

namespace BasicPipeline.Services
{
    public class CsvService
    {
        public List<T> ReadCsv<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
