using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StufoodSystem_ADMIN.Models
{
    public class AffiliatedSchool
    {
        public string schoolId { get; set; } = "";
        public string schoolName { get; set; } = "";
        public string schoolPhone { get; set; } = "";
        public string schoolAddress { get; set; } = "";
        public string email { get; set; } = "";
        public int numberOfStudents { get; set; } = 0;

    }
}