using Api_Project_Emp.Model;
using Microsoft.EntityFrameworkCore;

namespace Api_Project_Emp.Data
{
    public class ApplicationDb:DbContext
    {
        public ApplicationDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeeDetails> employeeDetails { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
