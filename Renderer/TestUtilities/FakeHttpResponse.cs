using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;

namespace Renderer.TestUtilities
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private HttpCookieCollection cookies;
        public FakeHttpResponse(HttpCookieCollection cookies)
        {
            this.cookies = cookies;
        }
        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookies;
            }
        }
        MemoryStream outputStream = new MemoryStream();
        public override Stream OutputStream => outputStream;

        TextWriter textWriter;
        public override TextWriter Output
        {
            get => textWriter ?? (textWriter = new StreamWriter(outputStream));
            set => throw new NotSupportedException();
        }

        public override bool BufferOutput { get; set; }

        public void CopyOutputTo(Stream target)
        {
            this.Output.Flush();
            this.outputStream.Seek(0, SeekOrigin.Begin);
            this.outputStream.CopyTo(target);
        }
        public void CopyOutputTo(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Create))
            {
                this.CopyOutputTo(s);
            }
        }
        public override void AddCacheDependency(params CacheDependency[] dependencies)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddCacheItemDependencies(ArrayList cacheKeys)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddCacheItemDependencies(string[] cacheKeys)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddCacheItemDependency(string cacheKey)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddFileDependencies(ArrayList filenames)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddFileDependencies(string[] filenames)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddFileDependency(string filename)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AddHeader(string name, string value)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AppendCookie(HttpCookie cookie)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AppendHeader(string name, string value)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void AppendToLog(string param)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string ApplyAppPathModifier(string overridePath)
        {
            return overridePath;
        }

        public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void BinaryWrite(byte[] buffer)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Clear()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void ClearContent()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void ClearHeaders()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Close()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void DisableKernelCache()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void DisableUserCache()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void End()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void EndFlush(IAsyncResult asyncResult)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Flush()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Pics(string value)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Redirect(string url)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Redirect(string url, bool endResponse)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectPermanent(string url)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        internal TextWriter SwitchWriter(TextWriter writer)
        {
            var old = this.textWriter;
            this.textWriter = writer;
            return old;
        }

        public override void RedirectPermanent(string url, bool endResponse)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoute(object routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoute(string routeName)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoute(RouteValueDictionary routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoute(string routeName, object routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoutePermanent(object routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoutePermanent(string routeName)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoutePermanent(RouteValueDictionary routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoutePermanent(string routeName, object routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RemoveOutputCacheItem(string path)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void RemoveOutputCacheItem(string path, string providerName)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SetCookie(HttpCookie cookie)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void TransmitFile(string filename)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void TransmitFile(string filename, long offset, long length)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Write(char ch)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Write(object obj)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Write(string s)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void WriteFile(string filename)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void WriteFile(string filename, bool readIntoMemory)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void WriteFile(IntPtr fileHandle, long offset, long size)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void WriteFile(string filename, long offset, long size)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }
    }

}
