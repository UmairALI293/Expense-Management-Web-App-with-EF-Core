using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TEK_Expense_Tracker.Models;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
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
            //var expenses = _context.Expenses.Include(e => e.Employee).ToList();
            //return View(expenses);
            return View();
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
        [HttpPost]
        public JsonResult GetExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            int start = Convert.ToInt32(Request.Form["start"]);
            int pageSize = Convert.ToInt32(Request.Form["length"]);
            string searchby = Request.Form["search[value]"];
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int pageNum = start / pageSize + 1;

            using (SqlConnection con = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetExpensesPaged", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", pageNum);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SortColumn", sortColumn);
                cmd.Parameters.AddWithValue("@SortOrder", sortDirection);
                cmd.Parameters.AddWithValue("@SearchText", searchby); // Add parameter for search text
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Expense expense = new Expense();

                    expense.Id = Convert.ToInt32(rdr["Id"]); 
                    expense.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]); 
                    expense.Description = rdr["Description"].ToString();
                    expense.Amount = Convert.ToDecimal(rdr["Amount"]);
                    expense.Date = Convert.ToDateTime(rdr["Date"]);
                    expense.Employee = new Employee
                    {
                        Name = rdr["Name"].ToString() 
                    };
                    expenses.Add(expense);
                }
                rdr.Close();
            }

            var jsonData = new
            {
                draw = Convert.ToInt32(Request.Form["draw"]),
                recordsTotal = expenses.Count,
                recordsFiltered = expenses.Count,
                //data = expenses.Select(e => new { e.Id, e.EmployeeId,  e.Employee, e.Description, e.Amount, e.Date })
                data = expenses
            };

            return Json(jsonData);
        }

        [HttpPost]
        public IActionResult UpdateField(int id, string field, string value)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            switch (field)
            {
                case "description":
                    expense.Description = value;
                    break;
                // Add cases for other fields if needed

                default:
                    return BadRequest();
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}