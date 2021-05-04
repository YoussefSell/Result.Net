namespace Result.Net
{
    using System.Collections.Generic;

    /// <summary>
    /// the paged result implementation class
    /// </summary>
    /// <typeparam name="TValue">the type of the data to be returned</typeparam>
    public partial class PagedResult<TValue>
    {
        /// <summary>
        /// the index of the current selected page
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// the page size, the desired number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// the total count of all items
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// the total count of all pages
        /// </summary>
        public int PagesCount => GetPageCount();
    }

    /// <summary>
    /// partial part for <see cref="PagedResult{TValue}"/>
    /// </summary>
    public partial class PagedResult<TValue> : ListResult<TValue>
    {
        /// <summary>
        /// create an instance of the <see cref="PagedResult{TValue}"/> object with given status, message, code, logTraceCode, and errors
        /// </summary>
        /// <param name="value">the value associated with the result</param>
        /// <param name="status">the status of the result</param>
        public PagedResult(IReadOnlyCollection<TValue> value, ResultStatus status)
            : base(value, status) { }

        /// <summary>
        /// this method is used to calculate the skip value
        /// </summary>
        /// <returns>skip value</returns>
        public int CalculateSkip() => (PageIndex - 1) * PageSize;

        /// <summary>
        /// get the page count
        /// </summary>
        /// <returns>the page count</returns>
        public int GetPageCount()
        {
            if (TotalRows <= 0)
                return 0;

            if (PageSize <= 0)
                return 0;

            return (int)System.Math.Ceiling((decimal)TotalRows / PageSize);
        }
    }
}
