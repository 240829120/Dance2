using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 状态栏位置
    /// </summary>
    public enum DanceStatusBarLocation
    {
        /// <summary>
        /// 左下
        /// </summary>
        LeftBottom,

        /// <summary>
        /// 右下
        /// </summary>
        RightBottom,

        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
    }

    /// <summary>
    /// 状态插件信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    public class DanceStatusBarPluginInfo(string id, string name) : DanceBarPluginInfoBase(id, name)
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        public List<DanceToolBarControlModel> BarItems { get; } = [];

        /// <summary>
        /// 位置
        /// </summary>
        public DanceStatusBarLocation Location { get; set; }
    }
}
