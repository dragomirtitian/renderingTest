using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IRR2.Common.Tests
{
    class FakeHttpWorkerRequest : HttpWorkerRequest
    {
        public override void EndOfRequest()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void FlushResponse(bool finalFlush)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetHttpVerbName()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetHttpVersion()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetLocalAddress()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override int GetLocalPort()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetQueryString()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetRawUrl()
        {
            return "/";
        }

        public override string GetRemoteAddress()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override int GetRemotePort()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string GetUriPath()
        {
            return "/";
        }

        public override void SendKnownResponseHeader(int index, string value)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }
    }
}
