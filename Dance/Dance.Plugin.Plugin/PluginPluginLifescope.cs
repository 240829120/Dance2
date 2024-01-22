using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Plugin
{
    /// <summary>
    /// 插件插件生命周期
    /// </summary>
    public class PluginPluginLifescope : DanceObject, IDancePluginLifescope
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
            DanceBarPluginInfo info = new(new("Dance", "Plugin", "PluginMainMenu"), "插件主菜单");

            info.MenuItems.Add(this.PluginController.CreateMainMenu());

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
        /// 插件控制器
        /// </summary>
        private readonly PluginController PluginController = new();
    }
}
