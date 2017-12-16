using System.Data.Entity;

using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Db
{
    public class EmployeeContext : DbContext
    {
        static EmployeeContext() {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EmployeeContext>());
        }


        public EmployeeContext() : base("EmployeeConnectionString") {}


        public DbSet<Employee> Employees { get; set; }
    }
}
