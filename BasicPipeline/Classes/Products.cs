﻿using System.ComponentModel.DataAnnotations;

namespace BasicPipeline.Classes
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; } = "";

        public string Category { get; set; } = "";

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
