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
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    /// <param name="viewType">视图类型</param>
    public abstract class DanceDockingItemPluginInfoBase(string id, string name, Type viewType) : IDancePluginInfo
    {
        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; } = viewType;

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = name;
    }
}
