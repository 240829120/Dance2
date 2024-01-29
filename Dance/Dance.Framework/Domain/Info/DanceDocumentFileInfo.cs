using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 文件文档信息
    /// </summary>
    /// <param name="path">文件路径</param>
    public class DanceDocumentFileInfo(string path) : DanceDocumentInfoBase
    {
        /// <summary>
        /// 文档类型
        /// </summary>
        public override DanceDocumentType DocumentType => DanceDocumentType.File;

        #region Path -- 文件路径

        private string path = path;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get { return path; }
            set { this.SetProperty(ref path, value); }
        }

        #endregion
    }
}
