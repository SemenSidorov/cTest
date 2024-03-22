using cTest.Controllers;
using cTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cTest.Controllers
{
    public class APIController : Controller
    {
        AppDBContent _content;

        private JobersController jobersController;

        private DepartmentsController departmentsController;

        private PositionsController positionsController;

        public APIController(AppDBContent content)
        {
            _content = content;
            jobersController = new JobersController(_content);
            departmentsController = new DepartmentsController(_content);
            positionsController = new PositionsController(_content);
        }

        [HttpPost]
        public IActionResult GetListJobers(int lastId = 1, string search = "")
        {
            return View(jobersController.GetList(lastId, search));
        }

        [HttpPost]
        public IActionResult FormUpdateJober(int id)
        {
            return View(jobersController.GetJoberById(id));
        }

        [HttpPost]
        public IActionResult FormUpdateDepartment(int id)
        {
            return View(departmentsController.GetDepartmentById(id));
        }

        [HttpPost]
        public void AddJober(Jobers jober, IFormFile? photo)
        {
            jobersController.AddJober(jober, photo);
        }

        [HttpPost]
        public void UpdateJober(Jobers jober, IFormFile? photo)
        {
            jobersController?.UpdateJober(jober, photo);
        }

        [HttpPost]
        public void DeleteJober(int id)
        {
            jobersController?.DeleteJober(id);
        }

        [HttpPost]
        public string Authorization(string login, string password)
        {
            Users user = new();
            try
            {
                user = _content.Users.First(el => el.login == login);
            }
            catch (Exception e)
            {
                return "{\"error\": \"Incorrect login\"}";
            }
            bool isPasswordCorrect = SecretHasherController.Verify(password, user.password);
            if (isPasswordCorrect)
            {
                return "{\"success\": " + user.id + "}";
            }
            return "{\"error\": \"Incorrect password\"}";
        }

        [HttpPost]
        public string Pass(string pass)
        {
            return SecretHasherController.Hash(pass);
        }

        [HttpPost]
        public IActionResult GetDepartments()
        {
            return View(departmentsController.GetDepartments());
        }

        [HttpPost]
        public IActionResult GetDepartmentsList(int? departmentId)
        {
            return View(departmentsController.GetDepartmentsList(departmentId));
        }

        [HttpPost]
        public IActionResult GetPositionsList(int departmentId)
        {
            return View(positionsController.GetPositionsList(departmentId));
        }

        [HttpPost]
        public IActionResult GetDepartmentsSearch(string search)
        {
            return View(departmentsController.GetDepartmentsSearch(search));
        }

        [HttpPost]
        public IActionResult FormAddJober()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormAddDepartment()
        {
            return View();
        }

        [HttpPost]
        public void AddDepartment(Department department)
        {
            departmentsController.AddDepartment(department);
        }

        [HttpPost]
        public void UpdateDepartment(Department department)
        {
            departmentsController.UpdateDepartment(department);
        }

        [HttpPost]
        public void DeleteDepartment(int id)
        {
            departmentsController.DeleteDepartment(id);
        }
    }
}
