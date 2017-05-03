using RenderingTest.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace IRR2.WebUI.UnitTests.TestUtilities
{

    public class FakeHttpContext : HttpContextBase
    {
        private readonly FakePrincipal _principal;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _cookies;
        private readonly SessionStateItemCollection _sessionItems;

        public FakeHttpContext(FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems, RequestData data, HttpBrowserCapabilitiesData dataCaps)
        {
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _sessionItems = sessionItems;
            _data = data;
            _dataCaps = dataCaps;
        }

        public override HttpRequestBase Request
        {
            get
            {
                return new FakeHttpRequest(_formParams, _queryStringParams, _cookies, _data, _dataCaps);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return _Response ?? (_Response = new FakeHttpResponse(_cookies));
            }
        }

        public override IPrincipal User
        {
            get
            {
                return _principal;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override HttpSessionStateBase Session
        {
            get
            {
                return new FakeHttpSessionState(_sessionItems);
            }
        }
        System.Collections.IDictionary _Items = new Dictionary<object, object>();
        private FakeHttpResponse _Response;
        private RequestData _data;
        private HttpBrowserCapabilitiesData _dataCaps;
        public override System.Collections.IDictionary Items
        {
            get
            {
                return _Items;
            }
        }


    }
}
