using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class PurchaseDetail
    {
        public string purchaseDetailNumber { get; set; }
        public Ingredient ingredient { get; set; }
        public String purchaseID { get; set; }
        public int quantity { get; set; }

    }
}