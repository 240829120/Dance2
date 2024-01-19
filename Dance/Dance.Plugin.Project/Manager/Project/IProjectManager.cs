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
    public interface IProjectManager
    {
        /// <summary>
        /// 当前项目
        /// </summary>
        ProjectDomain? Current { get; set; }
    }
}
