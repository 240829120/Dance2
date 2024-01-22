using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目插件信息
    /// </summary>
    /// <param name="id">插件ID</param>
    /// <param name="name">名称</param>
    /// <param name="categoryGroup">分类分组</param>
    public class ProjectPluginInfo(DancePluginID id, string name, string categoryGroup) : IDancePluginInfo
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public DancePluginID ID { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 分类分组
        /// </summary>
        public string CategoryGroup { get; } = categoryGroup;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Detail { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource? Icon { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<string> Tags { get; set; } = [];
    }
}
