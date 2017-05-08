using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace IRR2.Common.Tests.MvcRendering.PathProvider
{

    internal class RenderingVirtualFile : VirtualFile
    {
        private FileInfo _ffd;
        private string _physicalPath;

        internal RenderingVirtualFile(string virtualPath) : base(virtualPath)
        {
        }

        internal RenderingVirtualFile(string virtualPath, string physicalPath, FileInfo ffd) : base(virtualPath)
        {
            this._physicalPath = physicalPath;
            this._ffd = ffd;
        }

        private void EnsureFileInfoObtained()
        {
            if (this._physicalPath == null)
            {
                this._physicalPath = HostingEnvironment.MapPath(base.VirtualPath);
                _ffd = new FileInfo(this._physicalPath);
            }
        }

        public override Stream Open()
        {
            this.EnsureFileInfoObtained();
            return new FileStream(this._physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override string Name
        {
            get
            {
                this.EnsureFileInfoObtained();
                return this._ffd?.Name;
            }
        }

        internal string PhysicalPath
        {
            get
            {
                this.EnsureFileInfoObtained();
                return this._physicalPath;
            }
        }
    }
}
