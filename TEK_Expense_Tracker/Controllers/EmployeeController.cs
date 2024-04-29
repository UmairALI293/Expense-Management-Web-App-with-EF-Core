// EmployeesController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TEK_Expense_Tracker.Models;

namespace EmployeeExpenseManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ExDbContext _context;

        public EmployeeController(ExDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditEmp(int id)
        {
            var emp = _context.Employees.Find(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmp(Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult DeleteEmp(int id)
        //{
        //    var emp = _context.Employees.Find(id);
        //    return View(emp);
        //}

        [HttpPost, ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int id)
        {
            var emp = _context.Employees.Find(id);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
