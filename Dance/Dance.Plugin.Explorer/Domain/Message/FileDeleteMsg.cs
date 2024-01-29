using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 文件删除消息
    /// </summary>
    /// <param name="path">路径</param>
    public class FileDeleteMsg(string path)
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; } = path;
    }
}
