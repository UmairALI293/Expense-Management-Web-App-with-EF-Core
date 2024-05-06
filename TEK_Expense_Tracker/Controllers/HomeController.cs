using Microsoft.AspNetCore.Mvc;
using TEK_Expense_Tracker.Models;
using System.Linq;

namespace TEK_Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExDbContext _context;

        public HomeController(ExDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult List()
        {
            var employees = _context.Employees.ToList();
            return Json(employees);
        }

      

        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            var employee = _context.Employees.Find(id);
            return Json(employee);
        }

      
        [HttpPost]
        public JsonResult Add([FromBody] Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return Json(emp);
        }

        [HttpPost]
        public JsonResult Update([FromBody] Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return Json(emp);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Json(employee);
        }
    }
}
