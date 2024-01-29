using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 文档信息基类
    /// </summary>
    public abstract class DanceDocumentInfoBase : DanceModel
    {
        /// <summary>
        /// 文档类型
        /// </summary>
        public abstract DanceDocumentType DocumentType { get; }
    }
}
