using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TEK_Expense_Tracker.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Range(1, 200, ErrorMessage = "Department character must be between 1 to 200")]
        public string Department { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
    }
  
}

