using Dance.Framework;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 配置上下文扩展
    /// </summary>
    public static class ConfigContentExpansion
    {
        /// <summary>
        /// 获取布局表
        /// </summary>
        /// <param name="context">配置上下文</param>
        /// <returns>布局表</returns>
        public static ILiteCollection<LayoutEntity> GetLayouts(this DanceConfigContext context)
        {
            return context.Database.GetCollection<LayoutEntity>();
        }
    }
}
