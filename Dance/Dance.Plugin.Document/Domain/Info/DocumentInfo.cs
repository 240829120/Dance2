using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Document
{
    /// <summary>
    /// 文档信息
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        /// 支持的扩展名
        /// </summary>
        public List<string> Extensions { get; } = [];

        /// <summary>
        /// 视图
        /// </summary>
        public Type? ViewType { get; set; }

        /// <summary>
        /// /打开之后出发
        /// </summary>
        public Action<string>? Opend { get; set; }
    }
}
