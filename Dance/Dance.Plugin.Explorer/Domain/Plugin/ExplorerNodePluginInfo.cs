using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点插件信息
    /// </summary>
    /// <param name="id">插件ID</param>
    /// <param name="name">插件名称</param>
    public class ExplorerNodePluginInfo(DancePluginKey id, string name) : IDancePluginInfo
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public DancePluginKey Key { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 资源管理器信息
        /// </summary>
        public List<ExplorerInfo> Infos { get; } = [];
    }
}
