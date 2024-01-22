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
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    /// <param name="group">分组</param>
    public class ProjectPluginInfo(string id, string name, string group) : IDancePluginInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = group;

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
