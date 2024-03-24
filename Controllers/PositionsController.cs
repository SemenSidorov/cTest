using cTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cTest.Controllers
{
    public class PositionsController : Controller
    {
        AppDBContent _content;

        public PositionsController(AppDBContent content)
        {
            _content = content;
        }

        public IEnumerable<Positions> GetPositions(int departmentId)
        {
            return _content.Positions.AsNoTracking().Where(el => el.departmentId == departmentId);
        }

        public Positions GetPositionById(int id)
        {
            try
            {
                return _content.Positions.AsNoTracking().First(el => el.id == id);
            }
            catch (Exception ex)
            {
                return new Positions();
            }
        }

        public List<Positions> GetPositionsList(int departmentId)
        {
            return _content.Positions.AsNoTracking().Where(el => el.departmentId == departmentId).ToList();
        }

        public List<int>? GetDepartmentsByPositionsName(string name)
        {
            List<int> result = new List<int>();
            try
            {
                var listPositions = _content.Positions.AsNoTracking().Where(el => EF.Functions.Like(el.name, $"%{name}%"));
                foreach (var item in listPositions)
                {
                    result.Add(item.departmentId);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
