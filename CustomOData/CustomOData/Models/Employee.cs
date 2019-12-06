using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomOData.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public Salary Salary { get; set; }
    }
}