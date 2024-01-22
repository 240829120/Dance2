using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 视图插件生命周期
    /// </summary>
    public class ViewPluginLifescope : DanceObject, IDancePluginLifescope
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
            DanceBarPluginInfo info = new(new("Dance", "Dock", "ViewMainMenu"), "视图主菜单");

            info.MenuItems.Add(this.ViewController.CreateMainMenu());

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
        /// 视图控制器
        /// </summary>
        private readonly ViewController ViewController = new();
    }
}
