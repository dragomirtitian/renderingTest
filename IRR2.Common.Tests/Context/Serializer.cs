using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;

namespace IRR2.Common.Tests.Context
{
    public class Serializer
    {
        public static string Serialize(HttpContext context)
        {

            //var 
            var worker = (HttpWorkerRequest)((IServiceProvider)context).GetService(typeof(HttpWorkerRequest));

            var data = new
            {
                GetAppPath = worker.GetAppPath(),
                GetAppPoolID = worker.GetAppPoolID(),
                GetClientCertificate = worker.GetClientCertificate(),
                GetClientCertificateBinaryIssuer = worker.GetClientCertificateBinaryIssuer(),
                GetClientCertificateEncoding = worker.GetClientCertificateEncoding(),
                GetClientCertificatePublicKey = worker.GetClientCertificatePublicKey(),
                GetClientCertificateValidFrom = worker.GetClientCertificateValidFrom(),
                GetClientCertificateValidUntil = worker.GetClientCertificateValidUntil(),
                GetConnectionID = worker.GetConnectionID(),
                GetFilePath = worker.GetFilePath(),
                GetFilePathTranslated = worker.GetFilePathTranslated(),
                GetHttpVerbName = worker.GetHttpVerbName(),
                GetHttpVersion = worker.GetHttpVersion(),
                GetKnownRequestHeader = Enumerable.Range(0, 40).Select(worker.GetKnownRequestHeader).ToArray(),
                GetLocalAddress = worker.GetLocalAddress(),
                GetLocalPort = worker.GetLocalPort(),
                GetPathInfo = worker.GetPathInfo(),
                GetPreloadedEntityBodyLength = worker.GetPreloadedEntityBodyLength(),
                GetPreloadedEntityBody = worker.GetPreloadedEntityBody(),
                GetProtocol = worker.GetProtocol(),
                GetQueryString = worker.GetQueryString(),
                GetQueryStringRawBytes = worker.GetQueryStringRawBytes(),
                GetRawUrl = worker.GetRawUrl(),
                GetRemoteAddress = worker.GetRemoteAddress(),
                GetRemoteName = worker.GetRemoteName(),
                GetRemotePort = worker.GetRemotePort(),
                GetRequestReason = worker.GetRequestReason(),
                GetServerName = worker.GetServerName(),
                //GetServerVariable = worker.GetServerVariable(),
                GetTotalEntityBodyLength = worker.GetTotalEntityBodyLength(),
                //GetUnknownRequestHeader = worker.GetUnknownRequestHeader(),
                GetUnknownRequestHeaders = worker.GetUnknownRequestHeaders(),
                GetUriPath = worker.GetUriPath(),
                GetUrlContextID = worker.GetUrlContextID(),
                HasEntityBody = worker.HasEntityBody(),
                MachineConfigPath = worker.MachineConfigPath,
                MachineInstallDirectory = worker.MachineInstallDirectory,
                //ReadEntityBody = worker.ReadEntityBody(),
                RequestTraceIdentifier = worker.RequestTraceIdentifier,
                RootWebConfigPath = worker.RootWebConfigPath,
            };

            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new HttpContractResolver(),
                Converters =
                {
                    new NameValueJsonConverter(),
                    new StreamJsonConverter(),
                }
            });
        }
    }

    public class HttpContractResolver: DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var items = (List<JsonProperty>)base.CreateProperties(type, memberSerialization);
            items.RemoveAll(_ => typeof(ProfileBase).IsAssignableFrom(_.PropertyType));
            return items;
        }
    }

    public class NameValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(NameValueCollection).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var nv = ((NameValueCollection)value);
            var dic = nv.Keys.Cast<string>().ToDictionary(_ => _, _ => nv[_]);
            serializer.Serialize(writer, dic);
        }
    }
    public class StreamJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Stream).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, "STREAM");
        }
    }
}
