using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources
{
    public class ResourceController<TCommonResource>
        : ResourceController
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ResourceController"/>.
        /// </summary>
        public ResourceController()
            : base(typeof(TCommonResource))
        {
        }
        #endregion
    }
}
