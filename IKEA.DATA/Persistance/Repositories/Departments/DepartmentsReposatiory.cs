using IKEA.DAl.Models.Departments;
using IKEA.DAl.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Repositories.Departments
{
    public class DepartmentsReposatiory : IDepartmentsReposaiotry
    {

        //implementaion depend on Dbcontex we should create  object from context 
        // we  will find the importanvce of thre Dependency Injection  ?
        //for add if we want to Call any function ftrom  the DeparmentReposatory we should open connection 

        private readonly ApplicationDbcontext dbContext;

        public DepartmentsReposatiory(ApplicationDbcontext Context )//Ask Clr for Create Object of Context becouse We work By DI
        {
            dbContext = Context;
        }
        //for each time create object of the DepartmentsReposatiory Clr make Refrence(Context) Refer to the Object created Already 



        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if(WithNoTracking)
                return dbContext.Departments.AsNoTracking().ToList();
            return dbContext.Departments.ToList();
        }


        public Department GetByID(int id)
        {
            //search Local First 
            var Department=dbContext.Departments.Find(id);

            //var Department = dbContext.Department.Local.FirstOrDefault(D=>D.Id == id);

            //if(Department is null)
            //{
            //    Department = dbContext.Department.FirstOrDefault(D => D.Id == id);

            //}
            return Department;
        }

       
        public int Add(Department Department)
        {
            dbContext.Departments.Add(Department);
            return dbContext.SaveChanges();
        }

        public int Update(Department Department)
        {
            dbContext.Departments.Update(Department);
            return dbContext.SaveChanges() ;

        }

        public int Delete(Department Department)
        {
            dbContext.Departments.Remove(Department);
            return dbContext.SaveChanges() ;

        }


        
      
    }
}
