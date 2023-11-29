using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class Supplier
    {
        public string supplierId { get; set; }
        public string supplierName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Ingredient> ingredientProvided { get; set; }
        public double rate { get; set; }

    }
}