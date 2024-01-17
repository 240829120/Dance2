using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Wpf;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Office.Model;
using DevExpress.Xpf.Core;
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
    public class DanceMainWindowModel : DanceViewModel
    {
        public DanceMainWindowModel()
        {
            this.LoadedCommand = new(this.Loaded);
        }

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 布局容器视图
        /// </summary>
        public ObservableCollection<DanceLayoutViewModel> Layouts { get; } = [];

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DanceDocumentViewModel> Documents { get; } = [];

        /// <summary>
        /// Bar集合
        /// </summary>
        public ObservableCollection<DanceBarModelBase> Bars { get; } = [];

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

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

            // 发送初始化完成消息
            DanceDomain.Current.Messenger.Send(new DanceMainWindowLoadedMessage());
        }

        #endregion

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 获取布局
        /// </summary>
        /// <returns>布局信息</returns>
        public string? GetLayout()
        {
            if (this.View is not DanceMainWindow window)
                return null;

            using MemoryStream ms = new();
            window.PART_DockLayoutManager.SaveLayoutToStream(ms);
            ms.Position = 0;
            byte[] buffer = ms.ToArray();
            string xml = Encoding.UTF8.GetString(buffer);

            return xml;
        }

        /// <summary>
        /// 设置布局
        /// </summary>
        /// <param name="xml">布局信息</param>
        public void SetLayout(string xml)
        {
            if (this.View is not DanceMainWindow window)
                return;

            using MemoryStream ms = new(Encoding.UTF8.GetBytes(xml));
            window.PART_DockLayoutManager.RestoreLayoutFromStream(ms);
        }

        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 加载菜单项
        /// </summary>
        private void LoadMenuBarItems()
        {
            var items = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is DanceBarPluginInfo).ToList();

            List<DanceBarItemModelBase> mainMenuItems = [];
            List<DanceToolBarModel> toolItems = [];
            List<DanceBarItemModelBase> statusItems = [];

            foreach (var item in items)
            {
                if (item.PluginInfo is not DanceBarPluginInfo info)
                    continue;

                mainMenuItems.AddRange(info.MenuItems);
                toolItems.AddRange(info.ToolItems);
                statusItems.AddRange(info.StatusItems);
            }

            DanceMainMenuModel mainMenu = new();
            mainMenu.Items.AddRange([.. mainMenuItems.OrderBy(p => p.Order)]);
            this.Bars.Add(mainMenu);

            DanceStatusBarModel statusBar = new();
            statusBar.Items.AddRange([.. statusItems.OrderBy(p => p.Order)]);
            this.Bars.Add(statusBar);

            this.Bars.AddRange([.. toolItems.OrderBy(p => p.Order)]);
        }

        /// <summary>
        /// 加载布局项
        /// </summary>
        private void LoadDockingItem()
        {
            if (this.View is not DanceMainWindow view || view.PART_DockLayoutManager == null)
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
                    AllowClose = info.AllowClose,
                    ViewType = info.ViewType,
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