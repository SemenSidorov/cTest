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

        public IEnumerable<Department> GetDepartmentsSearch(string search = "")
        {
            var departmentsId = positionsController.GetDepartmentsByPositionsName(search);
            return _content.Department
                .Include(x => x.positions)
                .AsNoTracking()
                .Where(el => EF.Functions.Like(el.name, $"%{search}%") || departmentsId.Contains(el.id));
        }

        public IEnumerable<Department> GetDepartments(int departmentId = 0)
        {
            return _content.Department
                .Include(x => x.positions)
                .Include(x => x.departments).ThenInclude(x => x.positions)
                .AsNoTracking().Where(el => el.departmentId == departmentId);
        }

        public IEnumerable<Department> GetDepartmentsList(int? departmentId)
        {
            return _content.Department.AsNoTracking().Where(el => el.id != departmentId);
        }

        public Department GetDepartmentById(int id)
        {
            return _content.Department.AsNoTracking().First(el => el.id == id);
        }

        public IEnumerable<int>? GetDepartmentByName(string name)
        {
            return _content.Department.AsNoTracking().Where(el => EF.Functions.Like(el.name, $"%{name}%")).Select(x => x.id);
        }

        public void AddDepartment(Department department)
        {
            _content.Department.Add(department);
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
