﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 菜单插件信息
    /// </summary>
    /// <param name="id">插件编号</param>
    /// <param name="name">名称</param>
    public class DanceBarPluginInfo(DancePluginKey id, string name) : IDancePluginInfo
    {
        /// <summary>
        /// 插件编号
        /// </summary>
        public DancePluginKey Key { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 菜单项
        /// </summary>
        public List<DanceBarItemModelBase> MenuItems { get; } = [];

        /// <summary>
        /// 工具项集合
        /// </summary>
        public List<DanceToolBarModel> ToolItems { get; } = [];

        /// <summary>
        /// 状态项集合
        /// </summary>
        public List<DanceBarItemModelBase> StatusItems { get; } = [];
    }
}
