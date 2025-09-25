using System.ComponentModel.DataAnnotations;

namespace BasicPipeline.Classes
{
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        public string City { get; set; } = "";

        public string Country { get; set; } = "";
    }
}
