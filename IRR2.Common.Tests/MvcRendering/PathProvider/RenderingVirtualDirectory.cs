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

    internal class RenderingVirtualDirectory : VirtualDirectory
    {
        private string _physicalPath;
        private DirectoryInfo _ffd;

        public RenderingVirtualDirectory(string virtualPath, string physicalPath, DirectoryInfo dirInfo) : base(virtualPath)
        {
            this._ffd = dirInfo;
            this._physicalPath = physicalPath;
        }


        public RenderingVirtualDirectory(string virtualPath) : base(virtualPath)
        {
        }

        private DirectoryInfo EnsureFileInfoObtained()
        {
            if (this._physicalPath == null)
            {
                this._physicalPath = HostingEnvironment.MapPath(base.VirtualPath);
                _ffd = new DirectoryInfo(this._physicalPath);
            }
            return _ffd;
        }

        public override IEnumerable Children =>
            new MapPathBasedVirtualPathCollection(this.VirtualPath, this.EnsureFileInfoObtained(), RequestedEntryType.All);

        public override IEnumerable Directories =>
            new MapPathBasedVirtualPathCollection(this.VirtualPath, this.EnsureFileInfoObtained(), RequestedEntryType.Directories);

        public override IEnumerable Files =>
            new MapPathBasedVirtualPathCollection(this.VirtualPath, this.EnsureFileInfoObtained(), RequestedEntryType.Directories);
    }


}
