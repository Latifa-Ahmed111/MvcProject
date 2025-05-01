using IKEA.BLL.DTOS.Employees;
using IKEA.DAl.Models.Departments;
using IKEA.DAl.Models.Employees;
using IKEA.DAl.Persistance.Repositories.Employees;
using IKEA.DAl.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeService:IEmployeeServices
    {


        private readonly  IUnitOfWork UnitOfWork;

        public EmployeeService(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            var Employees = UnitOfWork.employeesReposaitory.GetAll();
            var filteredEmployees = Employees.Where(E => E.IsDeleted == false&&(string.IsNullOrEmpty(search)||E.Name.ToLower().Contains(search.ToLower()))).Include(E=>E.Department);
            var AfterFilteration= filteredEmployees. Select(E=> new EmployeeDto()
            {
                Id=E.Id,
                Name =E.Name,
                Age =E.Age,
                Salary =E.Salary,
                Email=E.Email,
                IsActive =E.IsActive,
                Gender=E.Gender,
                EmployeeType= E.EmployeeType,
                Department=E.Department.Name?? "N/A"

            });
            return AfterFilteration.ToList();
        }

        public EmployeeDetailsDto? GetEmployeeById(int Id)
        {
           var Employee=UnitOfWork.employeesReposaitory.GetByID(Id);
            if (Employee is not null )
            {
                return new EmployeeDetailsDto() 
                {
                    Id=Employee.Id,
                    Name =Employee.Name,
                    Age = Employee.Age,
                    Address= Employee.Address,  
                    IsActive=Employee.IsActive,
                    Salary=Employee.Salary,
                    Email=Employee.Email,
                    PhoneNumber=Employee.PhoneNumber,
                    HiringDate=Employee.HiringDate,
                    Gender=Employee.Gender,
                    EmployeeType= Employee.EmployeeType,
                    LastModifiedBy=Employee.LastModifiedBy,
                    CreatedBy=Employee.CreatedBy,   
                    LastModifiedon=Employee.LastModifiedon,
                    CreatedOn=Employee.CreatedOn,
                    Department = Employee.Department.Name ?? "N/A"
                };
            }
            return null;
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            { 
            
                Name = employeeDto.Name,
                Age = employeeDto.Age,  
                Address = employeeDto.Address,
                IsActive=employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email=employeeDto.Email,
                PhoneNumber=employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType  = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy =1,
                LastModifiedBy=1,   
                LastModifiedon=DateTime.Now,
                CreatedOn=DateTime.Now, 
               

            };
             UnitOfWork.employeesReposaitory.Add(Employee);
            return UnitOfWork.complete();
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            {
                Id= employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId= employeeDto.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedon = DateTime.Now,

            };
            UnitOfWork.employeesReposaitory.Update(Employee);
            return UnitOfWork.complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = UnitOfWork.employeesReposaitory.GetByID(id);

            if (employee is not null)
            {
                 UnitOfWork.employeesReposaitory.Delete(employee) ;
            }
            var result = UnitOfWork.complete();
            if(result>0)
            {
                return true;
            }

            else
                return false;
        }

        

        

        
    }
}
