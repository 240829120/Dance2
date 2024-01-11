using CommunityToolkit.Mvvm.Input;
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
        public ObservableCollection<DanceBarItemModelBase> MenuBarItems { get; } = [];

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

            // 工具项
            this.LoadToolBarItems();

            // 状态项
            this.LoadStatusBarItems();

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
            var items = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceMenuBarPluginInfo).ToList();

            foreach (var item in items)
            {
                if (item.PluginInfo is not DanceMenuBarPluginInfo info)
                    continue;

                this.MenuBarItems.AddRange(info.BarItems);
            }
        }

        /// <summary>
        /// 加载工具项
        /// </summary>
        private void LoadToolBarItems()
        {
            var items = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceToolBarPluginInfo).ToList();

            foreach (var item in items)
            {
                if (item.PluginInfo is not DanceToolBarPluginInfo info)
                    continue;

                this.ToolBarItems.AddRange(info.BarItems);
            }
        }

        /// <summary>
        /// 加载状态项
        /// </summary>
        private void LoadStatusBarItems()
        {
            var items = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceStatusBarPluginInfo).ToList();

            foreach (var item in items)
            {
                if (item.PluginInfo is not DanceStatusBarPluginInfo info)
                    continue;

                switch (info.Location)
                {
                    case DanceStatusBarLocation.LeftBottom:
                        this.StatusBarLeftBottomItems.AddRange(info.BarItems);
                        break;
                    case DanceStatusBarLocation.RightBottom:
                        this.StatusBarRightBottomItems.AddRange(info.BarItems);
                        break;
                    case DanceStatusBarLocation.RightTop:
                        this.StatusBarRightTopItems.AddRange(info.BarItems);
                        break;
                }
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