using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace IRR2.Common.Tests.MvcRendering.PathProvider
{

    internal class MapPathBasedVirtualPathCollection : MarshalByRefObject, IEnumerable
    {
        private RequestedEntryType _requestedEntryType;
        private DirectoryInfo _dirInfo;
        private string virtualPath;

        internal MapPathBasedVirtualPathCollection(string virtualPath, DirectoryInfo dirInfo, RequestedEntryType requestedEntryType)
        {
            this.virtualPath = virtualPath;
            this._dirInfo = dirInfo;
            this._requestedEntryType = requestedEntryType;
        }

        public override object InitializeLifetimeService() =>
            null;

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<VirtualFileBase> files = new VirtualFileBase[0];
            if (_requestedEntryType.HasFlag(RequestedEntryType.Files))
            {
                files = files.Concat(this._dirInfo.EnumerateFiles().Select(_ => new RenderingVirtualFile(Path.Combine(virtualPath, _.Name), _.FullName, _)));
            }
            if (_requestedEntryType.HasFlag(RequestedEntryType.Directories))
            {
                files = files.Concat(this._dirInfo.EnumerateDirectories().Select(_ => new RenderingVirtualDirectory(Path.Combine(virtualPath, _.Name), _.FullName, _)));
            }
            return files.GetEnumerator();
        }
    }
    enum RequestedEntryType
    {
        Directories = 0x1, Files = 0x1 << 1, All = Directories & Files
    }

}
