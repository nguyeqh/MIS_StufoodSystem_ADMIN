using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    internal class AffiliatedSchool
    {
        public string schoolId { get; set; } = "";
        public string schoolName { get; set; } = "";
        public string schoolPhone { get; set; } = "";
        public string schoolAddress { get; set; } = "";
        public int numStudents { get; set; } = 0;

    }
}