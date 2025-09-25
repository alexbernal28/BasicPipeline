using BasicPipeline.Classes;

namespace BasicPipeline.Services
{
    public class OrderDetailsService
    {
        private readonly CsvService _csvService;
        public OrderDetailsService(CsvService csvService)
        {
            _csvService = csvService;
        }
        public List<Order_Details> ReadAndTransform(string filePath, List<Orders> orders, List<Products> products)
        {
            var orderDetails = _csvService.ReadCsv<Order_Details>(filePath);

            // Eliminate duplicates
            orderDetails = orderDetails
                .GroupBy(d => new { d.OrderID, d.ProductID })
                .Select(g => g.First())
                .ToList();

            // Eliminate records with non-existing orders or products

            orderDetails = orderDetails
                .Where(d => orders.Any(o => o.OrderID == d.OrderID) && products.Any(p => p.ProductID == d.ProductID))
                .ToList();

            return orderDetails;
        }
    }
}
