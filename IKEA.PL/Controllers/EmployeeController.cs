using IKEA.BLL.DTOS.Department;
using IKEA.BLL.DTOS.Employees;
using IKEA.BLL.Services.DepartmentsServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region services_DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices,ILogger<EmployeeController>logger,IWebHostEnvironment environment)
        {
           this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index 
        [HttpGet] // Employee/index
        public IActionResult Index()
        {
            var Employees = employeeServices.GetAllEmployees();
            return View(Employees);
        }
        #endregion



        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto Employeedto)
        {


            if (!ModelState.IsValid)
            {
                return View(Employeedto);
            }
            var message = string.Empty;

            try
            {
                var Result = employeeServices.CreateEmployee(Employeedto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee IS not Created ";
                   
                }
            }
            catch (Exception ex)
            {
                //log exception in Kestral
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    message = ex.Message;
                   
                }
                else
                {
                    message = "An Error Effect at the creation operation ";
                    
                }
                
            }
            ModelState.AddModelError(string.Empty, message);
            return View(Employeedto);


        }
        #endregion

        #region Details 
        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee is null)
            {

                return NotFound();
            }
            return View(employee);
        }

        #endregion 
    }
}
