using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    internal class Order
    {
        public string orderNumber { get; set; }
        public string orderStatus { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime receivedDate { get; set; }
        public int orderTotal { get; set; }
        public Employee employee { get; set; }
        public AffiliatedSchool affiliatedSchool { get; set; }
        public List<OrderDetail> orders { get; set; }
        public List<PaymentRecord> payment { get; set; }
    }
}
