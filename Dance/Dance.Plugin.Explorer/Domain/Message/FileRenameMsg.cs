using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 文件修改名称消息
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="oldPath">原始路径</param>
    public class FileRenameMsg(string path, string oldPath)
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; } = path;

        /// <summary>
        /// 原始路径
        /// </summary>
        public string OldPath { get; } = oldPath;
    }
}
