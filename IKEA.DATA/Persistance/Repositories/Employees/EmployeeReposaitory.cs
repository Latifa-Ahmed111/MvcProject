using IKEA.DAl.Models.Departments;
using IKEA.DAl.Models.Employees;
using IKEA.DAl.Persistance.Data;
using IKEA.DAl.Persistance.Repositories._Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Repositories.Employees
{
    public class EmployeeReposaitory : GenericRepository<Employee>, IEmployeesReposaitory
    {
        private readonly ApplicationDbcontext dbContext;

        public EmployeeReposaitory(ApplicationDbcontext Context) : base(Context)//Ask Clr for Create Object of Context becouse We work By DI
        {
            dbContext = Context;
        }

    }
}
