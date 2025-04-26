using IKEA.DAl.Models;
using IKEA.DAl.Models.Employees;
using IKEA.DAl.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Repositories._Generics
{
    public class GenericRepository<T> : IGenericReposaitory<T> where T : ModelBase
    {
        //implementaion depend on Dbcontex we should create  object from context 
        // we  will find the importanvce of thre Dependency Injection  ?
        //for add if we want to Call any function ftrom  the DeparmentReposatory we should open connection 

        private readonly ApplicationDbcontext dbContext;

        public GenericRepository(ApplicationDbcontext Context)//Ask Clr for Create Object of Context becouse We work By DI
        {
            dbContext = Context;
        }
        //for each time create object of the DepartmentsReposatiory Clr make Refrence(Context) Refer to the Object created Already 


        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return dbContext.Set<T>().Where(D => D.IsDeleted == false).AsNoTracking().ToList();
            }
            return dbContext.Set<T>().Where(D => D.IsDeleted == false).ToList();
        }


        public T GetByID(int id)
        {
            //search Local First 
            var item = dbContext.Set<T>().Find(id);


            return item;
        }


        public int Add(T item)
        {
            dbContext.Set<T>().Add(item);
            return dbContext.SaveChanges();
        }

        public int Update(T item)
        {
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();

        }

        public int Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();

        }




    }
}

