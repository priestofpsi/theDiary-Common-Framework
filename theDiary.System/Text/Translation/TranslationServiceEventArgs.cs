using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public class TranslationServiceEventArgs
        : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationServiceEventArgs"/> class.
        /// </summary>
        public TranslationServiceEventArgs()
            : base()
        {
        }
        #endregion

        #region Private Declarations
        internal ITranslationProvider service;
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets the <see cref="ITranslationProvider"/> instance.
        /// </summary>
        internal ITranslationProvider Service
        {
            get
            {
                return this.service;
            }
        }

        /// <summary>
        /// Gets a value indicating if the <see cref="ITranslationProvider"/> instance has been set.
        /// </summary>
        internal bool ServiceSet
        {
            get
            {
                return this.service != null;
            }
        }
        #endregion

        #region Public Methods & Functions
        public void SetService(ITranslationProvider service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            this.service = service;
        }
        #endregion
    }
}
