using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class Product
    {
        public string ProductId { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string ProductDescription { get; set; } = "";
        public string ProductCategory { get; set; } = "";
        public double ProductRating { get; set; } = 0.0;
        public double ProductPrice { get; set; }
        public int quantityAvailable { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }
}