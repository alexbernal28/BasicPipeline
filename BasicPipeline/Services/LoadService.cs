using BasicPipeline.Classes;
using BasicPipeline.Context;
using Microsoft.EntityFrameworkCore;

namespace BasicPipeline.Services
{
    public class LoadService
    {
        private readonly AppDbContext _dbContext;
        public LoadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void LoadData(
            List<Products> products,
            List<Customers> customers,
            List<Orders> orders,
            List<Order_Details> orderDetails)
        {
            // Clear existing data
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM Order_Details");
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM Orders");
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM Customers");
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM Products");

            // Insert processed data into the database
            _dbContext.Products.AddRange(products);
            _dbContext.Customers.AddRange(customers);
            _dbContext.Orders.AddRange(orders);
            _dbContext.Order_Details.AddRange(orderDetails);

            _dbContext.SaveChanges();
        }
    }
}
