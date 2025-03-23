using IKEA.BLL.Services.DepartmentsServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {

        //mainPage 

        //Service to get DataBase belongs to departments 

        private IDepartmentsServices departmentsServices;
       public DepartmentController(IDepartmentsServices _departmentsServices)
        {

            departmentsServices = _departmentsServices;

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
