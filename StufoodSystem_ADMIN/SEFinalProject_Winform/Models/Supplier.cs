using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    internal class Supplier
    {
        public string supplierId { get; set; }
        public string supplierName { get; set; }
        public string supplierAddress { get; set;}
        public string supplierEmail { get; set;}
        public string supplierPhone { get; set;}
        public List<Ingredient> ingredientProvided { get; set;}
        public double rate { get; set;}

    }
}
