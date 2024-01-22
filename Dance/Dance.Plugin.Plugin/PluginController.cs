using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Plugin
{
    /// <summary>
    /// 帮助控制器
    /// </summary>
    public class PluginController
    {
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "插件菜单";

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IDanceWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();

        /// <summary>
        /// 主菜单
        /// </summary>
        private readonly BarPluginItemModel MainSubItem = new();

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public BarPluginItemModel CreateMainMenu()
        {
            this.MainSubItem.Content = "插件";
            this.MainSubItem.Order = DefaultMainMenuOrders.Plugin;
            this.MainSubItem.ClickCommand = new DanceCommand(COMMAND_GROUP, "插件", this.PluginManage);

            return this.MainSubItem;
        }

        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 插件管理
        /// </summary>
        private async Task PluginManage()
        {
            PluginManageWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();

            await Task.CompletedTask;
        }
    }
}
