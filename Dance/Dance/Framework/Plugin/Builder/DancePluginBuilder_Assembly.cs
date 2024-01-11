using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public static class DancePluginBuilder_Assembly
    {
        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">插件构建器</param>
        /// <param name="assemblies">程序集</param>
        public static void AddAssemblies(this DancePluginBuilder builder, params Assembly[] assemblies)
        {
            builder.PluginAssemblies.AddRange(assemblies);
        }
    }
}