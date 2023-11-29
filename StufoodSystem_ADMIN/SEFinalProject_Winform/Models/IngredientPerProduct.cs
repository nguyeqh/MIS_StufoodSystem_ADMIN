using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class IngredientPerProduct
    {
        public string IngredientPerProductID { get; set; }
        public Ingredient ingredient { get; set; }
        public string productId { get; set; }  
        public double quantity { get; set; }
        public string note { get; set; }
    }
}