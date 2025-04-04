using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Services.DepartmentsServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        
        //mainPage 

        //Service to get DataBase belongs to departments 

       private readonly IDepartmentsServices departmentsServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment enviroment;

        public DepartmentController(IDepartmentsServices _departmentsServices,ILogger<DepartmentController>_Logger,IWebHostEnvironment enviroment)
        {

            departmentsServices = _departmentsServices;
            logger = _Logger;
            this.enviroment = enviroment;
        }
        #region Index 
        [HttpGet]
        public IActionResult Index()
        {
            var Departmenmts=departmentsServices.GetAllDepartments();
                return View(Departmenmts);
        }

        #endregion

        #region Details 
        [HttpGet]
        public IActionResult Details(int ? id )
        {

            if(id is null)
            {
                return BadRequest();
            }
            var department = departmentsServices.GetDepartmentById(id.Value);
            if(department is null)
            {

                return NotFound();
            }
            return View(department);
        }

        #endregion 

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentdto)
        {

           
            if (!ModelState.IsValid)
            {
                return View(departmentdto);
            }
            var message=string.Empty;
            
            try 
            {
                var Result = departmentsServices.CreateDepartment(departmentdto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department IS not Created ";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentdto);
                }
            }
            catch(Exception ex)
            {
                //log exception in Kestral
                logger.LogError(ex, ex.Message);
                if(enviroment.IsDevelopment())
                {
                    message = ex.Message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentdto);
                }
                else
                {
                    message = "An Error Effect at the creation operation ";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentdto);
                }
            }
            
            
        }
        #endregion

        #region Update
        [HttpGet]

        public IActionResult Edit(int?id)
        {
            
            if(id is null)
            {
                return BadRequest();
            }
            var Department = departmentsServices.GetDepartmentById(id.Value);
            if (Department is null)
            {
                return NotFound();
            }
            var MappedDepartment = new UpdatedDepartmentDto
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,

            };
            return View(MappedDepartment);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentdto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentdto);

            }
            var message = String.Empty;
            try
            {
                var Result = departmentsServices.UpdateDepartment(departmentdto);
                if (Result > 0)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                    message = "Department is Not Updated ";
            }
            catch (Exception ex)
            {
                //log exception throw the kestral 
                logger.LogError(ex,ex.Message);
                message = enviroment.IsDevelopment() ? ex.Message : "An Error Occured During Updated the Department ";
            }
            ModelState.AddModelError(string.Empty,message);
            return View(departmentdto);
        }


        #endregion 
    }
}
