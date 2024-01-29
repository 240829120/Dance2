using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 空插件信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    public class DanceNonePluginInfo(DancePluginKey id, string name) : IDancePluginInfo
    {
        /// <summary>
        /// 插件编号
        /// </summary>
        public DancePluginKey Key { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;
    }
}
