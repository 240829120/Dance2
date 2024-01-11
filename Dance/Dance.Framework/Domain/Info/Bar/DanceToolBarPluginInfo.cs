using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 工具插件信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    public class DanceToolBarPluginInfo(string id, string name) : DanceBarPluginInfoBase(id, name)
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        public List<DanceToolBarControlModel> BarItems { get; } = [];
    }
}
