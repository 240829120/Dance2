using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目打开之前消息
    /// </summary>
    /// <param name="workPath">工作路径</param>
    public class ProjectOpeningMsg(string workPath)
    {
        /// <summary>
        /// 工作路径
        /// </summary>
        public string WorkPath { get; } = workPath;

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
