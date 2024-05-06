using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEK_Expense_Tracker.Models;

namespace TEK_Expense_Tracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExDbContext _context;

        public ExpenseController(ExDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var expenses = _context.Expenses.Include(e => e.Employee).ToList();
            return View(expenses);
        }
        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Include(e => e.Expenses).FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
    }
}

