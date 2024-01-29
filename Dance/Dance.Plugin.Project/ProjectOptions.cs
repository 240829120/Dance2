using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目设置
    /// </summary>
    public static class ProjectOptions
    {
        /// <summary>
        /// 项目扩展名
        /// </summary>
        public static string ProjectExtension { get; set; } = ".dance";

        /// <summary>
        /// 项目缓存文件名
        /// </summary>
        public static string ProjectCacheFileName { get; set; } = "cache.db";

        /// <summary>
        /// 项目缓存日志文件
        /// </summary>
        public static string ProjectCacheLogFileName { get; set; } = "cache-log.db";

        /// <summary>
        /// 最近使用项目个数
        /// </summary>
        public static int RecentlyUsedProjectCount { get; set; } = 10;
    }
}
