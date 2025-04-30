using IKEA.BLL.DTOS.Employees;
using IKEA.DAl.Models.Departments;
using IKEA.DAl.Models.Employees;
using IKEA.DAl.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeService:IEmployeeServices
    {
        private readonly IEmployeesReposaitory reposaitory ;

        public EmployeeService(IEmployeesReposaitory employeesReposaitory)
        {
            reposaitory = employeesReposaitory;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = reposaitory.GetAll();
            var filteredEmployees = Employees.Where(E => E.IsDeleted == false);
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

            });
            return AfterFilteration.ToList();
        }

        public EmployeeDetailsDto? GetEmployeeById(int Id)
        {
           var Employee=reposaitory.GetByID(Id);
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
                EmployeeType    = employeeDto.EmployeeType,
                CreatedBy =1,
                LastModifiedBy=1,   
                LastModifiedon=DateTime.Now,
                CreatedOn=DateTime.Now, 

            };
            return reposaitory.Add(Employee);
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
                LastModifiedBy = 1,
                LastModifiedon = DateTime.Now,

            };
            return reposaitory.Update(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = reposaitory.GetByID(id);

            if (employee is not null)
            {
                return reposaitory.Delete(employee) > 0;
            }

            else
                return false;
        }

        

        

        
    }
}
