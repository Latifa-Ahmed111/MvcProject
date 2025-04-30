using IKEA.BLL.DTOS.Department;
using IKEA.DAl.Models.Departments;
using IKEA.DAl.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentsServices
{
    public class DepartmentsServices:IDepartmentsServices
    {
        //controller ==> Services ==>Reposatiory==>Context==>options  created By Clr By default (one Object per request )

        // we work By DI so we will pass Refrence of Reposatiory
        // We make refrence from IDepartmentsReposaiotry  to avoid that if we need call OrcaleDepartmentsReposaiotry مش عاىزين نلعب هنا
        private IDepartmentsReposaiotry Reposatiory;


        public DepartmentsServices(IDepartmentsReposaiotry _reposatiory)
        {

            Reposatiory = _reposatiory;

        }


        //Implementation   
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = Reposatiory.GetAll().Where(D=>!D.IsDeleted).Select(department => new DepartmentDto() {

                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,

            }).ToList();
            //List<DepartmentDto> departmentDtos= new List<DepartmentDto>();

            //foreach (var department in Department) 
            //{
            //    DepartmentDto depatmentDetailDto = new DepartmentDto()
            //    {
            //        Id=department.Id,
            //        Name=department.Name,
            //       Code=department.Code,
            //       CreationDate=department.CreationDate,
            //    };

            //    departmentDtos.Add(depatmentDetailDto);

            //}
            ///AutoMapper 
            return Departments;
        }

        public DepatmentDetailsDto? GetDepartmentById(int Id)
        {
           var Department=Reposatiory.GetByID(Id);
            if (Department is not null)
            {
                return new DepatmentDetailsDto()
                {
                    Id = Department.Id, 
                    Name = Department.Name,
                    Code = Department.Code, 
                    Description=Department.Description,
                    CreationDate = Department.CreationDate,
                    IsDeleted = Department.IsDeleted,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedon= Department.LastModifiedon,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,

                };
            }
            return null;
        }
        

        public int CreateDepartment(CreatedDepartmentDto departmentdto)
        {

            var CreatedDepartment = new Department()
            {
                Code = departmentdto.Code,
                Name = departmentdto.Name,
                Description = departmentdto.Description,
                CreationDate = departmentdto.CreationDate,
                CreatedBy = 1,
                CreatedOn=DateTime.Now, 
                LastModifiedBy= 1,
                LastModifiedon=DateTime.Now


            };
            return Reposatiory.Add(CreatedDepartment);





        }


        public int UpdateDepartment(UpdatedDepartmentDto departmentdto)
        {
            var UpdatedDepartment = new Department()
            {
                Id = departmentdto.Id,
                Code = departmentdto.Code,
                Name = departmentdto.Name,
                Description = departmentdto.Description,
                CreationDate = departmentdto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedon= DateTime.Now,
               
            };
            return Reposatiory.Update(UpdatedDepartment);
        }


         

        public bool DeleteDepartment(int departmentid)
        {
            

            var department=Reposatiory.GetByID(departmentid);
          
            if(department is not null)
            {
                return Reposatiory.Delete(department)>0;
            }
           
            else
                return false;

        }


       

        

     
    }
}
