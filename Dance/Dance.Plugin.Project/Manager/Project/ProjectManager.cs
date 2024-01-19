using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目管理器
    /// </summary>
    [DanceSingleton(typeof(IProjectManager))]
    public class ProjectManager : IProjectManager
    {
        /// <summary>
        /// 当前项目
        /// </summary>
        public ProjectDomain? Current { get; set; }
    }
}
