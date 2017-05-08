using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages.Scope;

namespace IRR2.Common.Tests.MvcRendering
{
    public class RenderingScopeStorageProvider : IScopeStorageProvider
    {
        public IDictionary<object, object> CurrentScope { get; set; } = new Dictionary<object, object>();

        public IDictionary<object, object> GlobalScope { get; } = new Dictionary<object, object>();
    }
}
