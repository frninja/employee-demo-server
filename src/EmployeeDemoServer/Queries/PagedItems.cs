using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCode.EmployeeDemoServer.Queries
{
    public class PagedItems<T>
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public int TotalPagesCount { get; }

        public int TotalItemsCount { get; }
        public IEnumerable<T> Items { get; }

        public PagedItems(int pageSize, int pageNumber, int totalPagesCount, int totalItemsCount, IEnumerable<T> items)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPagesCount = totalPagesCount;

            TotalItemsCount = totalItemsCount;
            Items = items;
        }

        public PagedItems<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new PagedItems<TResult>(PageSize, PageNumber, TotalPagesCount, TotalItemsCount,
                                           Items.Select(selector));
        }
    }
}
