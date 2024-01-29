using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 文件打开消息
    /// </summary>
    /// <param name="path">文件路径</param>
    public class FileOpendMsg(string path)
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; } = path;
    }
}
