using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public class PagedList<T>
        : List<T>, IPagedList<T>
    {
        #region Constructors
        public PagedList(IEnumerable<T> source, int index, int pageSize, int? totalCount = null)
            : this(source.AsQueryable(), index, pageSize, totalCount)
        {
        }

        public PagedList(IQueryable<T> source, int index, int pageSize, int? totalCount = null)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Value can not be below 0.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "Value can not be less than 1.");

            if (source == null)
                source = new List<T>().AsQueryable();

            var realTotalCount = source.Count();

            this.pageSize = pageSize;
            this.pageIndex = index;
            this.totalItemCount = totalCount.HasValue ? totalCount.Value : realTotalCount;
            this.pageCount = this.totalItemCount > 0 ? (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize) : 0;

            this.hasPreviousPage = (this.pageIndex > 0);
            this.hasNextPage = (this.pageIndex < (this.pageCount - 1));
            this.isFirstPage = (this.pageIndex <= 0);
            this.isLastPage = (this.pageIndex >= (this.pageCount - 1));

            this.itemStart = this.pageIndex * this.pageSize + 1;
            this.itemEnd = Math.Min(this.pageIndex * this.pageSize + this.pageSize, this.totalItemCount);

            if (this.totalItemCount <= 0)
                return;

            var realTotalPages = (int)Math.Ceiling(realTotalCount / (double)this.pageSize);

            if (realTotalCount < this.totalItemCount && realTotalPages <= this.pageIndex)
                base.AddRange(source.Skip((realTotalPages - 1) * this.pageSize).Take(this.pageSize));
            else
                base.AddRange(source.Skip(this.pageIndex * this.pageSize).Take(this.pageSize));
        }
        #endregion

        #region Private Declarations
        private int pageCount;
        private int totalItemCount;
        private int pageIndex;
        private int pageSize;
        private bool hasPreviousPage;
        private bool hasNextPage;
        private bool isFirstPage;
        private bool isLastPage;
        private int itemStart;
        private int itemEnd;
        #endregion

        #region IPagedList Members
        int IPagedList.PageCount
        {
            get
            {
                return this.pageCount;
            }
        }

        int IPagedList.TotalItemCount
        {
            get
            {
                return this.totalItemCount;
            }
        }

        int IPagedList.PageIndex
        {
            get
            {
                return this.pageIndex;
            }
        }

        int IPagedList.PageNumber
        {
            get
            {
                return (this.pageIndex + 1);
            }
        }
        int IPagedList.PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        bool IPagedList.HasPreviousPage
        {
            get
            {
                return this.hasPreviousPage;
            }
        }
        bool IPagedList.HasNextPage
        {
            get
            {
                return this.hasNextPage;
            }
        }
        bool IPagedList.IsFirstPage
        {
            get
            {
                return this.isFirstPage;
            }
        }
        bool IPagedList.IsLastPage
        {
            get
            {
                return this.isLastPage;
            }
        }
        int IPagedList.ItemStart
        {
            get
            {
                return this.itemStart;
            }
        }
        int IPagedList.ItemEnd
        {
            get
            {
                return this.itemEnd;
            }
        }

        #endregion
    }
}