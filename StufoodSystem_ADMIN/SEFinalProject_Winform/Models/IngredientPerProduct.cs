using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class IngredientPerProduct
    {
        public Ingredient ingredient { get; set; }
        public double quantity { get; set; }
        public string note { get; set; }
    }
}