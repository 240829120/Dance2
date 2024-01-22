using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 视图控制器
    /// </summary>   
    /// <remarks>
    /// ---------------------------------------------------------
    ///     视图(V)
    ///       | -- 测试视图1
    ///       | -- 测试视图2
    ///       | -- 测试视图3
    /// ---------------------------------------------------------
    /// </remarks>
    public class ViewController
    {
        public ViewController()
        {
            DanceDomain.Current.Messenger.Register<DanceMainWindowLoadedMsg>(this, this.DanceMainWindowLoaded);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "视图菜单";

        /// <summary>
        /// 主菜单
        /// </summary>
        private readonly DanceBarSubItemModel MainSubItem = new();

        // ===================================================================================================
        // **** Message ****
        // ===================================================================================================

        #region DanceMainWindowLoadedMessage -- 主窗口加载完成消息

        /// <summary>
        /// 主窗口加载完成消息
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="msg">消息</param>
        private void DanceMainWindowLoaded(object sender, DanceMainWindowLoadedMsg msg)
        {
            DanceMainWindowModel mainVM = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            foreach (var layout in mainVM.Layouts)
            {
                ViewBarCheckBoxItemModel item = new()
                {
                    Content = layout.Caption,
                    Description = layout.ToolTip ?? layout.Caption,
                    DockingItem = layout
                };

                this.MainSubItem.Items.Add(item);
            }
        }

        #endregion

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public DanceBarSubItemModel CreateMainMenu()
        {
            this.MainSubItem.Content = "视图(_V)";
            this.MainSubItem.Order = DefaultMainMenuOrders.View;

            return this.MainSubItem;
        }
    }
}
