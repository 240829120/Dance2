﻿using Dance.Framework;
using Dance.Plugin.Dock;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin
{
    /// <summary>
    /// 布局控制器
    /// </summary>
    /// <remarks>
    /// ---------------------------------------------------------
    ///     布局(L)
    ///       | -- 保存布局
    ///       | -- 应用布局
    ///       |       | -- 测试布局1
    ///       |       | -- 测试布局2
    ///       |       | -- 测试布局3
    ///       | -- 管理布局
    ///       | -- 重置布局
    ///       | -- 保存默认布局 [ 该菜单在调试模式下显示 ]
    /// ---------------------------------------------------------
    /// </remarks>
    public class LayoutController
    {
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "布局菜单";

        /// <summary>
        /// 配置管理器
        /// </summary>
        private readonly IDanceConfigManager ConfigManager = DanceDomain.Current.LifeScope.Resolve<IDanceConfigManager>();

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IDanceWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        /// <summary>
        /// 主菜单
        /// </summary>
        private readonly DanceBarSubItemModel MainSubItem = new();

        /// <summary>
        /// 保存布局
        /// </summary>
        private readonly DanceBarButtonItemModel SaveLayoutItem = new();

        /// <summary>
        /// 应用布局
        /// </summary>
        private readonly DanceBarSubItemModel ApplyLayoutItem = new();

        /// <summary>
        /// 管理布局
        /// </summary>
        private readonly DanceBarButtonItemModel ManageLayoutItem = new();

        /// <summary>
        /// 重置布局
        /// </summary>
        private readonly DanceBarButtonItemModel ResetLayoutItem = new();

        /// <summary>
        /// 保存默认布局 -- 该菜单在调试模式下显示
        /// </summary>
        private readonly DanceBarButtonItemModel SaveDefaultLayoutItem = new();

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public DanceBarSubItemModel CreateMainMenu()
        {
            var layoutCollection = this.ConfigManager.Context.GetLayouts();

            // 保存布局
            this.SaveLayoutItem.Content = "保存布局";
            this.SaveLayoutItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Dock;component/Themes/Icons/save_layout.svg");
            this.SaveLayoutItem.ClickCommand = new(COMMAND_GROUP, "保存布局", this.SaveLayout);

            // 应用布局
            this.ApplyLayoutItem.Content = "应用布局";
            var layouts = layoutCollection.Find(p => !p.IsMainLayout && !p.IsDefaultLayout).OrderBy(p => p.Order);
            foreach (var layout in layouts)
            {
                DanceBarButtonItemModel layoutItem = new()
                {
                    Content = layout.Name,
                    ClickCommand = new(COMMAND_GROUP, $"应用布局: {layout.Name}", async () => await LayoutItemClick(layout))
                };

                this.ApplyLayoutItem.Items.Add(layoutItem);
            }

            // 管理布局
            this.ManageLayoutItem.Content = "管理布局";
            this.ManageLayoutItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Dock;component/Themes/Icons/manage_layout.svg");
            this.ManageLayoutItem.ClickCommand = new(COMMAND_GROUP, "管理布局", this.ManageLayout);

            // 重置布局
            this.ResetLayoutItem.Content = "重置布局";
            this.ResetLayoutItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Dock;component/Themes/Icons/reset_layout.svg");
            this.ResetLayoutItem.ClickCommand = new(COMMAND_GROUP, "重置布局", this.ResetLayout);

            // 保存默认布局
            this.SaveDefaultLayoutItem.Content = "保存默认布局";
            this.SaveDefaultLayoutItem.ClickCommand = new(COMMAND_GROUP, "保存默认布局", this.SaveDefaultLayout);

            // 主菜单
            this.MainSubItem.Content = "布局(_L)";
            this.MainSubItem.Order = DefaultMainMenuOrders.Layout;

            this.MainSubItem.Items.Add(this.SaveLayoutItem);
            this.MainSubItem.Items.Add(this.ApplyLayoutItem);
            this.MainSubItem.Items.Add(this.ManageLayoutItem);
            this.MainSubItem.Items.Add(this.ResetLayoutItem);
            if (DanceDomain.Current.IsDebugMode)
            {
                this.MainSubItem.Items.Add(new DanceBarSeparatorItemModel());
                this.MainSubItem.Items.Add(this.SaveDefaultLayoutItem);
            }

            return this.MainSubItem;
        }

        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 保存布局
        /// </summary>
        private async Task SaveLayout()
        {
            var layoutCollection = this.ConfigManager.Context.GetLayouts();
            DanceMainWindowModel mainVM = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();

            // 输入布局名称
            LayoutInputNameWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };

            if (window.ShowDialog() != true || window.DataContext is not LayoutInputNameWindowModel vm)
                return;

            // 获取并保存布局信息
            string? xml = mainVM.GetLayout();
            if (string.IsNullOrWhiteSpace(xml))
                return;

            LayoutEntity layout = new()
            {
                Name = vm.LayoutName,
                Content = xml,
                Order = layoutCollection.Count() > 0 ? (layoutCollection.Max(p => p.Order) + 1) : 0
            };

            layoutCollection.Upsert(layout);

            // 添加布局项菜单
            DanceBarButtonItemModel layoutItem = new()
            {
                Content = vm.LayoutName,
                ClickCommand = new("布局菜单", $"应用布局: {vm.LayoutName}", async () => await LayoutItemClick(layout))
            };

            this.ApplyLayoutItem.Items.Add(layoutItem);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 保存默认布局
        /// </summary>
        private async Task SaveDefaultLayout()
        {
            var layoutCollection = this.ConfigManager.Context.GetLayouts();
            DanceMainWindowModel mainVM = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();

            // 获取并保存布局信息
            string? xml = mainVM.GetLayout();
            if (string.IsNullOrWhiteSpace(xml))
                return;

            LayoutEntity layout = layoutCollection.FindOne(p => p.IsDefaultLayout) ?? new();

            layout.Name = "DEFAULT";
            layout.Content = xml;
            layout.IsDefaultLayout = true;

            layoutCollection.Upsert(layout);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 管理布局
        /// </summary>
        private async Task ManageLayout()
        {
            LayoutManageWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };

            if (window.ShowDialog() != true)
                return;

            this.ApplyLayoutItem.Items.Clear();

            var layoutCollection = this.ConfigManager.Context.GetLayouts();
            var layouts = layoutCollection.Find(p => !p.IsMainLayout && !p.IsDefaultLayout).OrderBy(p => p.Order);
            foreach (var layout in layouts)
            {
                DanceBarButtonItemModel layoutItem = new()
                {
                    Content = layout.Name,
                    ClickCommand = new("布局菜单", $"应用布局: {layout.Name}", async () => await LayoutItemClick(layout))
                };

                this.ApplyLayoutItem.Items.Add(layoutItem);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 重置布局
        /// </summary>
        private async Task ResetLayout()
        {
            var layoutCollection = this.ConfigManager.Context.GetLayouts();
            LayoutEntity? layout = layoutCollection.FindOne(p => p.IsDefaultLayout);
            if (layout == null || string.IsNullOrWhiteSpace(layout.Content))
                return;

            DanceMainWindowModel mainVM = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            mainVM.SetLayout(layout.Content);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 布局项点击
        /// </summary>
        /// <param name="layout">布局数据</param>
        private static async Task LayoutItemClick(LayoutEntity layout)
        {
            if (string.IsNullOrWhiteSpace(layout.Content))
                return;

            DanceMainWindowModel mainVM = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();

            mainVM.SetLayout(layout.Content);

            await Task.CompletedTask;
        }
    }
}