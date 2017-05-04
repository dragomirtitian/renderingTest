using PrivateReflectionUsingDynamic;
using Renderer.TestUtilities;
using RenderingTest.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Instrumentation;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace IRR2.WebUI.UnitTests.TestUtilities
{

    public class FakeHttpContext : HttpContextBase
    {
        private readonly FakePrincipal _principal;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _cookies;
        private readonly SessionStateItemCollection _sessionItems;

        public HttpContext Context { get; set; }
        public FakeHttpContext(HttpApplication application, HttpContext context, FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems, RequestData data, HttpBrowserCapabilitiesData dataCaps)
        {
            this.ApplicationInstance = application;
            this.Context = context;
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _sessionItems = sessionItems;
            _data = data;
            _dataCaps = dataCaps;
        }
        public override HttpApplication ApplicationInstance { get; set; }
        public override HttpRequestBase Request
        {
            get
            {
                return new FakeHttpRequest(this.Context.Request, _formParams, _queryStringParams, _cookies, _data, _dataCaps);
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

        PageInstrumentationService _PageInstrumentation = new PageInstrumentationService();
        public override PageInstrumentationService PageInstrumentation
        {
            get => _PageInstrumentation;
        }

        public override HttpServerUtilityBase Server => new FakeHttpServerUtility(this);

        public int ServerExecuteDepth { get; internal set; }

        Stack<IHttpHandler> handlers = new Stack<IHttpHandler>();
        internal void SetCurrentHandler(IHttpHandler handler)
        {
            handlers.Push(handler);
        }

        internal void RestoreCurrentHandler()
        {
            handlers.Pop();
        }
    }

    class FakeHttpServerUtility: HttpServerUtilityBase
    {
        private FakeHttpContext _contextWrapper;

        public FakeHttpServerUtility(FakeHttpContext context)
        {
            this._contextWrapper = context;
        }

        class DisposableHttpContextWrapper :IDisposable
        {
            private HttpContext oldContext;

            public DisposableHttpContextWrapper(HttpContext context)
            {
                if (context != null && context != HttpContext.Current)
                {
                    this.oldContext = HttpContext.Current;
                    HttpContext.Current = context;
                }
            }

            public void Dispose()
            {
                if (this.oldContext != null)
                {
                    HttpContext.Current = this.oldContext;
                }
            }
        }
        public override void Execute(string path)
        {
            this.Execute(path, null, true);
        }

        public override void Execute(string path, TextWriter writer)
        {
            this.Execute(path, writer, true);
        }

        public override void Execute(string path, bool preserveForm)
        {
            this.Execute(path, null, preserveForm);
        }

        public override void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
        {
            if (this._contextWrapper.Context == null)
            {
                throw new HttpException("Server_not_available");
            }
            this.Execute(handler, writer, preserveForm, true);
        }

        internal void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm, bool setPreviousPage)
        {
            HttpRequest request = this._contextWrapper.Context.Request;
            string currentExecutionFilePathObject = request.AppRelativeCurrentExecutionFilePath;
            string physPath = request.MapPath(currentExecutionFilePathObject);
            this.ExecuteInternal(handler, writer, preserveForm, null, currentExecutionFilePathObject, physPath, null, null);
        }

        public override void Execute(string path, TextWriter writer, bool preserveForm)
        {
            if (this._contextWrapper.Context == null)
            {
                throw new HttpException("Server_not_available");
            }
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            string queryStringOverride = null;
            var request = this._contextWrapper.Context.Request;
            var response = this._contextWrapper.Context.Response;
            int num = path.IndexOf('?');
            if (num >= 0)
            {
                queryStringOverride = path.Substring(num + 1);
                path = path.Substring(0, num);
            }
            
            IHttpHandler handler = null;
            string text = request.MapPath(path);
            string virtualPath2 = Path.Combine(request.FilePath, path);
            
            try
            {
                if (path.EndsWith("."))
                {
                    throw new HttpException(404, string.Empty);
                }
                using (new DisposableHttpContextWrapper(this._contextWrapper.Context))
                {
                    try
                    {
                        var expr_113 = this._contextWrapper;
                        int serverExecuteDepth = expr_113.ServerExecuteDepth;
                        expr_113.ServerExecuteDepth = serverExecuteDepth + 1;
                        handler = this.MapHttpHandler(this._contextWrapper.Context.ApplicationInstance, this._contextWrapper.Context, request.RequestType, virtualPath2, text, false);
                    }
                    finally
                    {
                        var expr_189 = this._contextWrapper;
                        int serverExecuteDepth = expr_189.ServerExecuteDepth;
                        expr_189.ServerExecuteDepth = serverExecuteDepth - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpException)
                {
                    int httpCode = ((HttpException)ex).GetHttpCode();
                    if (httpCode != 500 && httpCode != 404)
                    {
                        ex = null;
                    }
                }
                throw new HttpException("Error_executing_child_request_for_path", ex);
            }
            this.ExecuteInternal(handler, writer, preserveForm,  path, virtualPath2, text, null, queryStringOverride);
        }

        private IHttpHandler MapHttpHandler(HttpApplication applicationInstance, HttpContext context, string requestType, string path, string pathTranslated, object useAppConfig)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
            //var dyn = applicationInstance.AsDynamic();
            //var handlerMapping = dyn.GetHandlerMapping(context, requestType, path, useAppConfig);
            //IHttpHandlerFactory factory = dyn.GetFactory(handlerMapping);
            //return factory.GetHandler(context, requestType, path, pathTranslated);
        }

        private void ExecuteInternal(IHttpHandler handler, TextWriter writer, bool preserveForm, string path, string filePath, string physPath, Exception error, string queryStringOverride)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            var request = (FakeHttpRequest)this._contextWrapper.Request;
            var response = (FakeHttpResponse)this._contextWrapper.Response;
            HttpApplication applicationInstance = this._contextWrapper.ApplicationInstance;
            NameValueCollection form = null;
            string path2 = null;
            string queryStringText = null;
            TextWriter writer2 = null;

            this._contextWrapper.SetCurrentHandler(handler);
            try
            {
                try
                {
                    this._contextWrapper.ServerExecuteDepth++;
                    path2 = request.SwitchCurrentExecutionFilePath(filePath);
                    if (!preserveForm)
                    {
                        form = request.SwitchForm(new NameValueCollection());
                        if (queryStringOverride == null)
                        {
                            queryStringOverride = string.Empty;
                        }
                    }
                    if (queryStringOverride != null)
                    {
                        queryStringText = request.QueryStringText;
                        request.QueryStringText = queryStringOverride;
                    }
                    if (writer != null)
                    {
                        writer2 = response.SwitchWriter(writer);
                    }
                    Page page = handler as Page;
                    if (!(handler is Page))
                    {
                        error = new HttpException(0x194, string.Empty);
                    }
                    else
                    {
                        if (handler is IHttpAsyncHandler)
                        {
                            var handler2 = (IHttpAsyncHandler)handler;
                            IAsyncResult result2;
                            bool flag4;
                            using (var countdownEvent = new CountdownEvent(1))
                            {
                                result2 = handler2.BeginProcessRequest(this._contextWrapper.Context, _=> countdownEvent.Signal(), null);
                                flag4 = !countdownEvent.IsSet;
                                countdownEvent.Wait();
                            }
                            try
                            {
                                handler2.EndProcessRequest(result2);
                            }
                            catch (Exception exception2)
                            {
                                error = exception2;
                            }
                            goto Label_041A;
                        }
                        using (new DisposableHttpContextWrapper(this._contextWrapper.Context))
                        {
                            try
                            {
                                handler.ProcessRequest(this._contextWrapper.Context);
                            }
                            catch (Exception exception3)
                            {
                                error = exception3;
                            }
                        }
                    }
                }
                finally
                {
                    this._contextWrapper.ServerExecuteDepth--;
                    this._contextWrapper.RestoreCurrentHandler();
                    if (writer2 != null)
                    {
                        response.SwitchWriter(writer2);
                    }
                    if ((queryStringOverride != null) && (queryStringText != null))
                    {
                        request.QueryStringText = queryStringText;
                    }
                    if (form != null)
                    {
                        request.SwitchForm(form);
                    }
                    request.SwitchCurrentExecutionFilePath(path2);
                }
            }
            catch
            {
                throw;
            }
            Label_041A:
            if (error == null)
            {
                return;
            }
            if ((error is HttpException) && (((HttpException)error).GetHttpCode() != 500))
            {
                error = null;
            }
            if (path != null)
            {
                object[] objArray1 = new object[] { path };
                throw new HttpException("Error_executing_child_request_for_path", error);
            }
            object[] args = new object[] { handler.GetType().ToString() };
            throw new HttpException("Error_executing_child_request_for_handler", error);
        }

        public override string UrlDecode(string s)
        {
            Encoding e = (this._contextWrapper != null) ? this._contextWrapper.Request.ContentEncoding : Encoding.UTF8;
            return HttpUtility.UrlDecode(s, e);
        }

        public override void UrlDecode(string s, TextWriter output)
        {
            if (s != null)
            {
                output.Write(this.UrlDecode(s));
            }
        }

        public override string UrlEncode(string s)
        {
            Encoding e = (this._contextWrapper != null) ? this._contextWrapper.Response.ContentEncoding : Encoding.UTF8;
            return HttpUtility.UrlEncode(s, e);
        }

        public override void UrlEncode(string s, TextWriter output)
        {
            if (s != null)
            {
                output.Write(this.UrlEncode(s));
            }
        }

        public override string UrlPathEncode(string s) =>
            HttpUtility.UrlPathEncode(s);
        

    }
}
