using Microsoft.AspNetCore.Mvc;

namespace cTest.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
