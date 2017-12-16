using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Expression<Func<Employee, object>> orderedKeySelector;
                if (!keySelectors.TryGetValue(OrderBy.ToLower(), out orderedKeySelector))
                {
                    orderedKeySelector = keySelectors.First().Value;
                }

                var ordered = Descending ? context.Employees.OrderByDescending(orderedKeySelector)
                                         : context.Employees.OrderBy(orderedKeySelector);

                return await new Paginator(PageSize).Paginate(ordered, PageNumber)
                        .ConfigureAwait(false);
            }
        }


        private static readonly Dictionary<string, Expression<Func<Employee, object>>> keySelectors =
            new Dictionary<string, Expression<Func<Employee, object>>>
            {
                { "name", e => e.Name },
                { "email", e => e.Email },
                { "birthDay", e => e.BirthDay },
                { "salary", e => e.Salary },
            };
    }
}
