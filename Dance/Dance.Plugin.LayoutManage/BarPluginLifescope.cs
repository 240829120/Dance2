using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.LayoutManage
{
    /// <summary>
    /// 菜单插件生命周期
    /// </summary>
    public class BarPluginLifescope : DanceObject, IDancePluginLifescope
    {
        // ===================================================================================================
        // **** IDancePluginLifescope ****
        // ===================================================================================================

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IDancePluginInfo Register()
        {
            DanceBarPluginInfo info = new("Dance.Plugin.LayoutManage", "布局管理");

            info.MenuBarItems.Add(this.CreateToolBar());

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {

        }

        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建工具项
        /// </summary>
        /// <returns></returns>
        private DanceBarSubItemModel CreateToolBar()
        {
            DanceBarSubItemModel sub = new()
            {
                Content = $"布局(_L)"
            };

            // 保存布局
            sub.Items.Add(new DanceBarButtonItemModel()
            {
                Content = "保存布局",
                ClickCommand = new DanceCommand("保存布局", this.SaveLayout, false)
            });

            // 应用布局


            // 管理布局
            sub.Items.Add(new DanceBarButtonItemModel()
            {
                Content = "管理布局",
                ClickCommand = new DanceCommand("管理布局", this.ManageLayout, false)
            });

            // 重置布局
            sub.Items.Add(new DanceBarButtonItemModel()
            {
                Content = "重置布局",
                ClickCommand = new DanceCommand("重置布局", this.ResetLayout, false)
            });

            return sub;
        }

        /// <summary>
        /// 保存布局
        /// </summary>
        private async Task SaveLayout()
        {

        }

        /// <summary>
        /// 应用布局
        /// </summary>
        private async Task ApplyLayout()
        {

        }

        /// <summary>
        /// 管理布局
        /// </summary>
        private async Task ManageLayout()
        {

        }

        /// <summary>
        /// 重置布局
        /// </summary>
        private async Task ResetLayout()
        {

        }
    }
}
