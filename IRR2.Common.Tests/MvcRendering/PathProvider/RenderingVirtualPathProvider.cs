using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace IRR2.Common.Tests.MvcRendering.PathProvider
{

    class RenderingVirtualPathProvider : VirtualPathProvider
    {
        private static string _AppRoot;
        ConcurrentDictionary<string, bool> cache = new ConcurrentDictionary<string, bool>();
        private bool CacheLookupOrInsert(string virtualPath, bool isFile)
        {
            string physicalPath = HostingEnvironment.MapPath(virtualPath);
            bool doNotCacheUrlMetadata = false;
            string key = null;
            if (!doNotCacheUrlMetadata)
            {
                key = this.CreateCacheKey(isFile, physicalPath);
                bool nullable;
                if (cache.TryGetValue(key, out nullable))
                {
                    return nullable;
                }
            }
            bool flag2 = isFile ? File.Exists(physicalPath) : Directory.Exists(physicalPath);
            if (!doNotCacheUrlMetadata)
            {
                CacheDependency dependencies = null;
                string filename = flag2 ? physicalPath : FileUtil.GetFirstExistingDirectory(AppRoot, physicalPath);
                if (filename != null)
                {
                    dependencies = new CacheDependency(filename);
                    cache.GetOrAdd(key, flag2);
                }
            }
            return flag2;
        }

        private string CreateCacheKey(bool isFile, string physicalPath)
        {
            if (isFile)
            {
                return ("Bf" + physicalPath);
            }
            return ("Bd" + physicalPath);
        }

        public override bool DirectoryExists(string virtualDir) =>
            this.CacheLookupOrInsert(virtualDir, false);

        public override bool FileExists(string virtualPath) =>
            this.CacheLookupOrInsert(virtualPath, true);

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (virtualPathDependencies == null)
            {
                return null;
            }
            StringCollection strings = null;
            foreach (string str in virtualPathDependencies)
            {
                string str2 = HostingEnvironment.MapPath(str);
                if (strings == null)
                {
                    strings = new StringCollection();
                }
                strings.Add(str2);
            }
            if (strings == null)
            {
                return null;
            }
            string[] array = new string[strings.Count];
            strings.CopyTo(array, 0);
            return new CacheDependency(array, utcStart);
        }

        public override VirtualDirectory GetDirectory(string virtualDir) =>
            new RenderingVirtualDirectory(virtualDir);

        public override VirtualFile GetFile(string virtualPath) =>
            new RenderingVirtualFile(virtualPath);

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            return this.ToString();
        }

        private static string AppRoot
        {
            get
            {
                string str = _AppRoot;
                if (str == null)
                {
                    str = FileUtil.RemoveTrailingDirectoryBackSlash(Path.GetFullPath(HttpRuntime.AppDomainAppPath));
                    _AppRoot = str;
                }
                return str;
            }
        }
    }

}
