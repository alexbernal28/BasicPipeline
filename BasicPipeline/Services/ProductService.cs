using BasicPipeline.Classes;
using CsvHelper;
using System.Globalization;

namespace BasicPipeline.Services
{
    public class ProductService
    {
        private readonly CsvService _csvService;

        public ProductService(CsvService csvService)
        {
            _csvService = csvService;
        }

        public List<Products> ReadAndTransform(string filePath)
        {
            var products = _csvService.ReadCsv<Products>(filePath);

            // Eliminate duplicates based on ProductID

            products = products
                .GroupBy(p => p.ProductID)
                .Select(g => g.First())
                .ToList();

            // Normalize texts

            foreach (var product in products)
            {
                product.ProductName = product.ProductName.Trim();
                product.Category = product.Category.Trim().ToLower();
            }

            // Eliminate invalid records

            products = products
                .Where(p => !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                .ToList();

            return products;
        }
    }
}
