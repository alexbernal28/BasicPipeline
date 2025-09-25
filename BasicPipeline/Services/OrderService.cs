using BasicPipeline.Classes;

namespace BasicPipeline.Services
{
    public class OrderService
    {
        private readonly CsvService _csvService;
        public OrderService(CsvService csvService)
        {
            _csvService = csvService;
        }
        public List<Orders> ReadAndTransform(string filePath, List<Customers> customers)
        {
            var orders = _csvService.ReadCsv<Orders>(filePath);

            // Eliminate duplicates based on OrderID
            orders = orders
                .GroupBy(o => o.OrderID)
                .Select(g => g.First())
                .ToList();

            // Normalize dates
            foreach (var order in orders)
            {
                order.OrderDate = order.OrderDate.Date;
                order.Status = order.Status.Trim().ToLower();
            }

            // Eliminate orders with non-existing customers

            orders = orders
                .Where(o => customers.Any(c => c.CustomerID == o.CustomerID))
                .ToList();

            return orders;
        }
    }
}
