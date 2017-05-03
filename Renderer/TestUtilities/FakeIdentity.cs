using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace IRR2.WebUI.UnitTests.TestUtilities
{
    public class FakeIdentity : IIdentity
    {
        private readonly string _name;

        public FakeIdentity(string userName)
        {
            _name = userName;

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public string AuthenticationType
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(_name); }
        }

        public string Name
        {
            get { return _name; }
        }

    }

}
