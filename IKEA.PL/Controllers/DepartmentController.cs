using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Services.DepartmentsServices;
using IKEA.PL.ViewModel;
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
            var Departmenmts = departmentsServices.GetAllDepartments();
            ViewData["Message"] = "Hello from ViewData ";
            ViewBag.Message = "Hello from ViewBag";
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentVm departmentVm)
        {

           
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            var message=string.Empty;
            
            try 
            {
                var departmentdto = new CreatedDepartmentDto()
                {
                    Name = departmentVm.Name,
                    Code = departmentVm.Code,
                    CreationDate = departmentVm.CreationDate,
                    Description = departmentVm.Description,

                };
                var Result = departmentsServices.CreateDepartment(departmentdto);
                if (Result > 0)
                {
                    TempData["Message"] = $"{departmentdto.Name} Department is Created ";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department IS not Created ";
                    
                }
            }
            catch(Exception ex)
            {
                //log exception in Kestral
                logger.LogError(ex, ex.Message);
                if(enviroment.IsDevelopment())
                {
                    message = ex.Message;
                    
                }
                else
                {
                    message = "An Error Effect at the creation operation ";
                   
                }
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVm);

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
            var MappedDepartment = new DepartmentVm()
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentVm departmentVm)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVm);

            }
            var message = String.Empty;
            try
            {
                var departmentdto = new UpdatedDepartmentDto()
                {
                    Id=departmentVm.Id,
                    Name = departmentVm.Name,
                    Code = departmentVm.Code,
                    CreationDate = departmentVm.CreationDate,
                    Description = departmentVm.Description,
                };


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
            return View(departmentVm);
        }


        #endregion

        #region Delete 
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }
            var Department = departmentsServices.GetDepartmentById(id.Value);
            if (Department is null) { return NotFound(); };
            return View(Department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Deptid)
        {
            var message = String.Empty;
            try
            {
                var IsDeleted = departmentsServices.DeleteDepartment(Deptid);
                if(IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Department is Not Deleted ";

            }
            catch   (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                message = enviroment.IsDevelopment()?ex.Message :" the is error during the  Deleting this Department ";

            }
            ModelState.AddModelError(string.Empty,message);
            return RedirectToAction(nameof(Delete), new { id = Deptid });
        }
        #endregion

    }
}
