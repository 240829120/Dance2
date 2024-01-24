using Dance.Plugin.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器管理器
    /// </summary>
    public interface IExplorerManager
    {
        /// <summary>
        /// 资源管理器信息
        /// </summary>
        List<ExplorerInfo> ExplorerInfos { get; }

        /// <summary>
        /// 节点集合
        /// </summary>
        DanceObservableCollection<ExplorerNodeModel> Nodes { get; }

        /// <summary>
        /// 扩展名过滤器
        /// </summary>
        List<string> ExtensionFilters { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="project">项目领域</param>
        void Initialize(ProjectDomain project);

        /// <summary>
        /// 取消初始化
        /// </summary>
        void UnInitialize();
    }
}
