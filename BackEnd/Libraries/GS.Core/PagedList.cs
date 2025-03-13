using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Core
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false, int? sourceCount= null)
        {
            int total = 0;
            if (sourceCount != null)
            {
                total = sourceCount??0;
            }
            else
            {
                if(source != null)
                { 
                total = source.Count();
                }    
            }
            pageSize = (pageSize == 0 ? int.MaxValue : pageSize);
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            if (getOnlyTotalCount)
                return;
            // nếu là trang cuối thì chỉ lấy số lượng còn lại thôi
            int remainAmount = total - pageSize * (pageIndex);
            if (TotalPages == pageIndex + 1 && remainAmount > 0)
            {
                if (source != null)
                {
                    this.AddRange(source.Skip(pageIndex * pageSize).Take(remainAmount).ToList());
                }
            }
            else
            {
                if (source != null)
                {
                    this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
                }
            }
           
        }

   
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            pageSize = (pageSize == 0 ? int.MaxValue : pageSize);

            this.TotalCount = total;
            this.TotalPages = total / pageSize;
            /*TotalCount = source.Count;
            TotalPages = TotalCount / pageSize;*/

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            //this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
            int remainAmount = TotalCount - pageSize * (pageIndex);
            if (TotalPages == pageIndex + 1 && remainAmount > 0)
            {
                this.AddRange(source.Skip(pageIndex * pageSize).Take(remainAmount).ToList());
            }
            else
            {
                this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total count</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source);
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage => PageIndex > 0;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
