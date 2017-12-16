using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCode.EmployeeDemoServer.Queries
{
    public class Paginator
    {
        public static readonly int MinPageSize = 1;
        public static readonly int MaxPageSize = 10;
        public static readonly int MinPageNumber = 1;

        public int PageSize { get; }

        public Paginator(int pageSize)
        {
            PageSize = FitPageSizeToValidRange(pageSize);
        }

        public async Task<PagedItems<T>> Paginate<T>(IQueryable<T> items, int pageNumber)
        {
            int totalItemsCount = items.Count();
            int totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / PageSize);

            pageNumber = FitPageNumberToValidRange(pageNumber, totalPagesCount);

            IEnumerable<T> paginatedItems =
                await items.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToListAsync().ConfigureAwait(false);

            return new PagedItems<T>(PageSize, pageNumber, totalPagesCount, totalItemsCount, paginatedItems);
        }


        private int FitPageSizeToValidRange(int pageSize)
        {
            if (pageSize < MinPageSize)
                return MinPageSize;

            if (pageSize > MaxPageSize)
                return MaxPageSize;

            return pageSize;
        }

        private int FitPageNumberToValidRange(int pageNumber, int totalPagesCount)
        {
            if (pageNumber < MinPageNumber)
                return MinPageNumber;

            if (pageNumber > totalPagesCount)
                return totalPagesCount;

            return pageNumber;
        }
    }
}
