using System.ComponentModel.DataAnnotations;

namespace BasicPipeline.Classes
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "";
    }
}
