using Microsoft.EntityFrameworkCore;
using System;

namespace TEK_Expense_Tracker.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
        
    }
  
}

