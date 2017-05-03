using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Renderer
{
    class ApplicationHost:  IApplicationHost
    {
        public System.Web.Configuration.IConfigMapPathFactory GetConfigMapPathFactory()
        {
            return new ConfigMapPathFactory();
        }

        public IntPtr GetConfigToken()
        {
            return IntPtr.Zero;
        }

        public string GetPhysicalPath()
        {
            return @"c:\users\titian.dragomir\documents\visual studio 2013\Projects\RenderingTest\RenderingTest\";
        }

        public string GetSiteID()
        {
            return "/LM/W3SVC/12/ROOT";
        }

        public string GetSiteName()
        {
            return "*";
        }

        public string GetVirtualPath()
        {
            return "/";
        }

        public void MessageReceived()
        {
            
        }
    }

     class  ConfigMapPathFactory :  IConfigMapPathFactory
     {

         public IConfigMapPath Create(string virtualPath, string physicalPath)
         {
             return new ConfigMapPath();
         }
     }

    class ConfigMapPath: IConfigMapPath
    {

        public string GetAppPathForPath(string siteID, string path)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultSiteNameAndID(out string siteName, out string siteID)
        {
            throw new NotImplementedException();
        }

        public string GetMachineConfigFilename()
        {
            return @"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config";
        }

        public void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
        {
            throw new NotImplementedException();
        }

        public string GetRootWebConfigFilename()
        {
            return @"C:\Users\titian.dragomir\Documents\visual studio 2013\Projects\RenderingTest\RenderingTest\Web.config";
        }

        public string MapPath(string siteID, string path)
        {
            return Path.Combine(@"C:\Users\titian.dragomir\Documents\visual studio 2013\Projects\RenderingTest\RenderingTest\", path);
        }

        public void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
        {
            throw new NotImplementedException();
        }
    }
}
