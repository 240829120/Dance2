using Dance.Framework;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 配置上下文扩展
    /// </summary>
    public static class ConfigContentExpansion
    {
        /// <summary>
        /// 获取最近使用的项目列表
        /// </summary>
        /// <param name="context">配置上下文</param>
        /// <returns>最近使用的项目列表</returns>
        public static ILiteCollection<RecentlyUsedProjectEntity> GetRecentlyUsedProjects(this DanceConfigContext context)
        {
            return context.Database.GetCollection<RecentlyUsedProjectEntity>();
        }
    }
}
