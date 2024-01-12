﻿using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Office.Model;
using DevExpress.Xpf.LayoutControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 主视图模型
    /// </summary>
    [DanceSingleton]
    public class DanceMainViewModel : DanceViewModel
    {
        public DanceMainViewModel()
        {
            this.LoadedCommand = new(this.Loaded);
        }

        // =======================================================================================
        // Property

        /// <summary>
        /// 布局容器视图
        /// </summary>
        public ObservableCollection<DanceLayoutViewModel> Layouts { get; } = [];

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DanceDocumentViewModel> Documents { get; } = [];

        /// <summary>
        /// 菜单项
        /// </summary>
        public ObservableCollection<DanceToolBarControlModel> MenuBarItems { get; } = [];

        /// <summary>
        /// 工具项
        /// </summary>
        public ObservableCollection<DanceToolBarControlModel> ToolBarItems { get; } = [];

        /// <summary>
        /// 状态项 -- 左下
        /// </summary>
        public ObservableCollection<DanceToolBarControlModel> StatusBarLeftBottomItems { get; } = [];

        /// <summary>
        /// 状态项 -- 右下
        /// </summary>
        public ObservableCollection<DanceToolBarControlModel> StatusBarRightBottomItems { get; } = [];

        /// <summary>
        /// 状态项 -- 右上
        /// </summary>
        public ObservableCollection<DanceToolBarControlModel> StatusBarRightTopItems { get; } = [];

        // =======================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private void Loaded()
        {
            // 菜单项
            this.LoadMenuBarItems();

            // Docking项
            this.LoadDockingItem();
        }

        #endregion

        // =======================================================================================
        // Private Function

        /// <summary>
        /// 加载菜单项
        /// </summary>
        private void LoadMenuBarItems()
        {
            var items = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceBarPluginInfo).ToList();

            foreach (var item in items)
            {
                if (item.PluginInfo is not DanceBarPluginInfo info)
                    continue;

                this.MenuBarItems.AddRange(info.MenuBarItems);
                this.ToolBarItems.AddRange(info.ToolBarItems);
                this.StatusBarLeftBottomItems.AddRange(info.StatusBarLeftBottomItems);
                this.StatusBarRightBottomItems.AddRange(info.StatusBarRightBottomItems);
                this.StatusBarRightTopItems.AddRange(info.StatusBarRightTopItems);
            }
        }

        /// <summary>
        /// 加载布局项
        /// </summary>
        private void LoadDockingItem()
        {
            if (this.View is not DanceMainView view || view.PART_DockLayoutManager == null)
                return;

            // 面板与文档视图
            var layouts = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceLayoutViewPluginInfo).ToList();

            foreach (var layout in layouts)
            {
                if (layout.PluginInfo is not DanceLayoutViewPluginInfo info)
                    continue;

                this.Layouts.Add(new()
                {
                    ID = info.ID,
                    Caption = info.Name,
                    ToolTip = info.Name,
                    ViewType = info.ViewType
                });
            }

            // 加载布局文件
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            if (!File.Exists(path))
                return;

            view.PART_DockLayoutManager.RestoreLayoutFromXml(path);
        }
    }
}