using AutoMapper;
using IKEA.BLL.DTOS.Department;
using IKEA.PL.ViewModel;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {

        public  MappingProfile()
        {
            CreateMap<DepartmentVm, CreatedDepartmentDto>().ReverseMap();
            CreateMap<DepatmentDetailsDto, DepartmentVm>().ReverseMap();
            CreateMap<DepartmentVm, UpdatedDepartmentDto>().ReverseMap();
        }
    }
}
