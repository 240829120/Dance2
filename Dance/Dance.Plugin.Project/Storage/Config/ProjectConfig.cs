using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目配置
    /// </summary>
    public class ProjectConfig : DanceObject
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Detail { get; set; }

        /// <summary>
        /// 项目插件ID
        /// </summary>
        public string? PluginID { get; set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string? PluginName { get; set; }
    }
}
