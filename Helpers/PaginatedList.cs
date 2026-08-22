using System;
using System.Collections.Generic;
using System.Linq;

namespace CampTravelGear.Helpers
{
    public interface IPaginatedList
    {
        int PageIndex { get; }
        int TotalPages { get; }
        int TotalCount { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        int StartItemIndex { get; }
        int EndItemIndex { get; }
    }

    public class PaginatedList<T> : List<T>, IPaginatedList
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public int StartItemIndex => TotalCount == 0 ? 0 : ((PageIndex - 1) * PageSize) + 1;
        public int EndItemIndex => Math.Min(PageIndex * PageSize, TotalCount);

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
