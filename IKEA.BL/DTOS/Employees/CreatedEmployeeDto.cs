using IKEA.DAl.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOS.Employees
{
    public class CreatedEmployeeDto
    {
        [MaxLength(50,ErrorMessage ="Maxmam length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Minmum length of Name is 5 Chars")]
        public String Name { get; set; }

        [Range(22,30)]
        public int? Age { get; set; }
        //[RegularExpression(@"^[0-9]){1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address Must Be like 123-Street-Street-City-Country")]
        public String? Address { get; set; }

        public decimal Salary { get; set; }
        [Display( Name="IsActive")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public String? Email { get; set; }
        [Display(Name="Phone Number")]
        [Phone]
        public String? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

    }
}
