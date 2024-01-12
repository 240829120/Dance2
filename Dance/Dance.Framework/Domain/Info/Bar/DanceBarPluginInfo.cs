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
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    public class DanceBarPluginInfo(string id, string name) : IDancePluginInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 菜单项
        /// </summary>
        public List<DanceToolBarControlModel> MenuBarItems { get; } = [];

        /// <summary>
        /// 工具项集合
        /// </summary>
        public List<DanceToolBarControlModel> ToolBarItems { get; } = [];

        /// <summary>
        /// 状态项 -- 左下
        /// </summary>
        public List<DanceToolBarControlModel> StatusBarLeftBottomItems { get; } = [];

        /// <summary>
        /// 状态项 -- 右下
        /// </summary>
        public List<DanceToolBarControlModel> StatusBarRightBottomItems { get; } = [];

        /// <summary>
        /// 状态项 -- 右上
        /// </summary>
        public List<DanceToolBarControlModel> StatusBarRightTopItems { get; } = [];
    }
}
