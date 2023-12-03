using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class ManufacturingRecord
    {
        public String manufacturingID { get; set; } 
        public DateTime manufacturingDate { get; set; }
        public Product product { get; set; } 
        public int quantity { get; set; }
        public Employee	SupervisoryStaff { get; set; }
        public String Status { get; set; }
	
    }
}
