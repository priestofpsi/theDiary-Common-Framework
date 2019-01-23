using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Optimization
{
    public class RegisteredBundle
    {
        public RegisteredBundle(string registrationName, string bundleName)
            : base()
        {
            this.RegistrationName = registrationName;
        }

        public string RegistrationName { get; private set; }

        public string BundleName { get; private set; }


        private void InitializeBundle()
        {

        }
    }
}
