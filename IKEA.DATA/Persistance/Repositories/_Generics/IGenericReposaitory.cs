using IKEA.DAl.Models;
using IKEA.DAl.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Repositories._Generics
{
    public interface IGenericReposaitory<T>where T : ModelBase
    {
        //crud operations 
        //Getall 
        //GetByid
        //Create 
        //Upadte
        //Delete 
        IQueryable<T> GetAll(bool WithNoTracking = true);

        T GetByID(int id);
        //REturns Numbers of Raw affected 

        int Add(T Entity);

        int Update(T Entity);

        int Delete(T Entity);

    }
}
