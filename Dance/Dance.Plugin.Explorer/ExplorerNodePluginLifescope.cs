using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点插件生命周期
    /// </summary>
    public class ExplorerNodePluginLifescope : DanceObject, IDancePluginLifescope
    {
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            ExplorerNodePluginInfo info = new(new DancePluginKey("Dance", "Explorer", "DefaultNode"), "默认文件节点");
            info.Infos.Add(new ExplorerInfo(".txt", this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Explorer;component/Themes/Icons/txt.svg")));

            return info;
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
