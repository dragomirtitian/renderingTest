using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace IRR2.Common.Tests.MvcRendering
{
    class RenderingConfigMapPathFactory : IConfigMapPathFactory
    {
        private readonly RenderingEnviroment Enviroment;

        public RenderingConfigMapPathFactory(RenderingEnviroment enviroment)
        {
            this.Enviroment = enviroment;
        }
        public IConfigMapPath Create(string virtualPath, string physicalPath)
        {
            return new ConfigMapPath();
        }


        class ConfigMapPath : IConfigMapPath
        {
            public string GetAppPathForPath(string siteID, string path)
            {
                System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
            }

            public void GetDefaultSiteNameAndID(out string siteName, out string siteID)
            {
                System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
            }

            public string GetMachineConfigFilename()
            {
                return @"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config";
            }

            public void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
            {
                directory = this.MapPath(siteID, path);
                if (directory != null)
                {
                    baseName = "web.config";
                }
                else
                {
                    baseName = null;
                }
            }

            public string GetRootWebConfigFilename()
            {
                return @"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\Web.config";
            }

            public string MapPath(string siteID, string path)
            {
                return Path.Combine(@"C:\Users\drago\Desktop\renderingTest\RenderingTest", path.Substring(1).Replace('/', '\\'));
            }

            public void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
            {
                System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
            }
        }
    }
}
