using Microsoft.Data.SqlClient;

namespace TEK_Expense_Tracker.Models
{
    public class DBConnection
    {
        private static string connectionString = "Data Source=UmairA-Lap\\SQLEXPRESS;Initial Catalog=ExpenseManagement;Integrated Security=true;";
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
