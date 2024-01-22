using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Help
{
    /// <summary>
    /// 帮助插件生命周期
    /// </summary>
    public class HelpPluginLifescope : DanceObject, IDancePluginLifescope
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
            DanceBarPluginInfo info = new(new("Dance", "Help", "HelpMainMenu"), "帮助主菜单");

            info.MenuItems.Add(this.HelpController.CreateMainMenu());

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
        /// 帮助控制器
        /// </summary>
        private readonly HelpController HelpController = new();
    }
}
