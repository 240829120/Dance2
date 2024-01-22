using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Help
{
    /// <summary>
    /// 帮助控制器
    /// </summary>
    /// <remarks>
    /// ---------------------------------------------------------
    ///     帮助(H)
    ///       | -- 查看帮助
    ///       | -- 关于软件
    /// ---------------------------------------------------------
    /// </remarks>
    public class HelpController
    {
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "帮助菜单";

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        /// <summary>
        /// 主菜单
        /// </summary>
        private readonly DanceBarSubItemModel MainSubItem = new();

        /// <summary>
        /// 查看帮助
        /// </summary>
        private readonly DanceBarButtonItemModel ShowHelpItem = new();

        /// <summary>
        /// 关于
        /// </summary>
        private readonly DanceBarButtonItemModel AboutItem = new();

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public DanceBarSubItemModel CreateMainMenu()
        {
            // 查看帮助
            this.ShowHelpItem.Content = "查看帮助";
            this.ShowHelpItem.ClickCommand = HelpOptions.ShowHelpCommand;
            this.ShowHelpItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Help;component/Themes/Icons/help.svg");

            // 关于
            this.AboutItem.Content = "关于";
            this.AboutItem.ClickCommand = HelpOptions.AboutCommand;
            this.AboutItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Help;component/Themes/Icons/about.svg");

            // 主菜单
            this.MainSubItem.Content = "帮助(_H)";
            this.MainSubItem.Order = DefaultMainMenuOrders.Help;

            this.MainSubItem.Items.Add(this.ShowHelpItem);
            this.MainSubItem.Items.Add(this.AboutItem);

            return this.MainSubItem;
        }
    }

}
