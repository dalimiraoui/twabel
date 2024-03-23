using System.Collections.Generic;
using System.Linq;

namespace Twabel.CrossCutting.Helpers
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }

        public PagedList()
        {

        }

        public PagedList(IQueryable<T> source, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = source.Count();
            this.Items = items;
        }

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize, int appSettingsSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            var pageSizeTouse = pageSize == 0 ? appSettingsSize : pageSize;
            var items = source.Skip((pageNumber - 1) * pageSizeTouse).Take(pageSizeTouse).ToList();

            this.CurrentPage = pageNumber;
            this.PageSize = pageSizeTouse;
            this.TotalCount = source.Count();
            this.Items = items;
        }
    }
}
/*
This is a C# class named PagedList<T> that represents a paged list of items of type T. It contains the following properties:

CurrentPage: an integer representing the current page of the paged list.
PageSize: an integer representing the maximum number of items to be displayed on a page.
TotalCount: an integer representing the total number of items in the paged list.
Items: a list of items of type T that are included in the current page of the paged list.
The class has three constructors:

The default constructor, which takes no arguments.
A constructor that takes an IQueryable<T> source, a page number, and a page size. This constructor initializes the CurrentPage, PageSize, and TotalCount properties based on the input arguments, and retrieves a list of items from the source using the Skip and Take methods to retrieve items for the current page.
A constructor that takes an IQueryable<T> source, a page number, a page size, and an appSettingsSize integer value. This constructor is similar to the previous constructor, but it uses the appSettingsSize value instead of the pageSize value if the pageSize value is 0.

*/