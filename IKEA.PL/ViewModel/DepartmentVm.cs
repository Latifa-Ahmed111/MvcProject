using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
    public class DepartmentVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name is required ")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "The Code is required ")]
        public string Code { get; set; } = null!;

        public string? Description { get; set; }
        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }


    }
}
