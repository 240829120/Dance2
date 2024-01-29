using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework.Plugin
{
    /// <summary>
    /// Docking项插件信息基类
    /// </summary>
    /// <param name="key">插件Key</param>
    /// <param name="viewType">视图类型</param>
    public abstract class DanceDockingItemPluginInfoBase(DancePluginKey key, string name, Type viewType) : IDancePluginInfo
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public DancePluginKey Key { get; } = key;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; } = viewType;

        /// <summary>
        /// 是否允许关闭
        /// </summary>
        public bool AllowClose { get; set; } = true;
    }
}
