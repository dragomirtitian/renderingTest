using IRR2.Common.Tests.MvcRendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;

namespace IRR2.Common.Tests
{
    public class FakeHttpWorkerRequest : HttpWorkerRequest
    {
        private readonly dynamic data;
        private readonly IApplicationHost applicationHost;
        private readonly IConfigMapPath mapPath;

        public FakeHttpWorkerRequest(dynamic data, IApplicationHost appHost)
        {
            this.applicationHost = appHost;
            this.mapPath = this.applicationHost.GetConfigMapPathFactory().Create(null, null);
            this.data = data;
        }
        public string GetData(string name) => this.GetData<string>(name);
        public T[][] GetDataArrayOfArray<T>(string name)
        {
            return ((JArray)data[name])
                ?.Select(_ => (JArray)_)
                ?.Select(_=> _?.Select(x => x.Value<T>())?.ToArray())
                ?.ToArray();
        }
        public T[] GetDataArray<T>(string name)
        {
            return ((JArray)data[name])?.Select(_=> _.Value<T>())?.ToArray();
        }

        public T GetData<T>(string name)
        {
            return (T)data[name];
        }
        public override void EndOfRequest()
        {
            
        }

        public override void FlushResponse(bool finalFlush)
        {
            
        }

        public override string GetAppPath() => GetData(nameof(GetAppPath));

        public override string GetAppPathTranslated() => mapPath.MapPath(this.GetAppPoolID(), this.GetAppPath());

        public override byte[] GetClientCertificate() => GetDataArray<byte>(nameof(GetClientCertificate));

        public override byte[] GetClientCertificateBinaryIssuer() => GetDataArray<byte>(nameof(GetClientCertificateBinaryIssuer));

        public override int GetClientCertificateEncoding() => GetData<int>(nameof(GetClientCertificateEncoding));

        public override byte[] GetClientCertificatePublicKey() => GetDataArray<byte>(nameof(GetClientCertificatePublicKey));

        public override DateTime GetClientCertificateValidFrom() => GetData<DateTime>(nameof(GetClientCertificateValidFrom));

        public override DateTime GetClientCertificateValidUntil() => GetData<DateTime>(nameof(GetClientCertificateValidUntil));

        public override long GetConnectionID() => GetData<int>(nameof(GetConnectionID));

        public override string GetFilePath() => GetData(nameof(GetFilePath));

        public override string GetFilePathTranslated() => mapPath.MapPath(this.GetAppPoolID(), this.GetFilePath());

        public override string GetHttpVerbName() => GetData(nameof(GetHttpVerbName));

        public override string GetHttpVersion() => GetData(nameof(GetHttpVersion));

        public override string GetKnownRequestHeader(int index) => GetDataArray<string>(nameof(GetKnownRequestHeader))[index];

        public override string GetLocalAddress() => GetData(nameof(GetLocalAddress));

        public override int GetLocalPort() => GetData<int>(nameof(GetLocalPort));

        public override string GetPathInfo() => GetData(nameof(GetPathInfo));

        public override int GetPreloadedEntityBodyLength() => GetData<int>(nameof(GetPreloadedEntityBodyLength));

        public override byte[] GetPreloadedEntityBody() => Encoding.UTF8.GetBytes(GetData(nameof(GetPreloadedEntityBody)));

        public override string GetQueryString() => GetData(nameof(GetQueryString));

        public override string GetProtocol() => GetData(nameof(GetProtocol));

        public override string GetRawUrl() => GetData(nameof(GetRawUrl));
        
        public override string GetRemoteAddress() => GetData(nameof(GetRemoteAddress));

        public override int GetRemotePort() => GetData<int>(nameof(GetRemotePort));

        public override int GetRequestReason() => GetData<int>(nameof(GetRequestReason));

        public override string GetServerName() => GetData(nameof(GetServerName));

        public override int GetTotalEntityBodyLength() => GetData<int>(nameof(GetTotalEntityBodyLength));

        public override string GetUnknownRequestHeader(string name) => GetUnknownRequestHeaders().FirstOrDefault(_ => _[0] == name)?[1];

        public override string[][] GetUnknownRequestHeaders() => GetDataArrayOfArray<string>(nameof(GetUnknownRequestHeaders));

        public override string GetUriPath() => GetData(nameof(GetUriPath));

        public override long GetUrlContextID() => GetData<long>(nameof(GetUrlContextID));

        public override string MachineInstallDirectory => RenderingConfigurationSystem.MsCorLibDirectory;

        public override string RootWebConfigPath => RenderingConfigurationSystem.MachineConfigPath;

        public override string MachineConfigPath => RenderingConfigurationSystem.MachineConfigPath;

        public override void SendKnownResponseHeader(int index, string value)
        {
            this.SendUnknownResponseHeader(GetKnownResponseHeaderName(index), value);
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
        }

        MemoryStream stream = new MemoryStream();
        public void WriteToResult(byte[] data, int? length = null)
        {
            stream.Write(data, 0, length ?? data.Length);
        }
        bool hasLength = false;
        public override void SendResponseFromMemory(byte[] data, int length)
        {
            if(!hasLength)
            {
                var httpLength = Encoding.ASCII.GetString(data);
                this.SendUnknownResponseHeader("http-length", httpLength.Trim());
                hasLength = true;
                return;
            }
            this.WriteToResult(data, length);
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
            byte[] bytes = Encoding.ASCII.GetBytes($"<!-- statusCode: {statusCode} statusDescription: {statusDescription} --> \r\n");
            this.WriteToResult(bytes);
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes($"<!-- {name}: {value} --> \r\n");
            this.WriteToResult(bytes);
        }

        public void CopyTo(string fileName)
        {
            using (var w = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                this.CopyTo(w);
            }
        }

        public void CopyTo(FileStream w)
        {
            this.stream.WriteTo(w);
        }
    }
}
