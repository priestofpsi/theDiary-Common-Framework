using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public class PaginationLink
    {
        public bool IsArrow
        {
            get
            {
                return this.DisplayText == "«";
            }
        }

        public bool Active { get; set; }

        public bool IsCurrent { get; set; }

        public int? PageIndex { get; set; }

        public string DisplayText { get; set; }

        public string Url { get; set; }

        public bool IsSpacer { get; set; }
    }
}
