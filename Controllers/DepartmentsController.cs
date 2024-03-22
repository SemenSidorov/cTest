using cTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cTest.Controllers
{
    public class DepartmentsController : Controller
    {
        AppDBContent _content;

        private PositionsController positionsController;

        public DepartmentsController(AppDBContent content)
        {
            _content = content;
            positionsController = new PositionsController(_content);
        }

        public List<Department> GetDepartmentsSearch(string search = "")
        {
            List<int>? departmentsId = positionsController.GetDepartmentsByPositionsName(search);
            var departmentsList = _content.Department.AsNoTracking().Where(el => EF.Functions.Like(el.name, $"%{search}%") || departmentsId.Contains(el.id)).ToList();
            foreach (var item in departmentsList)
            {
                item.positions = (List<Positions>?)positionsController.GetPositions(item.id);
            }
            return departmentsList;
        }

        public List<Department> GetDepartments(int departmentId = 0)
        {
            var departmentsList = _content.Department.AsNoTracking().Where(el => el.departmentId == departmentId).ToList();
            foreach (var item in departmentsList)
            {
                item.departments = GetDepartments(item.id);
                item.positions = (List<Positions>?)positionsController.GetPositions(item.id);
            }
            return departmentsList;
        }

        public List<Department> GetDepartmentsList(int? departmentId)
        {
            return _content.Department.AsNoTracking().Where(el => el.id != departmentId).ToList();
        }

        public Department GetDepartmentById(int id)
        {
            try
            {
                return _content.Department.AsNoTracking().First(el => el.id == id);
            }
            catch (Exception ex)
            {
                return new Department();
            }
        }

        public List<int>? GetDepartmentByName(string name)
        {
            List<int> result = new List<int>();
            try
            {
                var listDepart = _content.Department.AsNoTracking().Where(el => EF.Functions.Like(el.name, $"%{name}%"));
                foreach (var item in listDepart)
                {
                    result.Add(item.id);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void AddDepartment(Department department)
        {
            _content.Department.AddRange(department);
            _content.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            _content.Department.Update(department);
            _content.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var department = new Department { id = id };
            _content.Department.Attach(department);
            _content.Department.Remove(department);
            _content.SaveChanges();
        }
    }
}
