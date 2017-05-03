using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace IRR2.WebUI.UnitTests.TestUtilities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class HttpContextHelper
    {
        public static void InitializeContextAndUrlHelper(Controller controller, bool isAjax = false)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var request = MockRepository.GenerateStub<HttpRequestBase>();
            request.Stub(x => x.ApplicationPath).Return("/");

            if (isAjax)
            {
                request.Expect(x => x.Headers["X-Requested-With"]).Return("XMLHttpRequest");
            }

            request.Stub(x => x.Url).Return(new Uri("http://localhost/", UriKind.Absolute));
            request.Stub(x => x.ServerVariables).Return(new System.Collections.Specialized.NameValueCollection());

            var response = MockRepository.GenerateStub<HttpResponseBase>();
            response.Stub(x => x.ApplyAppPathModifier(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(x => x.ReturnValue = x.Arguments[0]);
            var cache = MockRepository.GenerateStub<HttpCachePolicyBase>();
            response.Stub(x => x.Cache).Return(cache);

            var httpcontext = MockRepository.GenerateStub<HttpContextBase>();
            httpcontext.Stub(x => x.Request).Return(request);
            httpcontext.Stub(x => x.Response).Return(response);

            controller.ControllerContext = new ControllerContext(httpcontext, new RouteData(), controller);

            controller.Url = new UrlHelper(new RequestContext(httpcontext, new RouteData()), routes);

            var view = MockRepository.GenerateStub<IView>();
            var engine = MockRepository.GenerateStub<IViewEngine>();
            var viewEngineResult = new ViewEngineResult(view, engine);
            engine
                .Stub(x => x.FindPartialView(null, null, false))
                .IgnoreArguments()
                .Return(viewEngineResult);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);
        }
    }
}
