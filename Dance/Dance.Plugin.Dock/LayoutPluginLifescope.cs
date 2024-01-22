using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 布局插件生命周期
    /// </summary>
    public class LayoutPluginLifescope : DanceObject, IDancePluginLifescope
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
            DanceBarPluginInfo info = new(new("Dance", "Dock", "LayoutMainMenu"), "布局主菜单");

            info.MenuItems.Add(this.LayoutController.CreateMainMenu());

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {

        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 布局控制器
        /// </summary>
        private readonly LayoutController LayoutController = new();
    }
}
