using IKEA.DAl.Persistance.Repositories.Departments;
using IKEA.DAl.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.UnitOfWork
{
    public  interface IUnitOfWork
    {


        public IDepartmentsReposaiotry DepartmentsReposaiotry { get; }

        public IEmployeesReposaitory employeesReposaitory { get; }

        //Aproach of save changes 
        int complete();
      

    }
}
