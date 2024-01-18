using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目关闭消息
    /// </summary>
    /// <param name="closedProject">关闭的项目</param>
    public class ProjectClosedMsg(DanceProjectDomain closedProject)
    {
        /// <summary>
        /// 已经关闭的项目
        /// </summary>
        public DanceProjectDomain ClosedProject { get; } = closedProject;
    }
}
