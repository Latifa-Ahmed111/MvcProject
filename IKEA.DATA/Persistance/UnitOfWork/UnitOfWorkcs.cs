using IKEA.DAl.Persistance.Data;
using IKEA.DAl.Persistance.Repositories.Departments;
using IKEA.DAl.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.UnitOfWork
{
    public class UnitOfWorkcs : IUnitOfWork
    {
        public IDepartmentsReposaiotry DepartmentsReposaiotry { get; }
        public IEmployeesReposaitory employeesReposaitory { get; }
        public ApplicationDbcontext DbContext { get; }

        public UnitOfWorkcs( ApplicationDbcontext dbContext) 
        {
            this. DbContext = dbContext;
            DepartmentsReposaiotry = new DepartmentsReposatiory(this.DbContext);
            employeesReposaitory = new EmployeeReposaitory(this.DbContext);
        }

        public int complete()
        {
           return DbContext.SaveChanges();
        }

        
    }
}
