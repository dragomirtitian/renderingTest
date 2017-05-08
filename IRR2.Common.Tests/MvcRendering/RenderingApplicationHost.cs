using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;

namespace IRR2.Common.Tests.MvcRendering
{
    public class RenderingApplicationHost : IApplicationHost
    {
        public RenderingApplicationHost(RenderingEnviroment env)
        {
            this.Enviroment = env;
        }

        public RenderingEnviroment Enviroment { get; private set; }

        public IConfigMapPathFactory GetConfigMapPathFactory()
        {
            return new RenderingConfigMapPathFactory(this.Enviroment);
        }

        public IntPtr GetConfigToken()
        {
            return IntPtr.Zero;
        }

        public string GetPhysicalPath()
        {
            return this.Enviroment.Path;
        }

        public string GetSiteID()
        {
            return this.Enviroment.AppId;
        }

        public string GetSiteName()
        {
            return this.Enviroment.AppDomain;
        }

        public string GetVirtualPath()
        {
            return this.Enviroment.AppVPath;
        }

        public void MessageReceived()
        {
        }
    }

}
