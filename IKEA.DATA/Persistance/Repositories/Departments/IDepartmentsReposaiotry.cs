using IKEA.DAl.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Repositories.Departments
{
    public interface IDepartmentsReposaiotry
    {
        //crud operations 
        //Getall 
        //GetByid
        //Create 
        //Upadte
        //Delete 
        IEnumerable<Department> GetAll(bool WithNoTracking =true);

        Department GetByID(int id);
        //REturns Numbers of Raw affected 

        int Add(Department Department);

        int Update(Department Department);  

        int Delete(Department Department);  

    }
}
