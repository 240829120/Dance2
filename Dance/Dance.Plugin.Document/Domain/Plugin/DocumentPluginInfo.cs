using Dance.Plugin.Explorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Document
{
    /// <summary>
    /// 文档插件信息
    /// </summary>
    public class DocumentPluginInfo(DancePluginKey id, string name) : IDancePluginInfo
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