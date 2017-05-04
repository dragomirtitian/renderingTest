

using RenderingTest.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace IRR2.WebUI.UnitTests.TestUtilities
{
    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext(HttpContext context, ControllerBase controller)
            : this(context, controller, null, null, null, null, null, null)
        {
        }

        public FakeControllerContext(HttpContext context, ControllerBase controller, HttpCookieCollection cookies)
            : this(context, controller, null, null, null, null, cookies, null)
        {
        }

        public FakeControllerContext(HttpContext context, ControllerBase controller, SessionStateItemCollection sessionItems)
            : this(context, controller, null, null, null, null, null, sessionItems)
        {
        }


        public FakeControllerContext(HttpContext context, ControllerBase controller, NameValueCollection formParams)
            : this(context, controller, null, null, formParams, null, null, null)
        {
        }


        public FakeControllerContext(HttpContext context, ControllerBase controller, NameValueCollection formParams, NameValueCollection queryStringParams)
            : this(context, controller, null, null, formParams, queryStringParams, null, null)
        {
        }



        public FakeControllerContext(HttpContext context, ControllerBase controller, string userName)
            : this(context, controller, userName, null, null, null, null, null)
        {
        }


        public FakeControllerContext(HttpContext context, ControllerBase controller, string userName, string[] roles)
            : this(context, controller, userName, roles, null, null, null, null)
        {
        }


        public FakeControllerContext
            (
            HttpContext context,
                ControllerBase controller,
                string userName,
                string[] roles,
                NameValueCollection formParams,
                NameValueCollection queryStringParams,
                HttpCookieCollection cookies,
                SessionStateItemCollection sessionItems, RequestData data = null
            )
            : base(new FakeHttpContext(context, new FakePrincipal(new FakeIdentity(userName), roles), formParams, queryStringParams, cookies, sessionItems, data, null), new RouteData(), controller)
        { }
    }
}
