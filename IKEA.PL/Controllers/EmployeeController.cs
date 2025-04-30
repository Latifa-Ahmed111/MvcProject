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
        [ValidateAntiForgeryToken]
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

        #region Update
        [HttpGet]

        public IActionResult Edit(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var Employee = employeeServices.GetEmployeeById(id.Value);
            if (Employee is null)
            {
                return NotFound();
            }
            var MappedEmployee = new UpdatedEmployeeDto
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                HiringDate = Employee.HiringDate,
                Salary=Employee.Salary,
                Gender=Employee.Gender,
                EmployeeType=Employee.EmployeeType,
                IsActive=Employee.IsActive,
                PhoneNumber = Employee.PhoneNumber,
                Email = Employee.Email

            };
            return View(MappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdatedEmployeeDto employeedto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeedto);

            }
            var message = String.Empty;
            try
            {
                var Result = employeeServices.UpdateEmployee(employeedto);
                if (Result > 0)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                    message = "Employee is Not Updated ";
            }
            catch (Exception ex)
            {
                //log exception throw the kestral 
                logger.LogError(ex, ex.Message);
                message = environment.IsDevelopment() ? ex.Message : "An Error Occured During Updated the Employee ";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeedto);
        }


        #endregion

        #region Delete 
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee is null) { return NotFound(); };
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int EmpId)
        {
            var message = String.Empty;
            try
            {
                var IsDeleted =employeeServices.DeleteEmployee(EmpId);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Employee is Not Deleted ";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                message = environment.IsDevelopment() ? ex.Message : " the is error during the  Deleting this Employee ";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { id = EmpId });
        }
        #endregion
    }
}
