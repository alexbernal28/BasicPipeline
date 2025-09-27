using BasicPipeline.Context;
using BasicPipeline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPipeline
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string basePath = @"C:\Users\alexa\OneDrive\Documentos\Electiva1\DataCSV";

            var csvService = new CsvService();

            var productService = new ProductService(csvService);
            var orderService = new OrderService(csvService);
            var orderDetailsService = new OrderDetailsService(csvService);
            var customerService = new CustomerService(csvService);

            var products = productService.ReadAndTransform($@"{basePath}\products.csv");
            var customers = customerService.ReadAndTransform($@"{basePath}\customers.csv");
            var orders = orderService.ReadAndTransform($@"{basePath}\orders.csv", customers);
            var orderDetails = orderDetailsService.ReadAndTransform($@"{basePath}\order_details.csv", orders, products);

            using var db = new AppDbContext();

            var loadService = new LoadService(db);
            loadService.LoadData(products, customers, orders, orderDetails);

            Console.WriteLine($"Products: {products.Count}");
            Console.WriteLine($"Customers: {customers.Count}");
            Console.WriteLine($"Orders: {orders.Count}");
            Console.WriteLine($"Order Details: {orderDetails.Count}");

            if (products.Count > 0 && customers.Count > 0 && orders.Count > 0 && orderDetails.Count > 0)
            {
                Console.WriteLine("Data loaded successfully.");
            }
            else
            {
                Console.WriteLine("Data loading failed.");
            }
        }
    }
}
