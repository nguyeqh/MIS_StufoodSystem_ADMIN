using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class Order
    {
        public string orderNumber { get; set; }
        public string orderStatus { get; set; }
        public DateTime dateOrdered { get; set; }
        public DateTime dateReceived { get; set; }
        public int orderTotal { get; set; }
        public Employee employee { get; set; }
        public AffiliatedSchool affiliatedSchool { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public List<PaymentRecord> paymentRecords { get; set; }
    }
}