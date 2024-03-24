using cTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cTest.Controllers
{
    public class JobersController : Controller
    {
        AppDBContent _content;

        private DepartmentsController departmentsController;

        private PositionsController positionsController;

        public JobersController(AppDBContent content)
        {
            _content = content;
            departmentsController = new DepartmentsController(_content);
            positionsController = new PositionsController(_content);
        }

        public IEnumerable<Jobers> GetList(int lastId = 1, string search = "")
        {
            var departmentsId = departmentsController.GetDepartmentByName(search);
            return _content.Jobers
                .Include(x => x.position)
                .Include(x => x.department)
                .AsNoTracking()
                .Where(el => el.id > lastId && (EF.Functions.Like(el.fio, $"%{search}%") || EF.Functions.Like(el.phone, $"%{search}%") || departmentsId.Contains(el.departmentId)))
                .Take(3);
        }

        public void AddJober(Jobers jober, IFormFile? photo)
        {
            if (photo != null)
            {
                jober.photo = FileController.SaveFile(photo);
            }
            _content.Jobers.Add(jober);
            _content.SaveChanges();
        }

        public void UpdateJober(Jobers jober, IFormFile? photo)
        {
            if (photo != null)
            {
                jober.photo = FileController.SaveFile(photo);
            }
            _content.Jobers.Update(jober);
            _content.SaveChanges();
        }

        public void DeleteJober(int id)
        {
            var jober = new Jobers { id = id };
            _content.Jobers.Attach(jober);
            _content.Jobers.Remove(jober);
            _content.SaveChanges();
        }

        public Jobers GetJoberById(int id)
        {
            Jobers jober = _content.Jobers.AsNoTracking().First(x => x.id == id);
            jober.department = departmentsController.GetDepartmentById(jober.departmentId);
            jober.position = positionsController.GetPositionById(jober.positionId);
            return jober;
        }
    }
}
