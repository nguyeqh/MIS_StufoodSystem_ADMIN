using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class OrderDetail
    {
        public string orderDetailNumber { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public double discountPercent { get; set; }
    }
}