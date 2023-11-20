using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    internal class Employee
    {
        public string employeeID { get; set; } = "";
        public string employeeName { get; set; } = "";

        public string phone { get; set; } = "";
        public string address{ get; set; } = "";
        public string job { get; set; } = "";
        public string position { get; set; } = "";
        public string email { get; set; } = "";
        public double salary { get; set; } = 0.0;

    }
}
