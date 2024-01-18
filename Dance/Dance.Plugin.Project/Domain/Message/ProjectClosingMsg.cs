using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目关闭之前消息
    /// </summary>
    public class ProjectClosingMsg
    {
        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
