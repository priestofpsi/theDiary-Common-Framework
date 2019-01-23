using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace System.Web.Mvc
{
	public class PagerOptions
	{
		public static class DefaultDefaults
		{
			public const int MaxNrOfPages = 10;
            public const int ItemsPerPage = 20;
            public const string PagingClass = "";
            public const string DisabledClass = "disabled";
            public const string PageItemElement = "span";
			public const string DisplayTemplate = null;
			public const bool AlwaysAddFirstPageNumber = false;
			public const string DefaultPageRouteValueKey = "page";
            public const string ItemElementAdditionalClass = "";
		}

		/// <summary>
		/// The static Defaults class allows you to set Pager defaults for the entire application. 
		/// Set values at application startup.
		/// </summary>
		public static class Defaults
		{
			public static int MaxNrOfPages = DefaultDefaults.MaxNrOfPages;
			public static string DisplayTemplate = DefaultDefaults.DisplayTemplate;
			public static bool AlwaysAddFirstPageNumber = DefaultDefaults.AlwaysAddFirstPageNumber;
			public static string DefaultPageRouteValueKey = DefaultDefaults.DefaultPageRouteValueKey;
            public static int ItemsPerPage = 20;
            public static string PagingClass = "";
            public static string DisabledClass = "disabled";
            public static string PageItemElement = "span";
            public static string ItemElementAdditionalClass = "";

			public static void Reset()
			{
				MaxNrOfPages = DefaultDefaults.MaxNrOfPages;
				DisplayTemplate = DefaultDefaults.DisplayTemplate;
				AlwaysAddFirstPageNumber = DefaultDefaults.AlwaysAddFirstPageNumber;
				DefaultPageRouteValueKey = DefaultDefaults.DefaultPageRouteValueKey;
                ItemsPerPage = DefaultDefaults.ItemsPerPage;
                PagingClass = DefaultDefaults.PagingClass;
                DisabledClass = DefaultDefaults.DisabledClass;
                PageItemElement = DefaultDefaults.PageItemElement;
                ItemElementAdditionalClass = DefaultDefaults.ItemElementAdditionalClass;
			}
		}

		public RouteValueDictionary RouteValues { get; internal set; }
		public string DisplayTemplate { get; internal set; }
		public int MaxNrOfPages { get; internal set; }
		public AjaxOptions AjaxOptions { get; internal set; }
		public bool AlwaysAddFirstPageNumber { get; internal set; }
		public string Action { get; internal set; }
		public string PageRouteValueKey { get; set; }
        public int ItemsPerPage { get; set; }
        public string PagingClass { get; set; }
        public string DisabledClass { get; set; }
        public string PageItemElement { get; set; }
        public string ItemElementAdditionalClass { get; set; }
		public PagerOptions()
		{
            
			RouteValues = new RouteValueDictionary();
			DisplayTemplate = Defaults.DisplayTemplate;
            MaxNrOfPages = (System.Configuration.ConfigurationManager.AppSettings["pager:MaxNrOfPages"].IsNullEmptyOrWhiteSpace()) ? Defaults.MaxNrOfPages : int.Parse(System.Configuration.ConfigurationManager.AppSettings["pager:MaxNrOfPages"]);
			AlwaysAddFirstPageNumber = (System.Configuration.ConfigurationManager.AppSettings["page:AlwaysAddFirstPageNumber"].IsNullEmptyOrWhiteSpace()) ? Defaults.AlwaysAddFirstPageNumber : bool.Parse(System.Configuration.ConfigurationManager.AppSettings["page:AlwaysAddFirstPageNumber"]);
            ItemsPerPage = (System.Configuration.ConfigurationManager.AppSettings["pager:ItemsPerPage"].IsNullEmptyOrWhiteSpace()) ? Defaults.ItemsPerPage : int.Parse(System.Configuration.ConfigurationManager.AppSettings["pager:ItemsPerPage"]);
            PagingClass = (System.Configuration.ConfigurationManager.AppSettings["pager:PagingClass"].IsNullEmptyOrWhiteSpace()) ? Defaults.PagingClass : System.Configuration.ConfigurationManager.AppSettings["pager:PagingClass"];
            DisabledClass = (System.Configuration.ConfigurationManager.AppSettings["pager:DisabledClass"].IsNullEmptyOrWhiteSpace()) ? Defaults.DisabledClass : System.Configuration.ConfigurationManager.AppSettings["pager:DisabledClass"]; ;
            PageItemElement = (System.Configuration.ConfigurationManager.AppSettings["pager:PageItemElement"].IsNullEmptyOrWhiteSpace()) ? Defaults.PageItemElement : System.Configuration.ConfigurationManager.AppSettings["pager:PageItemElement"];
            ItemElementAdditionalClass = (System.Configuration.ConfigurationManager.AppSettings["pager:ItemElementAdditionalClass"].IsNullEmptyOrWhiteSpace()) ? Defaults.ItemElementAdditionalClass : System.Configuration.ConfigurationManager.AppSettings["pager:ItemElementAdditionalClass"];
            PageRouteValueKey = Defaults.DefaultPageRouteValueKey;
		}
	}
}
