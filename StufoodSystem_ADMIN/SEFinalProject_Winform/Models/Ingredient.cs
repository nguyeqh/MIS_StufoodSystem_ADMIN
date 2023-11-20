using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    internal class Ingredient
    {
        public string ingredientID { get; set; } = "";
        public string ingredientName { get; set; } = "";
        public string ingredientCategory { get; set; } = "";
        public string ingredientDescription { get; } = "";
        public string ingredientPreservation { get; set; } = "";
        public int quantityAvailable { get; set; } = 0;
        public double price { get; set; }= 0.0;

    }
}
