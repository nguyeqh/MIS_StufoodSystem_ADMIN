using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class Purchase
    {
        public string PurchaseNumber { get; set; } = "";
        public string PurchaseStatus { get; set; } = "";
        public DateTime purchaseDate { get; set; }
        public DateTime receivedDate { get; set; }
        public int purchaseTotal { get; set; }
        public Employee employee { get; set; }
        public Supplier supplier { get; set; }
        public List<PaymentRecord> payment { get; set; }
        public List<PurchaseDetail> purchaseDetails { get; set; }
    }
}