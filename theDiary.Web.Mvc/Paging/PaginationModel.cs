using System.Collections.Generic;
using System.Web.Mvc.Ajax;

namespace System.Web.Mvc
{
	public class PaginationModel
    {
        #region Constructors
        public PaginationModel()
        {
            this.PaginationLinks = new List<PaginationLink>();
            this.AjaxOptions = null;
            this.Options = null;
        }
        #endregion

        #region Public Read-Only Properties
        public int PageSize { get; internal set; }
		
        public int CurrentPage { get; internal set; }
		
        public int PageCount { get; internal set; }
		
        public int TotalItemCount { get; internal set; }
		
        public IList<PaginationLink> PaginationLinks { get; private set; }
		
        public AjaxOptions AjaxOptions { get; internal set; }
		
        public PagerOptions Options { get; internal set; }
        #endregion

    }


	
}
