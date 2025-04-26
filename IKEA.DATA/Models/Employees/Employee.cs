using IKEA.DAl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Models.Employees
{
    public class Employee:ModelBase
    {
        public String Name { get; set; }

        public int? Age { get; set; }

        public String? Address { get; set; }   

        public decimal Salary { get; set; }

        public bool IsActive { get;set; }

        public String? Email  { get; set; }

        public String? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
