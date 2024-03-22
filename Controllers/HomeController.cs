using cTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cTest.Controllers
{
    public class HomeController : Controller
    {
        AppDBContent _content;

        private JobersController jobersController;

        private DepartmentsController departmentsController;

        public HomeController(AppDBContent content)
        {
            _content = content;
            jobersController = new JobersController(_content);
            departmentsController = new DepartmentsController(_content);
        }

        public IActionResult Index()
        {
            return View(jobersController.GetList());
        }

        public IActionResult Department()
        {
            return View(departmentsController.GetDepartments());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
