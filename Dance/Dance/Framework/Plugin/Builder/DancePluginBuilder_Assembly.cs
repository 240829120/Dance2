using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">插件构建器</param>
        /// <param name="searchPattern">通配符</param>
        public static void AddAssemblies(this DancePluginBuilder builder, string searchPattern)
        {
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern, SearchOption.TopDirectoryOnly))
            {
                if (!file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    continue;

                builder.PluginAssemblies.Add(Assembly.LoadFrom(file));
            }
        }
    }
}