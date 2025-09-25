using BasicPipeline.Data;
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

            var csvService = new CsvService();

            var productService = new ProductService(csvService);
            var orderService = new OrderService(csvService);
            var orderDetailsService = new OrderDetailsService(csvService);
            var customerService = new CustomerService(csvService);

            var products = productService.ReadAndTransform("Data/products.csv");
            var customers = customerService.ReadAndTransform("Data/customers.csv");
            var orders = orderService.ReadAndTransform("Data/orders.csv", customers);
            var orderDetails = orderDetailsService.ReadAndTransform("Data/order_details.csv", orders, products);

            using var db = new AppDbContext();

            var loadService = new LoadService(db);
            loadService.LoadData(products, customers, orders, orderDetails);

            Console.WriteLine($"Products: {products.Count}");
            Console.WriteLine($"Customers: {customers.Count}");
            Console.WriteLine($"Orders: {orders.Count}");
            Console.WriteLine($"Order Details: {orderDetails.Count}");

            Console.WriteLine("Load completed on SQL Server");
        }
    }
}
