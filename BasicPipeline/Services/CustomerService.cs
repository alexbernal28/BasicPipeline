using BasicPipeline.Classes;

namespace BasicPipeline.Services
{
    public class CustomerService
    {
        private readonly CsvService _csvService;
        public CustomerService(CsvService csvService)
        {
            _csvService = csvService;
        }
        public List<Customers> ReadAndTransform(string filePath)
        {
            var customers = _csvService.ReadCsv<Customers>(filePath);

            // Eliminate duplicates based on CustomerID
            customers = customers
                .GroupBy(c => c.CustomerID)
                .Select(g => g.First())
                .ToList();

            // Normalize texts

            foreach (var customer in customers)
            {
                customer.FirstName = customer.FirstName.Trim();
                customer.LastName = customer.LastName.Trim();
                customer.Email = customer.Email.Trim().ToLower();
                customer.City = customer.City.Trim();
                customer.Country = customer.Country.Trim().ToLower();
                customer.Phone = NormalizePhone(customer.Phone);
            }
            // validate emails

            customers = customers
                .Where(c => !string.IsNullOrEmpty(c.Email) && c.Email.Contains("@") && c.Email.Contains("."))
                .ToList();

            return customers;
        }

        public string NormalizePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return "";

            phone = phone.Trim();

            // Remove extensions
            int index = phone.IndexOf('x');
            if (index > 0)
                phone = phone.Substring(0, index);

            index = phone.IndexOf("ext", StringComparison.OrdinalIgnoreCase);
            if (index > 0)
                phone = phone.Substring(0, index);

            // Remove all non-digit characters
            phone = new string(phone.Where(char.IsDigit).ToArray());

            // Limit to 20 characters
            if (phone.Length > 20)
                phone = phone.Substring(0, 20);

            return phone;
        }

    }
}
