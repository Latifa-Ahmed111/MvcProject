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
    }
}
