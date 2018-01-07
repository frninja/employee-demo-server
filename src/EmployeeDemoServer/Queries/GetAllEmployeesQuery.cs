using System.Linq;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Queries
{
    public class GetAllEmployeesQuery : Query<PagedItems<Employee>>
    {
        public int PageSize { get; }
        public int PageNumber { get; }

        public string OrderBy { get; }
        public bool Descending { get; }

        public GetAllEmployeesQuery(int pageSize, int pageNumber, string orderBy, bool descending)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            OrderBy = orderBy;
            Descending = descending;
        }

        public override async Task<PagedItems<Employee>> Execute()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                IOrderedQueryable<Employee> ordered = SortBy(context.Employees, OrderBy, Descending);

                return await new Paginator(PageSize).Paginate(ordered, PageNumber).ConfigureAwait(false);
            }
        }


        private IOrderedQueryable<Employee> SortBy(IQueryable<Employee> employees, string orderBy, bool descending) {
            switch (orderBy.ToLower()) {
                case "name":
                    return descending ? employees.OrderByDescending(e => e.Name) : employees.OrderBy(e => e.Name);
                case "email":
                    return descending ? employees.OrderByDescending(e => e.Email) : employees.OrderBy(e => e.Email);
                case "birthday":
                    return descending ? employees.OrderByDescending(e => e.BirthDay) : employees.OrderBy(e => e.BirthDay);
                case "salary":
                    return descending ? employees.OrderByDescending(e => e.Salary) : employees.OrderBy(e => e.Salary);
                default:
                    return descending ? employees.OrderByDescending(e => e.Id) : employees.OrderBy(e => e.Id);
            }
        }
    }
}
