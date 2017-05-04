using IRR2.Common.Tests.MvcRendering.PathProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace IRR2.Common.Tests.MvcRendering
{
    public class RenderingEnviroment
    {
        private RenderingEnviroment()
        {

        }
        private static object sync = new object();
        public static RenderingEnviroment Instance { get; private set; }
        public string AppVPath { get; private set; }
        public string DomainId { get; private set; }
        public string AppId { get; private set; }
        public string AppDomain { get; private set; }
        public string Path { get; private set; }

        public static void Initialize(string path, string appId = "1", string domainId = "LM/W3SVC/1/ROOT-1-131381888751591619", string appVPath = "/", string appDomain  = "*")
        {
            lock (sync)
            {
                if (Instance != null)
                {
                    throw new NotSupportedException();
                }

                Instance = new RenderingEnviroment
                {
                    Path = path,
                    AppId = appId,
                    DomainId = domainId,
                    AppVPath = appVPath,
                    AppDomain = appDomain,
                };
                Instance.BuildEnviroment();
            }
        }

        private void BuildEnviroment()
        {
            this.SetDomainData();
            this.ResetConfigurationManager();
            this.InitHostingEnvironment();
            this.CreateVirtualPathProvider();
        }
        private void CreateVirtualPathProvider()
        {
            CallContext.SetData("__TemporaryVirtualPathProvider__", new RenderingVirtualPathProvider());
        }
        private void SetDomainData()
        {
            var domain = Thread.GetDomain();
            domain.SetData(".appDomain", this.AppDomain);
            domain.SetData(".appId", this.AppId);
            domain.SetData(".appPath", this.Path);
            domain.SetData(".appVPath", this.AppVPath);
            domain.SetData(".domainId", this.DomainId);
        }

        private void ResetConfigurationManager()
        {
            typeof(System.Configuration.ConfigurationManager)
                .GetField("s_initState", BindingFlags.Static | BindingFlags.NonPublic)
                .SetValue(null, 0);
        }

        private void InitHostingEnvironment()
        {
            var env = new HostingEnvironment();
            typeof(HostingEnvironment)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(_ => _.Name == "Initialize" && _.GetParameters().Length == 5)
                .Invoke(env, new object[]
                {
                    null,
                    new RenderingApplicationHost(this),
                    new RenderingConfigMapPathFactory(this),
                    null,
                    null,
                });
        }

    }
}
