using IKEA.BLL.DTOS.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentsServices
{
    public  interface IDepartmentsServices
    {
        ///services 
        /////GetDEpartments Implementation Change from Reposatopry
        /////Dto data transfer Object 
        ///
        IEnumerable<DepartmentDto> GetAllDepartments();

        DepatmentDetailsDto? GetDepartmentById( int Id );

        int CreateDepartment(CreatedDepartmentDto departmentdto);
        int UpdateDepartment(UpdatedDepartmentDto departmentdto);
        bool DeleteDepartment(int departmentid);

    }
}
