using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Ioc构建器扩展 -- 程序集
    /// </summary>
    public static class DanceIocBuilder_Assembly
    {
        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="assemblies">程序集集合</param>
        /// <returns>Ioc构建器</returns>
        public static DanceIocBuilder AddAssemblies(this DanceIocBuilder builder, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    var singletons = type.GetCustomAttributes<DanceSingletonAttribute>(false);
                    foreach (var singleton in singletons)
                    {
                        builder.AddSingleton(singleton.Key, singleton.ServiceType ?? type, type);
                    }

                    var lifescopes = type.GetCustomAttributes<DanceLifeScopeAttribute>(false);
                    foreach (var lifescope in lifescopes)
                    {
                        builder.AddLifeScope(lifescope.Key, lifescope.ServiceType ?? type, type);
                    }
                }
            }

            return builder;
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="searchPattern">通配符</param>
        /// <returns>Ioc构建器</returns>
        public static DanceIocBuilder AddAssemblies(this DanceIocBuilder builder, string searchPattern)
        {
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern, SearchOption.TopDirectoryOnly))
            {
                if (!file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    continue;

                builder.AddAssemblies(Assembly.LoadFrom(file));
            }

            return builder;
        }
    }
}
