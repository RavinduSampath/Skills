using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skills.Models
{
    public class Student
    {
        public int regNo;
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string mobilePhone { get; set; } // Changed to string
        public string homePhone { get; set; } // Changed to string
        public string parentName { get; set; }
        public string nic { get; set; }
        public string contactNo { get; set; } // Changed to string
    }
}
