using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 打开的文档实体
    /// </summary>
    public class OpendDocumentEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [BsonId(true)]
        public int ID { get; set; }

        /// <summary>
        /// 绑定名称
        /// </summary>
        public string? BindableName { get; set; }

        /// <summary>
        /// 插件生命周期
        /// </summary>
        public string? PluginNameSpace { get; set; }

        /// <summary>
        /// 插件分组
        /// </summary>
        public string? PluginGroup { get; set; }

        /// <summary>
        /// 插件ID
        /// </summary>
        public string? PluginID { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 是否允许关闭
        /// </summary>
        public bool AllowClose { get; set; }
    }
}
