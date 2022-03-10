//Contributor : MVCContrib

using System;

namespace WebApp.Web.Front.Models.Paging
{
    /// <summary>
    /// Base class for pageable models
    /// </summary>
    public abstract class BasePageableModel : IPageableModel
    {
        #region Methods
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total count</param>
        public void LoadPagedList(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

            TotalItems = totalCount;
            TotalPages = TotalItems / pageSize;

            if (TotalItems % pageSize > 0)
                TotalPages++;

            FirstItem = (PageIndex * PageSize) + 1;
            HasNextPage = PageIndex + 1 < TotalPages;
            HasPreviousPage = PageIndex > 0;
            LastItem = Math.Min(TotalItems, ((PageIndex * PageSize) + PageSize));
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current page index (starts from 0)
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (PageNumber > 0)
                    return PageNumber - 1;
                
                return 0;
            }
        }

        /// <summary>
        /// The current page number (starts from 1)
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items in each page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of items.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The index of the first item in the page.
        /// </summary>
        public int FirstItem { get; set; }

        /// <summary>
        /// The index of the last item in the page.
        /// </summary>
        public int LastItem { get; set; }

        /// <summary>
        /// Whether there are pages before the current page.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Whether there are pages after the current page.
        /// </summary>
        public bool HasNextPage { get; set; }
        
        #endregion
    }
}